using Web_E_Commerce.DTOs.Client.Profile.Requests;
using Web_E_Commerce.DTOs.Client.Profile.Responses;
using Web_E_Commerce.DTOs.Shared;

namespace Web_E_Commerce.Services.Interfaces
{
    public interface IUserProfileService
    {
        Task<ApiResponse<UserProfileResponse>> GetMyProfileAsync();
        Task<ApiResponse<UserProfileResponse>> UpdateMyProfileAsync(UpdateProfileRequest request);
        Task<ApiResponse<bool>> ChangePasswordAsync(ChangePasswordRequest request);
    }

}
