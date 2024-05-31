

using MyApiNetCore8.DTO.Response;
using MyApiNetCore8.DTO.Request;

namespace MyApiNetCore8.Repository
{
    public interface ICategoryRepository
    {
        Task<List<CategoryResponse>> GetAllCategoriesAsync();
        Task<CategoryResponse> GetCategoryByIdAsync(long id);
        Task<CategoryResponse> CreateCategoryAsync(CategoryRequest category);
        Task<CategoryResponse> UpdateCategoryAsync(long id, CategoryRequest category);
        Task<int> GetTotalProductQuantityByCategoryNameAsync(string categoryName);

    }
}
