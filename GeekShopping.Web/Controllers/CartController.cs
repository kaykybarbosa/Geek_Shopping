using GeekShopping.Web.Interfaces;
using GeekShopping.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Permissions;

namespace GeekShopping.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly ICouponService _couponService;

        public CartController(ICartService cartService, ICouponService couponService)
        {
            _cartService = cartService;
            _couponService = couponService;
        }

        [Authorize]
        public async Task<IActionResult> CartIndex()
        {
            var response = await FindUserCart();

            return View(response);
        }

        private async Task<CartViewModel> FindUserCart()
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;

            var response = await _cartService.FindCartByUserId(userId, token);

            if (response?.CartHeader != null)
            {
                var coupon = response.CartHeader.CouponCode;
                if (!string.IsNullOrEmpty(coupon))
                {
                    var resultCoupon = await _couponService.GetCoupon(coupon, token);
                    if(resultCoupon?.CouponCode != null)
                    {
                        response.CartHeader.DiscountAmount = resultCoupon.DiscountAmount;
                    }
                }

                foreach (var detail in response.CartDetails)
                {
                    var price = detail.Product.Price;
                    var count = detail.Product.Count;

                    response.CartHeader.PurchaseAmount += (price * count);
                }

                response.CartHeader.PurchaseAmount -= response.CartHeader.DiscountAmount;
            }

            return response;
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

        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            var response = await FindUserCart();

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(CartViewModel cart)
        {
            var token = await HttpContext.GetTokenAsync("access_token");

            var response = await _cartService.Checkout(cart.CartHeader, token);
            if(response != null)
            {
                return RedirectToAction("Confirmation");
            }

            return View(cart);
        }

        public async Task<IActionResult> Confirmation()
        {
            return View();
        }
    }
}