using MyApiNetCore8.DTO.Response;

namespace MyApiNetCore8.Repositories
{
    public interface ICouponRepository
    {
        Task<List<CouponResponse>> GetAllCoupons();
        Task<CouponResponse> GetCouponById(long id);
        Task<CouponResponse> CreateCoupon(CouponRequest coupon);
        Task<CouponResponse> UpdateCoupon(long id, CouponRequest coupon);
        void DeleteCoupon(long id);


    }
}
