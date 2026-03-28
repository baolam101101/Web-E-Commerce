using AutoMapper;
using Web_E_Commerce.DTOs.Cart.Responses;
using Web_E_Commerce.Models;

namespace Web_E_Commerce.Mapping
{
    public class CartProfiles : Profile
    {
        public CartProfiles()
        {
            CreateMap<CartItem, CartItemResponse>()
                .ForMember(dest => dest.CartItemId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.PriceAtTime * src.Quantity)); ;

            CreateMap<Cart, CartResponse>()
                .ForMember(dest => dest.CartId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.CartItems));
        }
    }
}
