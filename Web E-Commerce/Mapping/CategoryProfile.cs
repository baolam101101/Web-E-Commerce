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
            CreateMap<CategoryCreateRequest, Category>()
                .ForMember(
                    dest => dest.NormalizedName,
                    opt => opt.MapFrom(src => src.Name.Trim().ToLower())
                    )
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => src.Name.Trim())
                    );
            CreateMap<CategoryUpdateRequest, Category>();
        }
    }
}
