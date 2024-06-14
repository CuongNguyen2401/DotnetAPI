using Microsoft.AspNetCore.Mvc;
using MyApiNetCore8.DTO.Response;
using MyApiNetCore8.Model;
using MyApiNetCore8.Repositories;

namespace MyApiNetCore8.Controllers
{
    [Route("api/[controller]")]
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
        public async Task<ActionResult<Coupon>> PostCoupon(CouponRequest coupon)
        {
            var couponResponse = await _service.CreateCoupon(coupon);
            return CreatedAtAction("GetCoupon", new ApiResponse<CouponResponse>(1000, "Success", couponResponse));

        }

        // DELETE: api/Coupons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoupon(long id)
        {
           var coupon = await _service.GetCouponById(id);
            if (coupon == null)
            {
                return NotFound();
            }
            _service.DeleteCoupon(id);
            return Ok(new ApiResponse<CouponResponse>(1000, "Success", coupon));
        }

        private bool CouponExists(long id)
        {
            return _service.GetCouponById(id) != null;
        }
    }
}
