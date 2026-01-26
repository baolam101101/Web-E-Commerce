using AutoMapper;
using Web_E_Commerce.DTOs.Category.Requests;
using Web_E_Commerce.DTOs.Category.Responses;
using Web_E_Commerce.Models;

namespace Web_E_Commerce.Mapping
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryResponse>();
            CreateMap<CategoryCreateRequest, Category>();
            CreateMap<CategoryUpdateRequest, Category>();
        }
    }
}
