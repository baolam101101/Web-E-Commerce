using AutoMapper;
using Web_E_Commerce.DTOs.Client.Product.Requests;
using Web_E_Commerce.DTOs.Client.Product.Responses;
using Web_E_Commerce.Models;

namespace Web_E_Commerce.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductResponse>()
                .ForMember(dest => dest.CategoryName, opt =>
                    opt.MapFrom(src => src.Category != null ? src.Category.Name : null));
            CreateMap<ProductCreateRequest, Product>();
            CreateMap<ProductUpdateRequest, Product>();

        }
    }
}