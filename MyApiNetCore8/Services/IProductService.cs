using MyApiNetCore8.DTO.Request;
using MyApiNetCore8.DTO.Response;
using MyApiNetCore8.Model;
using System.Runtime.InteropServices;

namespace MyApiNetCore8.Repository
{
    public interface IProductService
    {
        Task<List<ProductResponse>> GetAllProductsAsync();
        Task<ProductResponse> GetProductByIdAsync(long id);
        Task<ProductResponse> CreateProductAsync(ProductRequest productRequest);
        Task<ProductResponse> UpdateProductAsync(UpdateProductRequest product);
        Task DeleteProduct(long id);
        Task<List<ProductResponse>> FindLimitedProductsByCategory(string categoryName, int limit);
        Task<List<ProductResponse>> FindByCategory(string categoryName);
        
       Task<List<ProductResponse>> FindSalesProduct();

       Task<List<ProductResponse>> FindProductsByQueryString(string q);
       
       Task<ProductResponse> GetProductBySlug(string slug);

       Task<List<ProductResponse>> getListProductsByIds(List<long> ids);


    }
}