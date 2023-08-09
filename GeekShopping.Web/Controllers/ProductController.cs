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

        [Authorize]
        public async Task<IActionResult> ProductIndex()
        {
            var token = await HttpContext.GetTokenAsync("access_token");

            var products = await _productService.FindAllProducts(token);

            return View(products);
        }

        public async Task<IActionResult> ProductCreate()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ProductCreate(ProductModel product)
        {
            if (ModelState.IsValid) 
            {
                var token = await HttpContext.GetTokenAsync("access_token");

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
            var token = await HttpContext.GetTokenAsync("access_token");

            var product = await _productService.FindProductById(id, token);

            if(product != null) 
            { 
                return View (product);
            }

            return NotFound();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ProductUpdate(ProductModel product)
        {
            if (ModelState.IsValid) 
            {
                var token = await HttpContext.GetTokenAsync("access_token");

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
            var token = await HttpContext.GetTokenAsync("access_token");

            var product = await _productService.FindProductById(id, token);

            if(product != null ) 
            {
                return View(product);
            }

            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> ProductDelete(ProductModel product)
        {
            var token = await HttpContext.GetTokenAsync("access_token");

            var response = await _productService.DeleteProductById(product.Id, token);

            if (response)
            {
                return RedirectToAction("ProductIndex");
            }

            return View(product);
        }
    }
}