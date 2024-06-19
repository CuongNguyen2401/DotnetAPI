using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyApiNetCore8.DTO.Response;
using MyApiNetCore8.DTO.Request;
using MyApiNetCore8.Model;
using MyApiNetCore8.Data;



namespace MyApiNetCore8.Repository.impl
{
    public class CategoryService : ICategoryService
    {
        private readonly MyContext _context;
        private readonly IMapper _mapper;

        public CategoryService(MyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CategoryResponse> CreateCategoryAsync(CategoryRequest category)
        {
            var categoryEntity = _mapper.Map<Category>(category);
            categoryEntity.status = Enums.Status.ACTIVE;
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

        public async Task<CategoryResponse> UpdateCategoryAsync(UpdateCategoryRequest category)
        {
            var categoryEntity = await _context.Category.FindAsync(category.id);

            categoryEntity.name = category.name;
            categoryEntity.description = category.description;
            categoryEntity.status = category.status;

            _context.Category.Update(categoryEntity);
            await _context.SaveChangesAsync();

            if (category.status == Enums.Status.INACTIVE)
            {
                await SetProductsInactiveAsync(categoryEntity);
            }

            return _mapper.Map<CategoryResponse>(categoryEntity);
        }

        private async Task SetProductsInactiveAsync(Category category)
        {
            var productsToUpdate = await _context.Product
                                                .Where(p => p.category_id == category.id)
                                                .ToListAsync();

            foreach (var product in productsToUpdate)
            {
                product.status = Enums.Status.INACTIVE;
                _context.Product.Update(product);
            }

            await _context.SaveChangesAsync();
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

        public async Task DeteleteCategory(long id)
        {
            var category = await _context.Category.FirstOrDefaultAsync(c => c.id == id);

            if (category != null)
            {
               
                category.status = Enums.Status.INACTIVE;
                _context.Category.Update(category);

              
                var productsToUpdate = await _context.Product
                    .Where(p => p.category_id == id)
                    .ToListAsync();

                foreach (var product in productsToUpdate)
                {
                    product.status = Enums.Status.INACTIVE;
                    _context.Product .Update(product);
                }

                await _context.SaveChangesAsync();
            }
        }
    }
}
