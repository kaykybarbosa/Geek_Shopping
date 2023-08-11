using GeekShopping.CartApi.Dtos.Request;
using GeekShopping.CartApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> FindCartById(string userId)
        {
            if (ModelState.IsValid)
            {
                var result = await _cartService.FindCartByUserId(userId);

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
        public async Task<IActionResult> RemoveCart(long userId )
        {
            if (ModelState.IsValid)
            {
                var result = await _cartService.RemoveFromCart(userId);

                if (!result) return NotFound();

                return Ok(result);
            }

            return BadRequest();
        }
    }
}