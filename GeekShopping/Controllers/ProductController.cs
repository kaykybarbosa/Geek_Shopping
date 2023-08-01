using GeekShopping.Dtos.Request.Update;
using GeekShopping.Dtos.Request;
using GeekShopping.Dtos.Response;
using GeekShopping.Interfaces;
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

        [HttpGet]
        public async Task<IActionResult> FindAll()
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.FindAllProducts();

                if (result.IsSuccess)
                    return Ok(result);


                return BadRequest(result);
            }

            return StatusCode(500);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindById(long id)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.FindProductById(id);

                if (result.IsSuccess)
                    return Ok(result);

                return BadRequest(result);
            }

            return StatusCode(500);

        }

        [HttpPost]
        public async Task<ActionResult<ProductResponse>> Create([FromBody] ProductRequest productDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.CreateProduct(productDto);

                if (result.IsSuccess)
                    return Ok(result);

                return BadRequest(result);
            }

            return StatusCode(500);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ProductRequestUpdate productUpdateDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.UpdateProduct(productUpdateDto);

                if (result.IsSuccess)
                    return Ok(result);

                return BadRequest(result);
            }

            return StatusCode(500);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.DeleteProduct(id);

                if (result.IsSuccess)
                    return Ok(result);

                return BadRequest(result);
            }

            return StatusCode(500);
        }
    }
}