using AutoMapper;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.EntityFrameworkCore;
using MyApiNetCore8.Data;
using MyApiNetCore8.DTO.Request;
using MyApiNetCore8.DTO.Response;
using MyApiNetCore8.Model;
using MyApiNetCore8.Repository;
using MyApiNetCore8.Enums;

namespace MyApiNetCore8.Repository.impl
{
    public class ProductService : IProductService
    {


        private readonly MyContext _context;
        private readonly IMapper _mapper;
        private readonly Cloudinary _cloudinary;

        public ProductService(MyContext context, IMapper mapper, Cloudinary cloudinary)
        {
            _context = context;
            _mapper = mapper;
            _cloudinary = cloudinary;
        }

        public async Task<ProductResponse> CreateProductAsync(ProductRequest productRequest)
        {
            string imageUrl = null;
            if (productRequest.image != null)
            {
                imageUrl = await UploadImageAsync(productRequest.image, _cloudinary);
            }

            var productEntity = new Product
            {
                image = imageUrl,
                name = productRequest.name,
                description = productRequest.description,
                price = productRequest.price,
                salePrice = productRequest.salePrice,
                quantity = productRequest.quantity,
                category_id = productRequest.categoryId,
                status = Enums.Status.ACTIVE
            };

            _context.Product.Add(productEntity);
            await _context.SaveChangesAsync();

            productEntity.slug = CreateSlug(productEntity);
            _context.Product.Update(productEntity);
            await _context.SaveChangesAsync();


            productEntity = await _context.Product.Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.id == productEntity.id);

            return _mapper.Map<ProductResponse>(productEntity);
        }

        public async Task<List<ProductResponse>> FindSalesProduct()
        {
            var products = await _context.Product
                .Include(p => p.Category)
                .Where(p => p.salePrice < p.price)
                .ToListAsync();

            var sortedProducts = products
                .OrderByDescending(p => (p.price - p.salePrice) / p.price * 100)
                .ToList();

            return sortedProducts
                .Select(p => _mapper.Map<ProductResponse>(p))
                .ToList();
        }

        private string CreateSlug(Product product)
        {
            return $"{product.name.ToLower().Replace(" ", "-")}-{product.id}";
        }


        public async Task<string> UploadImageAsync(IFormFile image, Cloudinary cloudinary)
        {
            if (image == null || image.Length == 0)
            {
                throw new ArgumentException("Image file is null or empty");
            }

            try
            {
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(image.FileName, image.OpenReadStream())
                };

                var uploadResult = await cloudinary.UploadAsync(uploadParams);

                // Detailed logging
                if (uploadResult != null)
                {
                    Console.WriteLine($"Upload result: {uploadResult.JsonObj}");
                    if (uploadResult.SecureUrl != null)
                    {
                        Console.WriteLine($"Secure URL: {uploadResult.SecureUrl}");
                    }
                    else
                    {
                        Console.Error.WriteLine("Secure URL is null.");
                    }
                }
                else
                {
                    Console.Error.WriteLine("Upload result is null.");
                }

                if (uploadResult == null || uploadResult.SecureUrl == null)
                {
                    throw new InvalidOperationException("Failed to upload image to Cloudinary.");
                }

                return uploadResult.SecureUrl.ToString();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error uploading image: {ex.Message}");
                throw;
            }
        }


        public async Task DeleteProduct(long id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                throw new Exception($"Product with ID {id} not found.");
            }

            product.status = Enums.Status.INACTIVE;
            _context.Product.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ProductResponse>> FindLimitedProductsByCategory(string categoryName, int limit)
        {
            var products = await _context.Product
                  .Include(m => m.Category)
                  .Where(p => p.Category.name == categoryName)
                  .Take(limit)
                  .ToListAsync();
            return _mapper.Map<List<ProductResponse>>(products);
        }

        public async Task<List<ProductResponse>> FindByCategory(string categoryName)
        {
            var products = await _context.Product
                  .Include(m => m.Category)
                  .Where(p => p.Category.name == categoryName)
                  .ToListAsync();
            return _mapper.Map<List<ProductResponse>>(products);
        }

        public async Task<List<ProductResponse>> GetAllProductsAsync()
        {
            var products = await _context.Product
                .Include(m => m.Category)
                .ToListAsync();
            return _mapper.Map<List<ProductResponse>>(products);
        }

        public async Task<ProductResponse> GetProductByIdAsync(long id)
        {
            var product = await _context.Product
                .Include(m => m.Category)
                .FirstOrDefaultAsync(x => x.id == id);
            return _mapper.Map<ProductResponse>(product);
        }

        public async Task<ProductResponse> UpdateProductAsync(UpdateProductRequest product)
        {
            var productEntity = await _context.Product.FindAsync(product.id);
            if (productEntity == null)
            {
                return null;
            }

            productEntity.name = product.name;
            productEntity.description = product.description;
            productEntity.price = product.price;
            productEntity.salePrice = product.salePrice;
            productEntity.quantity = product.quantity;
            productEntity.category_id = product.categoryId;

            _context.Product.Update(productEntity);
            await _context.SaveChangesAsync();


            productEntity = await _context.Product.Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.id == productEntity.id);

            return _mapper.Map<ProductResponse>(productEntity);

        }
        public async Task<List<ProductResponse>> FindProductsByQueryString(string q)
        {
            return await _context.Product
                .Include(p => p.Category)
                .Where(p => p.name.Contains(q) || p.description.Contains(q) || p.Category.name.Contains(q))
                .Select(p => _mapper.Map<ProductResponse>(p))
                .ToListAsync();
        }

        public async Task<ProductResponse> GetProductBySlug(string slug)
        {
            var product = await _context.Product
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.slug == slug);

            return _mapper.Map<ProductResponse>(product);
        }

        public async Task<List<ProductResponse>> getListProductsByIds(List<long> ids)
        {
            var products = await _context.Product
                .Include(p => p.Category)
                .Where(p => ids.Contains(p.id))
                .ToListAsync();
            return _mapper.Map<List<ProductResponse>>(products);
        }

        public async Task<List<BestSellingProductResponse>> GetBestSellingProductsAsync()
        {
            var bestSellingProducts = await _context.OrderItem
                .Include(od => od.product)
                .ThenInclude(p => p.Category)
                .GroupBy(od => od.product)
                .Select(g => new BestSellingProductResponse
                {
                    Product = _mapper.Map<ProductResponse>(g.First().product),
                    TotalQuantitySold = g.Sum(od => od.quantity)
                })
                .OrderByDescending(p => p.TotalQuantitySold).ToListAsync();

            return bestSellingProducts;
        }

        public async Task<List<CategoryRevenueResponse>> GetTotalRevenueByCategoryAsync(DateTime startDate, DateTime endDate)
        {
            var categoryRevenues = await _context.OrderItem
                .Include(oi => oi.product)
                .ThenInclude(p => p.Category)
                .Where(oi => oi.Order.CreatedDate >= startDate && oi.Order.CreatedDate <= endDate)
                .GroupBy(oi => oi.product.Category)
                .Select(g => new CategoryRevenueResponse
                {
                    categoryName = g.Key.name,
                    totalRevenue = g.Sum(oi => oi.price * oi.quantity)
                })
                .ToListAsync();

            return categoryRevenues;
        }
    }
}