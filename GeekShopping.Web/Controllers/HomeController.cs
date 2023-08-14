using GeekShopping.Web.Interfaces;
using GeekShopping.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GeekShopping.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly ICartService _cartService;

        public HomeController(ILogger<HomeController> logger, 
            IProductService productService, 
            ICartService cartService)
        {
            _logger = logger;
            _productService = productService;
            _cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.FindAllProducts("");

            return View(products);
        }

        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            string token = await HttpContext.GetTokenAsync("access_token");
            var product = await _productService.FindProductById(id, token);
            
            return View(product);
        }

        [HttpPost]
        [Authorize]
        [ActionName("Details")]
        public async Task<IActionResult> Details(ProductViewModel product)
        {
            string token = await HttpContext.GetTokenAsync("access_token");

            CartViewModel cart = new()
            {
                CartHeader = new()
                {
                    UserId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value
                }
            };

            List<CartDetailViewModel> cartDetails = new List<CartDetailViewModel>() 
            {
                new CartDetailViewModel()
                {
                    Count = product.Count,
                    ProductId = product.Id,
                    Product = await _productService.FindProductById(product.Id, token)
                }
            };

            cart.CartDetails = cartDetails;

            var response = await _cartService.AddItemToCart(cart, token);
            if(response != null)
            {
                return RedirectToAction("Index");
            }

            return View(product);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
        public async Task<IActionResult> Login()
        {
            return RedirectToAction("Index");
        }

        public IActionResult Logout()
        {
            return SignOut("Cookies", "oidc");
        }
    }
}