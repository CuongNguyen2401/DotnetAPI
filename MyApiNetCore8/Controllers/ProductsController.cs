
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

namespace MyApiNetCore8.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly IProductRepository _productRepository;
        private readonly Cloudinary _cloudinary;


        public ProductsController(IProductRepository productRepository, Cloudinary cloudinary)
        {
            _productRepository = productRepository;
            _cloudinary = cloudinary;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<ProductResponse>>>> GetProduct()
        {
            return new ApiResponse<List<ProductResponse>>(1000, "Success", await _productRepository.GetAllProductsAsync());

        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<ProductResponse>>> GetProduct(long id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return new ApiResponse<ProductResponse>(1000, "Success", product);
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutProduct( ProductRequest product)
        {
            var productResponse = await _productRepository.UpdateProductAsync(product);
            if (productResponse == null)
            {
                return NotFound();
            }
            return Ok(new ApiResponse<ProductResponse>(1000, "Success", productResponse));
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductResponse>> PostProduct([FromForm] ProductRequest productRequest)
        {
            if (productRequest == null)
            {
                return BadRequest("Product request is null.");
            }

            try
            {
                var productResponse = await _productRepository.CreateProductAsync(productRequest);
                return CreatedAtAction("GetProduct", new { id = productResponse.id }, new ApiResponse<ProductResponse>(1000, "Success", productResponse));
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error creating product: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }





        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(long id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _productRepository.DeleteProduct(id);

            return Ok(new ApiResponse<ProductResponse>(1000, "Success", product));

        }

        private bool ProductExists(long id)
        {
            var product = _productRepository.GetProductByIdAsync(id);
            return product != null;
        }
    }
}
