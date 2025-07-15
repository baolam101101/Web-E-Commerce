using AutoMapper;
using Web_E_Commerce.DTOs.Admin.SellerRequest.Responses;
using Web_E_Commerce.DTOs.Seller.Requests;
using Web_E_Commerce.Models;

namespace Web_E_Commerce.Mapping
{
    public class SellerRequestProfile : Profile
    {
        public SellerRequestProfile() 
        {
            CreateMap<SellerRequestDto, SellerRequest>()
                .ForMember(dest => dest.RequestAt, opt => opt.MapFrom(_ => DateTime.UtcNow));

            CreateMap<SellerRequest, SellerRequestResponse>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User!.UserName));
        }
    }
}
