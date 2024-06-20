using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyApiNetCore8.DTO.Request;
using MyApiNetCore8.DTO.Response;
using MyApiNetCore8.Enums;
using MyApiNetCore8.Helper;
using MyApiNetCore8.Model;
using MyApiNetCore8.Services;

namespace MyApiNetCore8.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;



        public OrdersController(IOrderService orderService, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
        {
            _orderService = orderService;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        [HttpPost]
        [Authorize(Roles = AppRole.User)]
        public async Task<ActionResult<ApiResponse<OrderResponse>>> CreateOrder(OrderRequest orderRequest)
        {
            var orderResponse = await _orderService.CreateOrder(orderRequest);
            return Ok(new ApiResponse<OrderResponse>(1000, "Success", orderResponse));
        }


        [HttpGet]
        [Authorize(Roles = AppRole.Admin)]
        public async Task<ActionResult<ApiResponse<List<OrderResponse>>>> GetAllOrders()
        {
            var orderResponses = await _orderService.GetAllOrdersAsync();
            return Ok(new ApiResponse<List<OrderResponse>>(1000, "Success", orderResponses));
        }

        [HttpGet("{orderId}")]
        [Authorize(Roles = AppRole.User)]
        public async Task<ActionResult<ApiResponse<OrderResponse>>> GetOrder(long orderId)
        {
            var orderResponse = await _orderService.GetOrderAsync(orderId);
            return Ok(new ApiResponse<OrderResponse>(1000, "Success", orderResponse));
        }



        [HttpGet("user")]
        [Authorize(Roles = AppRole.User)]
        public async Task<ActionResult<ApiResponse<List<OrderResponse>>>> GetAllOrdersByUser()
        {
            var username = _httpContextAccessor.HttpContext.User.Identity.Name;
            var user = await _userManager.FindByNameAsync(username);
            var orderResponses = await _orderService.GetAllOrdersByUserAsync(user.Id);
            return Ok(new ApiResponse<List<OrderResponse>>(1000, "Success", orderResponses));
        }

        [HttpGet("monthly-sales")]
        [Authorize(Roles = AppRole.Admin)]
        public async Task<ActionResult<ApiResponse<List<MonthlySalesOrderResponse>>>> GetMonthlySales()
        {
            var monthlySales = await _orderService.GetMonthlySalesUpToCurrentMonthAsync();
            return Ok(new ApiResponse<List<MonthlySalesOrderResponse>>(1000, "Success", monthlySales));
        }

        [HttpGet("daily-sales")]
        [Authorize(Roles = AppRole.Admin)]
        public async Task<ActionResult<ApiResponse<int>>> GetOrdersSoldToday()
        {
            var ordersSoldToday = await _orderService.GetOrdersSoldTodayAsync();
            return Ok(new ApiResponse<int>(1000, "Success", ordersSoldToday));
        }

        [HttpGet("monthly-sales-revenue")]
        [Authorize(Roles = AppRole.Admin)]
        public async Task<ActionResult<ApiResponse<double>>> GetMonthlySalesRevenue()
        {
            var revenue = await _orderService.GetMonthlySalesRevenueAsync();

            return Ok(new ApiResponse<double>(1000, "Success", revenue));
        }

        [HttpGet("yearly-sales-revenue")]
        [Authorize(Roles = AppRole.Admin)]
        public async Task<ActionResult<ApiResponse<double>>> GetYearlySalesRevenue()
        {
            var revenue = await _orderService.GetYearlySalesRevenueAsync();

            return Ok(new ApiResponse<double>(1000, "Success", revenue));
        }

        [HttpPut("{orderId}/status")]
        public async Task<ActionResult<ApiResponse<OrderResponse>>> UpdateOrderStatus(long orderId, [FromQuery] OrderStatus status)
        {
            var orderResponse = await _orderService.UpdateOrderStatusAsync(orderId, status);

            if (orderResponse == null)
            {
                return NotFound(new ApiResponse<string>(1000, "Order not found", null));
            }

            return Ok(new ApiResponse<OrderResponse>(1000, "Success", orderResponse));
        }
    }
}

