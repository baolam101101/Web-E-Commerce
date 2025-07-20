using System.Security.Claims;
using Web_E_Commerce.Services.Interfaces;

namespace Web_E_Commerce.Services.Implementations
{
    public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
    {
        public int UserId => 
            int.TryParse(httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId) ? userId : 0;
        public string UserName => 
            httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Name)?.Value ?? string.Empty;
        public string Role =>
            httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty;
        public bool IsAuthenticated => 
            httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;
        public IEnumerable<Claim> Claims => 
            httpContextAccessor.HttpContext?.User.Claims ?? [];
    }
}