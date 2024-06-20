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
            //Product Map
            CreateMap<Product, ProductResponse>()
                .ForMember(dest => dest.category, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.productStatus, opt => opt.MapFrom(src => src.status))
                    .ReverseMap()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.category))
                .ForMember(dest => dest.status, opt => opt.MapFrom(src => src.productStatus));
            CreateMap<ProductRequest, Product>().ReverseMap();
            //Product Map

            //Category Map
            CreateMap<Category, CategoryRequest>().ReverseMap();
            CreateMap<Category, CategoryResponse>().ReverseMap();
            //Category Map

            //Coupon Map
            CreateMap<Coupon, CouponResponse>().ReverseMap();
            CreateMap<Coupon, CouponRequest>().ReverseMap();
            //Coupon Map

            //Order Map
            CreateMap<Order, OrderRequest>().ReverseMap();
            CreateMap<Order, OrderResponse>().ReverseMap();
            //Order Map

            //OrderItem Map
            CreateMap<OrderItem, OrderItemRequest>().ReverseMap();
            CreateMap<OrderItem, OrderItemResponse>()
                .ForMember(dest => dest.productName, opt => opt.MapFrom(src => src.product.name))
                .ForMember(dest => dest.image, opt => opt.MapFrom(src => src.product.image))
                    .ReverseMap();
            CreateMap<OrderItem, BestSellingProductResponse>()
                .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.product));
            CreateMap<OrderItem, CategoryRevenueResponse>();
            //OrderItem Map

            //User Map
            CreateMap<User, AccountResponse>()
                .ForMember(dest => dest.email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();
          
            //User Map
        }
    }
}
