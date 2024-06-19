using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyApiNetCore8.Data;
using MyApiNetCore8.DTO.Response;
using MyApiNetCore8.Model;
using System.Linq;

namespace MyApiNetCore8.Repositories.impl
{
    public class CouponService : ICouponService
    {
        private readonly MyContext _context;
        private readonly IMapper _mapper;

        public CouponService(MyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<CouponResponse> CreateGlobalCoupon(CouponRequest couponRequest)
        {
            // Assuming _context is an instance of AppDbContext and _couponMapper is an instance of a class that maps between entities and DTOs
            Coupon coupon = _mapper.Map<Coupon>(couponRequest);
            coupon.code = Guid.NewGuid().ToString();
            coupon.isGlobal = true;

            List<string> userIds = couponRequest.userIds;

            if (userIds != null && userIds.Any())
            {
            
                var users = await _context.Users.Where(u => userIds.Contains(u.Id)).ToListAsync();
                foreach (var user in users)
                {
                    user.coupons.Add(coupon);
                }
                coupon.users = new HashSet<User>(users);
            }

    
            await _context.Coupons.AddAsync(coupon);
            await _context.SaveChangesAsync();

            return _mapper.Map<CouponResponse>(coupon);
        }



        public async Task DeleteCoupon(long[] ids)
        {
            // Convert array to list for LINQ operations
            List<long> idList = ids.ToList();

            // Use the execution strategy to execute the transactional operations
            var executionStrategy = _context.Database.CreateExecutionStrategy();
            await executionStrategy.ExecuteAsync(async () =>
            {
                // Start a new transaction
                using (var transaction = await _context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        await DeleteUserCouponsByCouponIds(idList);

                        var couponsToDelete = await _context.Coupons.Where(c => idList.Contains(c.id)).ToListAsync();
                        _context.Coupons.RemoveRange(couponsToDelete);
                        await _context.SaveChangesAsync();

                        await transaction.CommitAsync();
                    }
                    catch (Exception)
                    {
                        // Rollback is handled by the execution strategy if needed
                        throw;
                    }
                }
            });
        }


        private async Task DeleteUserCouponsByCouponIds(List<long> ids)
        {
            var idListString = string.Join(",", ids);
            var sql = $"DELETE FROM usercoupon WHERE couponsid IN ({idListString})";
            await _context.Database.ExecuteSqlRawAsync(sql);
        }



        public async Task<List<CouponResponse>> GetAllCoupons()
        {
            var coupons = await _context.Coupons.ToListAsync();

            foreach (var coupon in coupons)
            {
                if (!IsValidDateTime(coupon.expiryDate))
                {
                    coupon.expiryDate = DateTime.MinValue; 
                }
            }

            return _mapper.Map<List<CouponResponse>>(coupons);
        }

        private bool IsValidDateTime(DateTime dateTime)
        {
            return dateTime >= (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue &&
                   dateTime <= (DateTime)System.Data.SqlTypes.SqlDateTime.MaxValue;
        }



        public async Task<CouponResponse> GetCouponById(long id)
        {
            var coupon = await _context.Coupons.FirstOrDefaultAsync(x => x.id == id);
            return _mapper.Map<CouponResponse>(coupon);


        }

        public async Task<CouponResponse> GetCouponByCode(string code)
        {
            var coupon = await _context.Coupons.FirstOrDefaultAsync(c => c.code == code);
            if (coupon == null)
            {
                throw new KeyNotFoundException($"Coupon with code {code} not found.");
            }
            return _mapper.Map<CouponResponse>(coupon);
        }

        
    }
}
