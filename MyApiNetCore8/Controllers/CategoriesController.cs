
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using MyApiNetCore8.DTO.Request;
using MyApiNetCore8.DTO.Response;
using MyApiNetCore8.Model;
using MyApiNetCore8.Repository;

namespace MyApiNetCore8.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {

        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(MyContext context, ICategoryRepository categoryRepository)
        {

            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<CategoryResponse>>>> GetCategory()
        {
            return new ApiResponse<List<CategoryResponse>>(200, "Success", await _categoryRepository.GetAllCategoriesAsync());

        }

        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(CategoryRequest category)
        {
            var categoryResponse = await _categoryRepository.CreateCategoryAsync(category);
            return CreatedAtAction("GetCategory", new ApiResponse<CategoryResponse>(200, "Success", categoryResponse));

        }

        [HttpGet("{categoryName}/products/count")]
        public async Task<ActionResult<ApiResponse<int>>> GetTotalProductQuantity(string categoryName)
        {
            int totalQuantity = await _categoryRepository.GetTotalProductQuantityByCategoryNameAsync(categoryName);
            return Ok(new ApiResponse<int>(200, "Success", totalQuantity));
        }

    }
}
