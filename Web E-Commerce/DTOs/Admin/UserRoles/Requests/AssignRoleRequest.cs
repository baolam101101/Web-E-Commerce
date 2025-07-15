namespace Web_E_Commerce.DTOs.Admin.UserRoles.Requests
{
    public class AssignRoleRequest
    {
        public int UserId { get; set; }
        public string RoleName { get; set; } = string.Empty;
    }
}