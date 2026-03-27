using System.Security.Claims;
using Web_E_Commerce.Services.Interfaces;

namespace Web_E_Commerce.Services.Implementations
{
    public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
    {
        public Guid UserId => 
            Guid.TryParse(httpContextAccessor.HttpContext?.User
                .FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId) 
            ? userId 
            : Guid.Empty;
        public string UserName => 
            httpContextAccessor.HttpContext?.User
                .FindFirst(ClaimTypes.Name)?.Value ?? string.Empty;
        public IEnumerable<string> Roles =>
            httpContextAccessor.HttpContext?.User
                .FindAll(ClaimTypes.Role)
                .Select(r => r.Value)
            ?? Enumerable.Empty<string>();
        public bool IsAuthenticated => 
            httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;
    }
}