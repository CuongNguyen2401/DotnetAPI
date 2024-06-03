
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApiNetCore8.Data;
using MyApiNetCore8.DTO.Request;
using MyApiNetCore8.DTO.Response;
using MyApiNetCore8.Model;
using MyApiNetCore8.Repository;

namespace MyApiNetCore8.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly IProductRepository _productRepository;

        public ProductsController(MyContext context, IProductRepository productRepository)
        {

            _productRepository = productRepository;
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
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(long id, ProductRequest product)
        {
            var productResponse = await _productRepository.UpdateProductAsync(id, product);
            if (productResponse == null)
            {
                return NotFound();
            }
            return Ok(new ApiResponse<ProductResponse>(1000, "Success", productResponse));
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct([FromBody] ProductRequest product)
        {
           
            var productResponse = await _productRepository.CreateProductAsync(product);
            return CreatedAtAction("GetProduct", new ApiResponse<ProductResponse>(1000, "Success", productResponse));

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
            return Ok(new ApiResponse<ProductResponse>(1000, "Success", product));
        }

        private bool ProductExists(long id)
        {
            var product = _productRepository.GetProductByIdAsync(id);
            return product != null;
        }
    }
}
