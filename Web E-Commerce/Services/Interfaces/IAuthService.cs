using Web_E_Commerce.DTOs.Auth.Requests;
using Web_E_Commerce.DTOs.Auth.Responses;
using Web_E_Commerce.DTOs.Shared;

namespace Web_E_Commerce.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ApiResponse<AuthResponse>> Register(AuthRequest request);
        Task<ApiResponse<AuthResponse>> Login(AuthRequest request);
        Task<ApiResponse<AuthResponse>> RefreshTokenAsync(string refreshToken);
    }
}
