using AutoMapper;
using Web_E_Commerce.DTOs.Order.Responses;
using Web_E_Commerce.Models;

namespace Web_E_Commerce.Mapping
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderResponse>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.OrderItems));

            CreateMap<OrderItem, OrderItemResponse>();
        }
    }
}
