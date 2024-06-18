
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApiNetCore8.Data;
using MyApiNetCore8.DTO.Request;
using MyApiNetCore8.DTO.Response;
using MyApiNetCore8.Helper;
using MyApiNetCore8.Model;
using MyApiNetCore8.Repository;

namespace MyApiNetCore8.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {

        private readonly ICategoryService _categoryService;

        public CategoriesController(MyContext context, ICategoryService categoryService)
        {

            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<CategoryResponse>>>> GetCategory()
        {
            return new ApiResponse<List<CategoryResponse>>(200, "Success", await _categoryService.GetAllCategoriesAsync());

        }

        [HttpPost]
        [Authorize(Roles = AppRole.Admin)]
        public async Task<ActionResult<Category>> PostCategory(CategoryRequest category)
        {
            var categoryResponse = await _categoryService.CreateCategoryAsync(category);
            return CreatedAtAction("GetCategory", new ApiResponse<CategoryResponse>(200, "Success", categoryResponse));

        }

        [HttpGet("{categoryName}/products/count")]
        public async Task<ActionResult<ApiResponse<int>>> GetTotalProductQuantity(string categoryName)
        {
            int totalQuantity = await _categoryService.GetTotalProductQuantityByCategoryNameAsync(categoryName);
            return Ok(new ApiResponse<int>(200, "Success", totalQuantity));
        }

    }
}
