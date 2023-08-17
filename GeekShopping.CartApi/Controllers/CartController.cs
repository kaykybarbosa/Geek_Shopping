using GeekShopping.CartApi.Dtos.Request;
using GeekShopping.CartApi.Interfaces;
using GeekShopping.CartApi.Messages;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace GeekShopping.CartApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService ?? throw new ArgumentNullException(nameof(cartService));
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
        public async Task<IActionResult> Checkout(CheckoutHeaderDto dto)
        {
            if (ModelState.IsValid)
            {
                var cart = await _cartService.FindCartByUserId(dto.UserId);
                if(cart == null)
                {
                    return NotFound();
                }
                
                //RabbitMq here

                dto.CartDetails = cart.CartDetails;

                return Ok(dto);
            }

            return BadRequest();
        }
    }
}