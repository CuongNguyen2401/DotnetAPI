
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApiNetCore8.Data;
using MyApiNetCore8.DTO.Request;
using MyApiNetCore8.DTO.Response;
using MyApiNetCore8.Model;
using MyApiNetCore8.Repository;
using System.Text;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using MyApiNetCore8.Helper;

namespace MyApiNetCore8.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly IProductService _productService;
        private readonly Cloudinary _cloudinary;


        public ProductsController(IProductService productService, Cloudinary cloudinary)
        {
            _productService = productService;
            _cloudinary = cloudinary;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<ProductResponse>>>> GetProduct()
        {
            return new ApiResponse<List<ProductResponse>>(1000, "Success", await _productService.GetAllProductsAsync());

        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        [Authorize]  
        public async Task<ActionResult<ApiResponse<ProductResponse>>> GetProduct(long id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return new ApiResponse<ProductResponse>(1000, "Success", product);
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        [Authorize(Roles = AppRole.Admin)]
        public async  Task<ActionResult<ApiResponse<ProductResponse>>>  PutProduct([FromForm] UpdateProductRequest product)
        {
            var productResponse = await _productService.UpdateProductAsync(product);
            if (productResponse == null)
            {
                return NotFound();
            }
            return Ok(new ApiResponse<ProductResponse>(1000, "Success", productResponse));
        }

   
        [HttpPost]
        [Authorize(Roles = AppRole.Admin)]
        public async Task<ActionResult<ApiResponse<ProductResponse>>> PostProduct([FromForm] ProductRequest productRequest)
        {
            if (productRequest == null)
            {
                return BadRequest("Product request is null.");
            }

            try
            {
                var productResponse = await _productService.CreateProductAsync(productRequest);
                return 
                    CreatedAtAction("GetProduct", 
                        new { id = productResponse.id }, 
                        new ApiResponse<ProductResponse>(1000, "Success", productResponse));
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error creating product: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
        
        [HttpGet("category/{categoryName}")]
        public async Task<ActionResult<ApiResponse<List<ProductResponse>>>> GetProductsByCategory(string categoryName, int? limit)
        {
            if (limit.HasValue)
            {
                var limitedProducts = await _productService.FindLimitedProductsByCategory(categoryName, limit.Value);
                return Ok(new ApiResponse<List<ProductResponse>>(1000, "Success", limitedProducts));
            }
            else
            {
                var products = await _productService.FindByCategory(categoryName);
                return Ok(new ApiResponse<List<ProductResponse>>(1000, "Success", products));
            }
        }


        [HttpDelete]
        [Authorize(Roles = AppRole.Admin)]
        public async Task<ActionResult<ApiResponse<string>>> DeleteProduct([FromQuery] long[] ids)
        {
           

            foreach (var id in ids)
            {
                    await _productService.DeleteProduct(id);
            }

            

            return Ok(new ApiResponse<string>(1000, "Success", "Products deleted successfully."));
        }

        private bool ProductExists(long id)
        {
            var product = _productService.GetProductByIdAsync(id);
            return product != null;
        }
        
        [HttpGet("sales")]
        public async Task<ActionResult<ApiResponse<List<ProductResponse>>>> GetSalesProduct()
        {
            var products = await _productService.FindSalesProduct();
            return Ok(new ApiResponse<List<ProductResponse>>(1000, "Success", products));
        }
        
        [HttpGet("search")]
        public async Task<ActionResult<ApiResponse<List<ProductResponse>>>> SearchProducts([FromQuery] string q)
        {
            var products = await _productService.FindProductsByQueryString(q);
            return Ok(new ApiResponse<List<ProductResponse>>(1000, "Success", products));
        }
        
        [HttpGet("detail/{slug}")]
        public async Task<ActionResult<ApiResponse<ProductResponse>>> getProductBySlug(string slug)
        {
            var product = await _productService.GetProductBySlug(slug);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(new ApiResponse<ProductResponse>(1000, "Success", product));
        }

        [HttpGet("list")]
        public async Task<ApiResponse<List<ProductResponse>>> GetProductsByIds([FromQuery] List<long> ids)
        {
            var products = await _productService.getListProductsByIds(ids);
            return new ApiResponse<List<ProductResponse>>(1000, "Success", products);
        }

    }
    
    
    
}
