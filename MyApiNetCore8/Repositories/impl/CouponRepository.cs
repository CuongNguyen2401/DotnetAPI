using AutoMapper;
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
        public Task<CouponResponse> CreateCoupon(CouponRequest coupon)
        {
            var couponEntity = _mapper.Map<Coupon>(coupon);
            //_context..Add(couponEntity);
            //await _context.SaveChangesAsync();
            //return _mapper.Map<CouponResponse>(couponEntity);
            throw new NotImplementedException();
        }

        public void DeleteCoupon(long id)
        {
            throw new NotImplementedException();
        }

        public Task<List<CouponResponse>> GetAllCoupons()
        {
            throw new NotImplementedException();
        }

        public Task<CouponResponse> GetCouponById(long id)
        {
            throw new NotImplementedException();
        }

        public Task<CouponResponse> UpdateCoupon(long id, CouponRequest coupon)
        {
            throw new NotImplementedException();
        }
    }
}
