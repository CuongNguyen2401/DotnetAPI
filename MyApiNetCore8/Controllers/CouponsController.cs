using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApiNetCore8.DTO.Response;
using MyApiNetCore8.Helper;
using MyApiNetCore8.Model;
using MyApiNetCore8.Repositories;

namespace MyApiNetCore8.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CouponsController : ControllerBase
    {
        private readonly ICouponService _service;

        public CouponsController(ICouponService service)
        {
            _service = service;
        }

        // GET: api/Coupons
        [HttpGet]
        [Authorize(Roles = AppRole.Admin)]
        public async Task<ActionResult<ApiResponse<List<CouponResponse>>>> GetCoupon()
        {
           
                var couponResponses = await _service.GetAllCoupons();
                return Ok(new ApiResponse<List<CouponResponse>>(1000, "Success", couponResponses));
            
            
        }



        // GET: api/Coupons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Coupon>> GetCoupon(long id)
        {
            var coupon = await _service.GetCouponById(id);
            return Ok(new ApiResponse<CouponResponse>(1000, "Success", coupon));
        }


        // POST: api/Coupons
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = AppRole.Admin)]
        public async Task<ActionResult<Coupon>> PostCoupon(CouponRequest coupon)
        {
            var couponResponse = await _service.CreateCoupon(coupon);
            return CreatedAtAction("GetCoupon", new ApiResponse<CouponResponse>(1000, "Success", couponResponse));

        }

        // DELETE: api/Coupons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoupon(long[] ids)
        {
            await _service.DeleteCoupon(ids);
            return Ok(new ApiResponse<object>(1000, "Success", null));
         

        }
        [HttpGet("code/{code}")]
        public async Task<ActionResult<CouponResponse>> GetCouponByCode(string code)
        {
            var coupon = await _service.GetCouponByCode(code);
            return Ok(new ApiResponse<CouponResponse>(1000, "Success", coupon));
        }
            

        private bool CouponExists(long id)
        {
            return _service.GetCouponById(id) != null;
        }
    }
}
