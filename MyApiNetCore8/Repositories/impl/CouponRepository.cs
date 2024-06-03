using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyApiNetCore8.Data;
using MyApiNetCore8.DTO.Response;
using MyApiNetCore8.Model;

namespace MyApiNetCore8.Repositories.impl
{
    public class CouponRepository : ICouponRepository
    {
        private readonly MyContext _context;
        private readonly IMapper _mapper;

        public CouponRepository(MyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<CouponResponse> CreateCoupon(CouponRequest coupon)
        {
            var couponEntity = _mapper.Map<Coupon>(coupon);
           
            _context.Add(couponEntity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CouponResponse>(couponEntity);
            throw new NotImplementedException();
        }


        public void DeleteCoupon(long id)
        {
            var coupon = _context.Coupons.Find(id);
            _context.Coupons.Remove(coupon);
            _context.SaveChanges();
        }

        public async Task<List<CouponResponse>> GetAllCoupons()
        {
            var coupons = await _context.Coupons.ToListAsync();

            foreach (var coupon in coupons)
            {
                if (!IsValidDateTime(coupon.ExpiryDate))
                {
                    coupon.ExpiryDate = DateTime.MinValue; 
                }
            }

            return _mapper.Map<List<CouponResponse>>(coupons);
        }

        private bool IsValidDateTime(DateTime dateTime)
        {
            return dateTime >= (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue &&
                   dateTime <= (DateTime)System.Data.SqlTypes.SqlDateTime.MaxValue;
        }
        private string GenerateRandomCode(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }


        public async Task<CouponResponse> GetCouponById(long id)
        {
            var coupon = await _context.Coupons.FirstOrDefaultAsync(x => x.Id == id);
            return _mapper.Map<CouponResponse>(coupon);


        }

        public async Task<CouponResponse> UpdateCoupon(long id, CouponRequest coupon)
        {
            var existingCoupon = await _context.Coupons.FindAsync(id);
            if (existingCoupon == null)
            {
                throw new KeyNotFoundException($"Coupon with id {id} not found.");
            }

            _mapper.Map(coupon, existingCoupon);

            _context.Coupons.Update(existingCoupon);

            await _context.SaveChangesAsync();

            return _mapper.Map<CouponResponse>(existingCoupon);
        }

    }
}
