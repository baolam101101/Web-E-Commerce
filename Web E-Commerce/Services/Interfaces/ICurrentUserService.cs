using System.Security.Claims;

namespace Web_E_Commerce.Services.Interfaces
{
    public interface ICurrentUserService
    {
        Guid UserId { get; }
        string UserName { get; }
        IEnumerable<string> Roles { get; }
        bool IsAuthenticated { get; }
    }
}