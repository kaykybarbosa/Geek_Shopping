using GeekShopping.Dtos.Request;
using GeekShopping.Dtos.Request.Update;
using GeekShopping.Interfaces;
using GeekShopping.ProductApi.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductController : ControllerBase
    {
        private IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        [AllowAnonymous]
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

        [HttpGet("find-product/{id}")]
        public async Task<IActionResult> FindById(long id)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.FindProductById(id);

                if(result != null)
                    return Ok(result);
                
                return NotFound();
            }

            return StatusCode(500);

        }

        [HttpPost("create-product")]
        public async Task<IActionResult> Create([FromBody] ProductRequest productDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.CreateProduct(productDto);

                if(result != null)
                    return Ok(result);

                return BadRequest();
            }

            return StatusCode(500);
        }

        [HttpPut("update-product")]
        public async Task<IActionResult> Update([FromBody] ProductRequestUpdate productUpdateDto)
        {
            if (ModelState.IsValid)
            {
                var productExist = await _productService.FindProductById(productUpdateDto.Id);
                
                if (productExist != null )
                {
                    var result = await _productService.UpdateProduct(productUpdateDto);

                    if (result != null)
                        return Ok(result);
                }

                return BadRequest();
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

                return BadRequest();
            }

            return StatusCode(500);
        }
    }
}