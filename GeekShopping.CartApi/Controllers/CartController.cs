using GeekShopping.CartApi.Dtos.Request;
using GeekShopping.CartApi.Dtos.Request.Message;
using GeekShopping.CartApi.Dtos.Response.Coupon;
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
        public async Task<IActionResult> Checkout(CheckoutHeaderRequest request)
        {
            if (ModelState.IsValid)
            {
                var token = await HttpContext.GetTokenAsync("access_token");

                var coupon = request.CouponCode;
                if (!string.IsNullOrEmpty(coupon))
                {
                    CouponResponse response = await _couponService.GetCouponByCouponCode(coupon, token);
                    if(response.DiscountAmount != request.DiscountAmount)
                    {
                        return StatusCode(412);
                    }
                }

                var cart = await _cartService.FindCartByUserId(request.UserId);
                if(cart == null) return NotFound();

                request.CartDetails = cart.CartDetails;
                request.DateTime = DateTime.Now;

                _rabbitMQMessageSender.SendMessage(request, "checkoutqueue");

                await _cartService.ClearCart(request.UserId);

                return Ok(request);
            }

            return BadRequest();
        }
    }
}