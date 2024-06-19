
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApiNetCore8.Data;
using MyApiNetCore8.DTO.Request;
using MyApiNetCore8.DTO.Response;
using MyApiNetCore8.Enums;
using MyApiNetCore8.Helper;
using MyApiNetCore8.Model;
using MyApiNetCore8.Repository;
using MyApiNetCore8.Repository.impl;

namespace MyApiNetCore8.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {

        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {

            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<CategoryResponse>>>> GetAllCategory()
        {
            return new ApiResponse<List<CategoryResponse>>(1000, "Success", await _categoryService.GetAllCategoriesAsync());

        }

        [HttpPost]
        [Authorize(Roles = AppRole.Admin)]
        public async Task<ActionResult<ApiResponse<CategoryResponse>>> PostCategory(CategoryRequest category)
        {
            var categoryResponse = await _categoryService.CreateCategoryAsync(category);
            return Ok(new ApiResponse<CategoryResponse>(1000, "Success", categoryResponse));

        }

        [HttpGet("{categoryName}/products/count")]
        public async Task<ActionResult<ApiResponse<int>>> GetTotalProductQuantity(string categoryName)
        {
            int totalQuantity = await _categoryService.GetTotalProductQuantityByCategoryNameAsync(categoryName);
            return Ok(new ApiResponse<int>(1000, "Success", totalQuantity));
        }

        [HttpDelete]
        [Authorize(Roles = AppRole.Admin)]
        public async Task<ActionResult<ApiResponse<string>>> DeleteCategory([FromQuery] long[] ids)
        {
            if (ids.Length == 0)
            {
                return BadRequest(new ApiResponse<string>(1001, "Error", "Please provide at least one category id."));
            }

            foreach (var id in ids)
            {
                await _categoryService.DeteleteCategory(id);
            }

            return Ok(new ApiResponse<string>(1000, "Success", "Categories deleted successfully."));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<CategoryResponse>>> GetCategoryById(long id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(new ApiResponse<CategoryResponse>(1000, "Success", category));
        }

        [HttpPut]
        [Authorize(Roles = AppRole.Admin)]
        public async Task<ActionResult<ApiResponse<CategoryResponse>>> UpdateCategoryAsync([FromBody]UpdateCategoryRequest category)
        {
            if (!Enum.TryParse(category.status.ToString(), true, out Status status))
            {
                return BadRequest("Invalid status value.");
            }

            var updatedCategory = await _categoryService.UpdateCategoryAsync(category);
            return Ok(new ApiResponse<CategoryResponse>(1000, "Success", updatedCategory));
        }







    }
}
