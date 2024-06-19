using MyApiNetCore8.DTO.Response;

namespace MyApiNetCore8.Repositories
{
    public interface ICouponService
    {
        Task<List<CouponResponse>> GetAllCoupons();
        Task<CouponResponse> GetCouponById(long id);
        Task<CouponResponse> CreateGlobalCoupon(CouponRequest coupon);
        Task DeleteCoupon(long[] ids);
        Task<CouponResponse> GetCouponByCode(string code);


    }
}



