﻿using AutoMapper;
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
                .ForMember(dest => dest.category, opt => opt.MapFrom(src => src.Category)).ReverseMap();
            CreateMap<ProductRequest, Product>().ReverseMap();

            CreateMap<CategoryRequest, Category>().ReverseMap();
            CreateMap<Category, CategoryResponse>().ReverseMap();

            CreateMap<Coupon, CouponResponse>().ReverseMap();
            CreateMap<CouponRequest, Coupon>().ReverseMap();

            CreateMap<Permission, PermissionRequest>().ReverseMap();
            CreateMap<Permission, PermissionResponse>().ReverseMap();
        }
    }
}
