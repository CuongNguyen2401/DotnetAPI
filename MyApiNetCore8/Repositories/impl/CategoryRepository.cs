using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyApiNetCore8.DTO.Response;
using MyApiNetCore8.DTO.Request;
using MyApiNetCore8.Model;
using MyApiNetCore8.Data;



namespace MyApiNetCore8.Repository.impl
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly MyContext _context;
        private readonly IMapper _mapper;

        public CategoryRepository(MyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CategoryResponse> CreateCategoryAsync(CategoryRequest category)
        {
            var categoryEntity = _mapper.Map<Category>(category);
            _context.Category.Add(categoryEntity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CategoryResponse>(categoryEntity);
        }

        public async Task<List<CategoryResponse>> GetAllCategoriesAsync()
        {
            var categories = await _context.Category.ToListAsync();
            return _mapper.Map<List<CategoryResponse>>(categories);
        }

        public async Task<CategoryResponse> GetCategoryByIdAsync(long id)
        {
            var category = await _context.Category.FirstOrDefaultAsync(x => x.id == id);
            return _mapper.Map<CategoryResponse>(category);
        }

        public async Task<CategoryResponse> UpdateCategoryAsync(long id, CategoryRequest category)
        {
            var categoryEntity = _mapper.Map<Category>(category);
            categoryEntity.id = id;
            _context.Category.Update(categoryEntity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CategoryResponse>(categoryEntity);
        }

        public async Task<int> GetTotalProductQuantityByCategoryNameAsync(string categoryName)
        {
            var category = await _context.Category
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.name == categoryName);

            if (category == null)
            {
                return 0; // or handle accordingly
            }

            return category.Products.Count();
        }

    }
}
