using System.Security.Claims;

namespace Web_E_Commerce.Services.Interfaces
{
    public interface ICurrentUserService
    {
        int UserId { get; }
        string UserName { get; }
        string Role { get; }
        bool IsAuthenticated { get; }
        IEnumerable<Claim> Claims { get; }
    }
}
