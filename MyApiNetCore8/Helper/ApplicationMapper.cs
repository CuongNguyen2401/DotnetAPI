using AutoMapper;
using MyApiNetCore8.DTO.Request;
using MyApiNetCore8.DTO.Response;
using MyApiNetCore8.Model;

namespace MyApiNetCore8.Helper
{
    public class ApplicationMapper : Profile
    {
        //CreateMap<ProductRequest, Product>()
        //    .ForMember(dest => dest.Category, opt => opt.Ignore());
        public ApplicationMapper()
        {
            CreateMap<Product, ProductResponse>()
            .ForMember(dest => dest.category, opt => opt.MapFrom(src => src.Category))
            .ForMember(dest => dest.productStatus, opt => opt.MapFrom(src => src.status))
            .ReverseMap()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.category))
            .ForMember(dest => dest.status, opt => opt.MapFrom(src => src.productStatus));


            CreateMap<ProductRequest, Product>().ReverseMap();

            CreateMap<CategoryRequest, Category>().ReverseMap();
            CreateMap<Category, CategoryResponse>().ReverseMap();

            CreateMap<Coupon, CouponResponse>().ReverseMap();
            CreateMap<CouponRequest, Coupon>().ReverseMap();

            CreateMap<OrderRequest, Order>().ReverseMap();
            CreateMap<Order, OrderResponse>().ReverseMap();
            CreateMap<OrderItemRequest, OrderItem>().ReverseMap();
            CreateMap<OrderItem, OrderItemResponse>().ReverseMap();

            CreateMap<User, AccountResponse>()
                .ForMember(dest => dest.email, opt => opt.MapFrom(src => src.Email))
                .ReverseMap();


        }
    }
}
