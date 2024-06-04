using AutoMapper;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
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
        private readonly Cloudinary _cloudinary;

        public ProductRepository(MyContext context, IMapper mapper, Cloudinary cloudinary)
        {
            _context = context;
            _mapper = mapper;
            _cloudinary= cloudinary;
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
                sale_price = productRequest.salePrice,
                quantity = productRequest.quantity,
                category_id = productRequest.categoryId
            };

            _context.Product.Add(productEntity);
            await _context.SaveChangesAsync();

   
            return _mapper.Map<ProductResponse>(productEntity);
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






        public void DeleteProduct(long id)
        {
            var product = _context.Product.Find(id);
            _context.Product.Remove(product);
            _context.SaveChanges();
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

        public async Task<ProductResponse> UpdateProductAsync( ProductRequest product)
        {
           var productEntity = await _context.Product.FindAsync(product.id);
            if (productEntity == null)
            {
                return null;
            }

            productEntity.name = product.name;
            productEntity.description = product.description;
            productEntity.price = product.price;
            productEntity.sale_price = product.salePrice;
            productEntity.quantity = product.quantity;
            productEntity.category_id = product.categoryId;

            _context.Product.Update(productEntity);
            await _context.SaveChangesAsync();

            return _mapper.Map<ProductResponse>(productEntity);
        }


    }
}
