using MyApiNetCore8.DTO.Request;
using MyApiNetCore8.DTO.Response;
using MyApiNetCore8.Model;
using System.Runtime.InteropServices;

namespace MyApiNetCore8.Repository
{
    public interface IProductRepository
    {
        Task<List<ProductResponse>> GetAllProductsAsync();
        Task<ProductResponse> GetProductByIdAsync(long id);
        Task<ProductResponse> CreateProductAsync(ProductRequest product);
        Task<ProductResponse> UpdateProductAsync(long id, ProductRequest product);


    }
}
