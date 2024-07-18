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
        private readonly IMapper _mapper;


        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
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
            var mappedProduct = _mapper.Map<Product>(products);
            return Ok(mappedProduct);
        }

        [HttpPut("{productId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Product>> UpdateProduct(int productId, [FromBody] ProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse(404,"Not Found") );
            }
        

           
               

            var updatedProduct = await _productService.UpdateProductAsync(productDto);
            if (updatedProduct is null)
            {
                return BadRequest(new ApiResponse(401, $"No product with id {productId}"));

            }
            var mappedProduct=_mapper.Map<Product>(updatedProduct); 
            return Ok(mappedProduct);
        }



    }
}
