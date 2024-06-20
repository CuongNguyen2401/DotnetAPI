using MyApiNetCore8.DTO.Request;
using MyApiNetCore8.DTO.Response;
using MyApiNetCore8.Enums;

namespace MyApiNetCore8.Services;

public interface IOrderService
{
    Task<OrderResponse> CreateOrder(OrderRequest orderRequest);

    Task<List<OrderResponse>> GetAllOrdersAsync();

    Task<List<OrderResponse>> GetAllOrdersByUserAsync(string userId);

    Task<OrderResponse> GetOrderAsync(long orderId);

    Task<List<MonthlySalesOrderResponse>> GetMonthlySalesUpToCurrentMonthAsync();
    Task<int> GetOrdersSoldTodayAsync();

    Task<double> GetMonthlySalesRevenueAsync();

    Task<double> GetYearlySalesRevenueAsync();

    Task<OrderResponse> UpdateOrderStatusAsync(long orderId, OrderStatus status);
}