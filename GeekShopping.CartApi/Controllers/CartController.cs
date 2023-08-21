using GeekShopping.CartApi.Dtos.Request;
using GeekShopping.CartApi.Dtos.Response.Coupon;
using GeekShopping.CartApi.Dtos.Response.Message;
using GeekShopping.CartApi.Interfaces;
using GeekShopping.CartApi.Interfaces.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.CartApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly ICouponService _couponService;
        private readonly IRabbitMQMessageSender _rabbitMQMessageSender;

        public CartController(ICartService cartService, ICouponService couponService, IRabbitMQMessageSender rabbitMQMessageSender)
        {
            _cartService = cartService ?? throw new ArgumentNullException(nameof(cartService));
            _couponService = couponService ?? throw new ArgumentNullException(nameof(couponService));
            _rabbitMQMessageSender = rabbitMQMessageSender ?? throw new ArgumentNullException(nameof(rabbitMQMessageSender));
        }

        [HttpGet("find-cart/{id}")]
        public async Task<IActionResult> FindCartById(string id)
        {
            if (ModelState.IsValid)
            {
                var result = await _cartService.FindCartByUserId(id);

                if(result == null) return NotFound();

                return Ok(result);
            }

            return BadRequest();
        }

        [HttpPost("add-cart")]
        public async Task<IActionResult> CreateCart([FromBody] CartRequest cartRequest)
        {
            if (ModelState.IsValid)
            {
                var result = await _cartService.SaveOrUpdateCart(cartRequest);

                if (result == null) return NotFound();

                return Ok(result);
            }

            return BadRequest();
        }

        [HttpPut("update-cart")]
        public async Task<IActionResult> UpdateCart([FromBody] CartRequest cartRequest)
        {
            if (ModelState.IsValid)
            {
                var result = await _cartService.SaveOrUpdateCart(cartRequest);

                if (result == null) return NotFound();

                return Ok(result);
            }

            return BadRequest();
        }
               
        [HttpDelete("remove-cart/{id}")]
        public async Task<IActionResult> RemoveCart(long id)
        {
            if (ModelState.IsValid)
            {
                var result = await _cartService.RemoveFromCart(id);

                if (!result) return NotFound();

                return Ok(result);
            }

            return BadRequest();
        }

        [HttpPost("apply-coupon")]
        public async Task<IActionResult> ApplyCoupon(CartRequest cartRequest)
        {
            if (ModelState.IsValid)
            {
                var result = await _cartService.ApplyCoupon(cartRequest.CartHeader.UserId, cartRequest.CartHeader.CouponCode);

                if (!result)
                {
                    return NotFound();
                }

                return Ok(result);
            }

            return BadRequest();
        }
        
        [HttpDelete("remove-coupon/{userId}")]
        public async Task<IActionResult> RemoveCoupon(string userId)
        {
            if (ModelState.IsValid)
            {
                var result = await _cartService.RemoveCoupon(userId);

                if (!result)
                {
                    return NotFound();
                }

                return Ok(result);
            }

            return BadRequest();
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout(CheckoutHeaderResponse dto)
        {
            if (ModelState.IsValid)
            {
                if(dto?.UserId == null) return BadRequest();

                var token = await HttpContext.GetTokenAsync("access_token");

                var cart = await _cartService.FindCartByUserId(dto.UserId);
                if(cart == null)
                {
                    return NotFound();
                }

                var coupon = dto.CouponCode;
                if (!string.IsNullOrEmpty(coupon))
                {
                    CouponResponse response = await _couponService.GetCouponByCouponCode(coupon, token);
                    if(response.DiscountAmount != dto.DiscountAmount)
                    {
                        return StatusCode(412);
                    }
                }

                dto.CartDetails = cart.CartDetails;
                dto.DateTime = DateTime.Now;

                _rabbitMQMessageSender.SendMessage(dto, "checkoutqueue");

                return Ok(dto);
            }

            return BadRequest();
        }
    }
}