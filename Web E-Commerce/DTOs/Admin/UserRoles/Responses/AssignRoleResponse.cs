namespace Web_E_Commerce.DTOs.Admin.UserRoles.Responses
{
    public class AssignRoleResponse
    {
        public Guid UserId { get; set; }
        public List<string> Roles { get; set; } = [];
    }
}