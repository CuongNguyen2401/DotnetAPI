using MyApiNetCore8.DTO.Response;
using System.Runtime.InteropServices;

namespace MyApiNetCore8.Service
{
    public interface IProductService
    {
        List<ProductResponse> findAll();

        List<ProductResponse> findByCategory(String categoryName);

        List<ProductResponse> find5ProductsByCategory(String categoryName);

        List<ProductResponse> findSalesProduct();

        ProductResponse findById(long id);
    }
}
