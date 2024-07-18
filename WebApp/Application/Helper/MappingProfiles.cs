using AutoMapper;
using WebApp.Application.Dto;
using WebApp.Domain.Models;

namespace ReviewApp.Application.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Products, ProductDto>();
            CreateMap<ProductDto, Products>();
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();

        }
    }
}