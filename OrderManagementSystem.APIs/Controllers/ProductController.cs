using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.APIs.Errors;
using OrderManagementSystem.Core.DataTransferObjects;
using OrderManagementSystem.Core.Entites;
using OrderManagementSystem.Core.Services;

namespace OrderManagementSystem.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;


        public ProductController(IProductService productService)
        {
            _productService = productService;
        
        }

        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<Product>> AddProduct([FromBody] ProductCreateDto productCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return Unauthorized("You need To login as Admin");
            }

            var createdProduct = await _productService.AddProductAsync(productCreateDto);
           
            return Ok(createdProduct);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductById(int productId)
        {
            var product = await _productService.GetProductByIdAsync(productId);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpGet]
        public async Task<ActionResult<Product>> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpPut("{productId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Product>> UpdateProduct(int productId, [FromBody] ProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse(404,"Not Found") );
            }
        

            if (productId != productDto.ProductId)
            {
                return BadRequest(new ApiResponse(401,$"No product with id {productId}"));
            }

            var updatedProduct = await _productService.UpdateProductAsync(productDto);
            return Ok(updatedProduct);
        }



    }
}
