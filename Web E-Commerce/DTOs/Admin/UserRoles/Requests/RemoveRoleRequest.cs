namespace Web_E_Commerce.DTOs.Admin.UserRoles.Requests
{
    public class RemoveRoleRequest
    {
        public int UserId { get; set; }
        public string RoleName { get; set; } = string.Empty;
    }
}
