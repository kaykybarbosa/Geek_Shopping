using GeekShopping.CouponApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.CouponApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CouponController : ControllerBase
    {
        private readonly ICouponService _couponService;

        public CouponController(ICouponService couponService)
        {
            _couponService = couponService ?? throw new ArgumentNullException(nameof(couponService));
        }

        [Authorize]
        [HttpGet("find-coupon-code/{couponCode}")]
        public async Task<IActionResult> GetCouponByCouponCode(string couponCode)
        {
            var coupon = await _couponService.GetCouponByCouponCode(couponCode);

            if(coupon != null)
            {
                return Ok(coupon);
            }

            return NotFound();
        }
    }
}