using Web_E_Commerce.Enums;
using Web_E_Commerce.Models;

namespace Web_E_Commerce.Services.Interfaces
{
    public interface IAuthService
    {
        Task<bool> UserExists(string username);
        Task<User> Register(string username, string password, UserRole role);
        Task<string?> Login(string username, string password);
    }
}
