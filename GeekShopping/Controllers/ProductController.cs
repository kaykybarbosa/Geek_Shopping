using GeekShopping.Dtos.Request;
using GeekShopping.Dtos.Request.Update;
using GeekShopping.Dtos.Response;
using GeekShopping.Interfaces;
using GeekShopping.ProductApi.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }
        
        [HttpGet("all-products")]
        public async Task<IActionResult> FindAll()
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.FindAllProducts();

                return Ok(result);
            }

            return StatusCode(500);

        }

        [Authorize]
        [HttpGet("find-product/{id}")]
        public async Task<IActionResult> FindById(long id)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.FindProductById(id);

                return Ok(result);

            }

            return StatusCode(500);

        }

        [Authorize]
        [HttpPost("create-product")]
        public async Task<ActionResult<ProductResponse>> Create([FromBody] ProductRequest productDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.CreateProduct(productDto);

                return Ok(result);

            }

            return StatusCode(500);
        }

        [Authorize]
        [HttpPut("update-product")]
        public async Task<IActionResult> Update([FromBody] ProductRequestUpdate productUpdateDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.UpdateProduct(productUpdateDto);

                return Ok(result);

            }

            return StatusCode(500);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpDelete("delete-product/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.DeleteProduct(id);

                if (result)
                    return Ok(result);

                return BadRequest(result);
            }

            return StatusCode(500);
        }
    }
}