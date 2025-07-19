using AutoMapper;
using Web_E_Commerce.DTOs.Admin.UserRoles.Responses;
using Web_E_Commerce.Models;

namespace Web_E_Commerce.Mapping
{
    public class UserRoleProfile : Profile
    {
        public UserRoleProfile()
        {
            // Mapping from User to UserWithRolesResponse
            CreateMap<User, UserWithRolesResponse>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.UserRoles.Select(ur => ur.Role.Name)));

            // Mapping from User to UserWithRolesResponse
            CreateMap<User, AssignRoleResponse>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.UserRoles.Select(ur => ur.Role.Name)));

            // Mapping from Role to RoleDto
            CreateMap<Role, RoleDto>();
        }
    }
}
