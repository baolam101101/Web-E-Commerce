using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web_E_Commerce.DTOs.Client.Profile.Requests;
using Web_E_Commerce.DTOs.Client.Profile.Responses;
using Web_E_Commerce.DTOs.Shared;
using Web_E_Commerce.Services.Interfaces;

namespace Web_E_Commerce.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class UsersController(IUserProfileService userProfileService) : ControllerBase
    {
        // GET api/v1/users/me
        [HttpGet("me")]
        public async Task<ApiResponse<UserProfileResponse>> GetMyProfile()
        {
            return await userProfileService.GetMyProfileAsync();
        }

        // PUT api/v1/users/me
        [HttpPut("me")]
        public async Task<ApiResponse<UserProfileResponse>> UpdateMyProfile(
            [FromBody] UpdateProfileRequest request)
        {
            return await userProfileService.UpdateMyProfileAsync(request);
        }

        // PUT api/v1/users/me/change-password
        [HttpPut("me/change-password")]
        public async Task<ApiResponse<bool>> ChangePassword(
            [FromBody] ChangePasswordRequest request)
        {
            return await userProfileService.ChangePasswordAsync(request);
        }
    }
}
