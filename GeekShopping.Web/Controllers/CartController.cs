using GeekShopping.Web.Interfaces;
using GeekShopping.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [Authorize]
        public async Task<IActionResult> CartIndex()
        {
            var response = await FindUserCart();

            return View(response);
        }

        [ActionName("ApplyCoupon")]
        public async Task<IActionResult> ApplyCoupon(CartViewModel cartView)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;

            var response = await _cartService.ApplyCoupon(cartView, token);
            if (response)
            {
                return RedirectToAction("CartIndex");
            }

            return View();
        } 
        
        [ActionName("RemoveCoupon")]
        public async Task<IActionResult> RemoveCoupon()
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;

            var response = await _cartService.RemoveCoupon(userId, token);
            if (response)
            {
                return RedirectToAction("CartIndex");
            }

            return View();
        }

        public async Task<IActionResult> Remove(long id)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;

            var response = await _cartService.RemoveFromCart(id, token);

            if (response)
            {
                return RedirectToAction("CartIndex");
            }

            return View();
        }

        private async Task<CartViewModel> FindUserCart()
        {
            string token = await HttpContext.GetTokenAsync("access_token");
            var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;

            var response = await _cartService.FindCartByUserId(userId, token);

            if (response?.CartHeader != null)
            {
                foreach (var detail in response.CartDetails)
                {
                    var price = detail.Product.Price;
                    var count = detail.Product.Count;

                    response.CartHeader.PurchaseAmount += (price * count);
                }
            }

            return response;
        }
    }
}