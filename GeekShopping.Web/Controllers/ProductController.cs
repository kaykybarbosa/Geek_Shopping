using GeekShopping.Web.Interfaces;
using GeekShopping.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
            var products = await _productService.FindAllProducts();

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
                var response = await _productService.CreateProduct(product);

                if (response != null) 
                {
                    return RedirectToAction("ProductIndex");
                }
            }
            
            return View(product);
        }

        public async Task<IActionResult> ProductUpdate(long id)
        {
            var product = await _productService.FindProductById(id);

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
                var response = await _productService.UpdateProduct(product);

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
            var product = await _productService.FindProductById(id);

            if(product != null ) 
            {
                return View(product);
            }

            return NotFound();
        }

        [HttpPost]
        [Authorize] /*(Roles = Role.Admin)*/
        public async Task<IActionResult> ProductDelete(ProductModel product)
        {
            var response = await _productService.DeleteProductById(product.Id);

            if (response)
            {
                return RedirectToAction("ProductIndex");
            }

            return View(product);
        }
    }
}