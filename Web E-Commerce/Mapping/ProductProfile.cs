using AutoMapper;
using Web_E_Commerce.DTOs.Product.Requests;
using Web_E_Commerce.DTOs.Product.Responses;
using Web_E_Commerce.Models;

namespace Web_E_Commerce.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductCreateRequest, Product>();
            CreateMap<Product, ProductCreateResponse>();
        }
    }
}
