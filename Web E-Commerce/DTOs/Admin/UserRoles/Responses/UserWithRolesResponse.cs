namespace Web_E_Commerce.DTOs.Admin.UserRoles.Responses
{
    public class UserWithRolesResponse
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public List<string> Roles { get; set; } = [];
    }
}
