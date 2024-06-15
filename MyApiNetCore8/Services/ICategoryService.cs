

using MyApiNetCore8.DTO.Response;
using MyApiNetCore8.DTO.Request;

namespace MyApiNetCore8.Repository
{
    public interface ICategoryService
    {
        Task<List<CategoryResponse>> GetAllCategoriesAsync();
        Task<CategoryResponse> GetCategoryByIdAsync(long id);
        Task<CategoryResponse> CreateCategoryAsync(CategoryRequest category);
        Task<CategoryResponse> UpdateCategoryAsync(UpdateCategoryRequest category);
        Task<int> GetTotalProductQuantityByCategoryNameAsync(string categoryName);

        Task DeteleteCategory(long id);

        

    }
}
