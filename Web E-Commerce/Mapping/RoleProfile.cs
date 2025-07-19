using AutoMapper;
using Web_E_Commerce.DTOs.Admin.UserRoles.Responses;
using Web_E_Commerce.Models;

namespace Web_E_Commerce.Mapping
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleDto>();
        }
    }
}
