using AutoMapper;
using Web_E_Commerce.DTOs.Cart.Responses;
using Web_E_Commerce.Models;

namespace Web_E_Commerce.Mapping
{
    public class CartProfile : Profile
    {
        public CartProfile()
        {
            CreateMap<CartItem, CartItemResponse>()
                .ForMember(dest => dest.CartItemId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.PriceAtTime * src.Quantity))
                .ForMember(dest => dest.Stock, opt => opt.MapFrom(src => src.Product.Stock));

            CreateMap<Cart, CartResponse>()
                .ForMember(dest => dest.CartId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.CartItems));
        }
    }
}
