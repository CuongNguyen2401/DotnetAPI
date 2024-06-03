using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyApiNetCore8.Data;
using MyApiNetCore8.DTO.Request;
using MyApiNetCore8.DTO.Response;
using MyApiNetCore8.Model;
using MyApiNetCore8.Repository;

namespace MyApiNetCore8.Repository.impl
{
    public class ProductRepository : IProductRepository
    {
        private readonly MyContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(MyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductResponse> CreateProductAsync(ProductRequest product)
        {
            var productEntity = _mapper.Map<Product>(product);

            _context.Product.Add(productEntity);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductResponse>(productEntity);
        }

        public void DeleteProduct(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ProductResponse>> GetAllProductsAsync()
        {
            var products = await _context.Product
                .Include(m=>m.Category)
                .ToListAsync();
            return _mapper.Map<List<ProductResponse>>(products);
        }

        public async Task<ProductResponse> GetProductByIdAsync(long id)
        {
            var product = await _context.Product
                .Include (m=>m.Category)
                .FirstOrDefaultAsync(x => x.id == id);
            return _mapper.Map<ProductResponse>(product);
        }

        public async Task<ProductResponse> UpdateProductAsync(long id, ProductRequest product)
        {
            var productEntity = _mapper.Map<Product>(product);
            productEntity.id = id;
            _context.Product.Update(productEntity);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductResponse>(productEntity);
        }
    }
}
