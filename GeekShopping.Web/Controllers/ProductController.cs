using GeekShopping.Web.Interfaces;
using GeekShopping.Web.Models;
using GeekShopping.Web.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        public async Task<IActionResult> ProductIndex()
        {
            var products = await _productService.FindAllProducts("");

            return View(products);
        }

        public async Task<IActionResult> ProductCreate()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ProductCreate(ProductViewModel product)
        {
            if (ModelState.IsValid) 
            {
                string token = await HttpContext.GetTokenAsync("access_token");

                var response = await _productService.CreateProduct(product, token);

                if (response != null) 
                {
                    return RedirectToAction("ProductIndex");
                }
            }
            
            return View(product);
        }

        public async Task<IActionResult> ProductUpdate(long id)
        {
            string token = await HttpContext.GetTokenAsync("access_token");

            var product = await _productService.FindProductById(id, token);

            if(product != null) 
            { 
                return View (product);
            }

            return NotFound();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ProductUpdate(ProductViewModel product)
        {
            if (ModelState.IsValid) 
            {
                string token = await HttpContext.GetTokenAsync("access_token");

                var response = await _productService.UpdateProduct(product, token);

                if(response != null) 
                {
                    return RedirectToAction("ProductIndex");
                }
            }
             
            return View(product);
        }

        [Authorize]
        public async Task<IActionResult> ProductDelete(long id)
        {
            string token = await HttpContext.GetTokenAsync("access_token");

            var product = await _productService.FindProductById(id, token);

            if(product != null ) 
            {
                return View(product);
            }

            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> ProductDelete(ProductViewModel product)
        {
            string token = await HttpContext.GetTokenAsync("access_token");

            var response = await _productService.DeleteProductById(product.Id, token);

            if (response)
            {
                return RedirectToAction("ProductIndex");
            }

            return View(product);
        }
    }
}