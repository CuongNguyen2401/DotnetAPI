using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApiNetCore8.Data;
using MyApiNetCore8.DTO.Response;
using MyApiNetCore8.Model;
using MyApiNetCore8.Repositories;

namespace MyApiNetCore8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponsController : ControllerBase
    {
        private readonly ICouponRepository _repository;

        public CouponsController(ICouponRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Coupons
        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<CouponResponse>>>> GetCoupon()
        {
           
                var couponResponses = await _repository.GetAllCoupons();
                return Ok(new ApiResponse<List<CouponResponse>>(1000, "Success", couponResponses));
            
            
        }



        // GET: api/Coupons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Coupon>> GetCoupon(long id)
        {
            var coupon = await _repository.GetCouponById(id);
            return Ok(new ApiResponse<CouponResponse>(1000, "Success", coupon));
        }

        // PUT: api/Coupons/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCoupon(long id, CouponRequest coupon)
        {
            var couponResponse = await _repository.UpdateCoupon(id, coupon);
            if (couponResponse == null)
            {
                return NotFound();
            }
            return Ok(new ApiResponse<CouponResponse>(1000, "Success", couponResponse));

        }

        // POST: api/Coupons
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Coupon>> PostCoupon(CouponRequest coupon)
        {
            var couponResponse = await _repository.CreateCoupon(coupon);
            return CreatedAtAction("GetCoupon", new ApiResponse<CouponResponse>(1000, "Success", couponResponse));

        }

        // DELETE: api/Coupons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoupon(long id)
        {
           var coupon = await _repository.GetCouponById(id);
            if (coupon == null)
            {
                return NotFound();
            }
            _repository.DeleteCoupon(id);
            return Ok(new ApiResponse<CouponResponse>(1000, "Success", coupon));
        }

        private bool CouponExists(long id)
        {
            return _repository.GetCouponById(id) != null;
        }
    }
}
