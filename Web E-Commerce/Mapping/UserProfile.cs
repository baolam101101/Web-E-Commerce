using AutoMapper;
using Web_E_Commerce.DTOs.Client.Profile.Responses;
using Web_E_Commerce.Models;

namespace Web_E_Commerce.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserProfileResponse>()
                .ForMember(dest => dest.Roles,
                    opt => opt.MapFrom(src =>
                        src.UserRoles.Select(ur => ur.Role.Name).ToList()));
        }
    }
}
