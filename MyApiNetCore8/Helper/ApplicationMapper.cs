using AutoMapper;
using MyApiNetCore8.DTO.Request;
using MyApiNetCore8.DTO.Response;
using MyApiNetCore8.Model;

namespace MyApiNetCore8.Helper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Product, ProductResponse>()
                .ForMember(dest => dest.category, opt => opt.MapFrom(src => src.Category));
            CreateMap<Category, CategoryResponse>();
            //CreateMap<ProductRequest, Product>()
            //    .ForMember(dest => dest.Category, opt => opt.Ignore());
        }
    }
}
