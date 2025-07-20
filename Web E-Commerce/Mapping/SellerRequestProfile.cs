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
            // Mapping configurations for SellerRequest
            CreateMap<SellerRequestDto, SellerRequest>()
                .BeforeMap((src, dest) =>
                {
                    dest.Status = "Pending";
                    dest.RequestAt = DateTime.UtcNow;
                });

            CreateMap<SellerRequest, SellerRequestResponse>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User!.UserName))
                .ForMember(dest => dest.RequestedAt, opt => opt.MapFrom(src => src.RequestAt));
        }
    }
}