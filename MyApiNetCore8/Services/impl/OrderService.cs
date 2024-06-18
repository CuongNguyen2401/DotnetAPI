using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyApiNetCore8.Data;
using MyApiNetCore8.DTO.Request;
using MyApiNetCore8.DTO.Response;
using MyApiNetCore8.Enums;
using MyApiNetCore8.Model;

namespace MyApiNetCore8.Services.impl;

public class OrderService : IOrderService
{
    private readonly UserManager<User> _userManager;
    private readonly MyContext _context;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;


    public OrderService(UserManager<User> userManager, MyContext context, IMapper mapper,
        IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
        _context = context;
        _mapper = mapper;
    }


    public async Task<OrderResponse> CreateOrder(OrderRequest orderRequest)
    {
        var savedOrder = _mapper.Map<Order>(orderRequest);
        var username = _httpContextAccessor.HttpContext.User.Identity.Name;
        var user = await _userManager.FindByNameAsync(username);

        double totalPay = ProcessOrderItems(savedOrder, orderRequest.OrderItems);

        // if (!string.IsNullOrEmpty(orderRequest.CouponCode))
        // {
        //  var coupon = _context.Coupons.FirstOrDefault(c => c.Code == orderRequest.CouponCode);
        //     if (coupon == null)
        //     {
        //         throw new Exception("Coupon not found");
        //     }
        //     totalPay -= totalPay * coupon.Discount;
        //     coupon.quantity -= 1;
        //     savedOrder.c = coupon;
        //     _couponRepository.Update(coupon);
        //     _couponRepository.DeleteUserCouponsByCouponIds(new List<int> { coupon.Id });
        // }

        savedOrder.totalPay = totalPay;
        savedOrder.orderStatus = OrderStatus.PENDING;
        savedOrder.user = user;

        _context.Order.Add(savedOrder);
         await _context.SaveChangesAsync();

        return _mapper.Map<OrderResponse>(savedOrder);
    }

    public async Task<List<OrderResponse>> GetAllOrdersByUserAsync(string userId)
    {
        var orders = await _context.Order.Where(o => o.user.Id == userId).ToListAsync();
        return _mapper.Map<List<OrderResponse>>(orders);
    }

    public async Task<OrderResponse> GetOrderAsync(long orderId)
    {
        var username = _httpContextAccessor.HttpContext.User.Identity.Name;
        var user = await _userManager.FindByNameAsync(username);
        var order = await _context.Order
            .Include(o => o.orderItems)
            .ThenInclude(oi => oi.product)
            .FirstOrDefaultAsync(o => o.id == orderId && o.user.Id == user.Id);
        return _mapper.Map<OrderResponse>(order);
    }

    public async Task<List<OrderResponse>> GetAllOrdersAsync()
    {
        var orders = await _context.Order.ToListAsync();
        return _mapper.Map<List<OrderResponse>>(orders);
    }

    private double ProcessOrderItems(Order savedOrder, List<OrderItemRequest> orderItemRequests)
    {
        double totalPay = 0;
        var orderItems = new HashSet<OrderItem>();

        foreach (var orderItemRequest in orderItemRequests)
        {
            var orderItem = _mapper.Map<OrderItem>(orderItemRequest);
            var product = _context.Product.FirstOrDefault(p => p.id == orderItem.productId);

            if (product == null)
            {
                throw new Exception("Product not found");
            }

            if (orderItem.quantity > product.quantity)
            {
                throw new Exception("Product quantity not enough");
            }

            product.quantity -= orderItem.quantity;
            orderItem.product = product;
            orderItem.Order = savedOrder;
            orderItems.Add(orderItem);
            totalPay += CalculateItemTotal(orderItem);
        }

        savedOrder.orderItems = orderItems;
        return totalPay;
    }

    private double CalculateItemTotal(OrderItem orderItem)
    {
        return orderItem.quantity * orderItem.price;
    }
}