using MyApiNetCore8.DTO.Request;
using MyApiNetCore8.DTO.Response;

namespace MyApiNetCore8.Services;

public interface IOrderService
{
    Task<OrderResponse> CreateOrder(OrderRequest orderRequest);

    Task<List<OrderResponse>> GetAllOrdersAsync();

    Task<List<OrderResponse>> GetAllOrdersByUserAsync(string userId);

    Task<OrderResponse> GetOrderAsync(long orderId);

    Task<List<MonthlySalesOrderResponse>> GetMonthlySalesUpToCurrentMonthAsync();
    Task<int> GetOrdersSoldTodayAsync();
}