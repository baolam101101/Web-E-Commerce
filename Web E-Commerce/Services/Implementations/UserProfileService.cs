using AutoMapper;
using Web_E_Commerce.DTOs.Client.Profile.Requests;
using Web_E_Commerce.DTOs.Client.Profile.Responses;
using Web_E_Commerce.DTOs.Shared;
using Web_E_Commerce.DTOs.Shared.Constants;
using Web_E_Commerce.Exceptions;
using Web_E_Commerce.Repositories.Interfaces;
using Web_E_Commerce.Services.Interfaces;

namespace Web_E_Commerce.Services.Implementations
{
    public class UserProfileService(
        IUserRepositories userRepository,
        ICurrentUserService currentUser,
        IMapper mapper) : IUserProfileService
    {
        public async Task<ApiResponse<UserProfileResponse>> GetMyProfileAsync()
        {
            var userId = currentUser.UserId;

            var user = await userRepository.GetByIdWithRolesAsync(userId)
                ?? throw new NotFoundException(
                    MessageKeys.USER_NOT_FOUND,
                    MessageDescriptions.USER_NOT_FOUND);

            var response = mapper.Map<UserProfileResponse>(user);

            return ApiResponse<UserProfileResponse>.Ok(
                response,
                MessageKeys.GET_PROFILE_SUCCESS,
                MessageDescriptions.GET_PROFILE_SUCCESS);
        }

        public async Task<ApiResponse<UserProfileResponse>> UpdateMyProfileAsync(UpdateProfileRequest request)
        {
            var userId = currentUser.UserId;

            var user = await userRepository.GetByIdWithRolesAsync(userId)
                ?? throw new NotFoundException(
                    MessageKeys.USER_NOT_FOUND,
                    MessageDescriptions.USER_NOT_FOUND);

            // Email uniqueness check (bỏ qua nếu không đổi email)
            if (!string.IsNullOrWhiteSpace(request.Email) &&
                request.Email != user.Email)
            {
                var emailTaken = await userRepository.IsEmailTakenAsync(request.Email, userId);
                if (emailTaken)
                    throw new BadRequestException(
                        MessageKeys.EMAIL_ALREADY_TAKEN,
                        MessageDescriptions.EMAIL_ALREADY_TAKEN);
            }

            user.FullName = request.FullName.Trim();
            user.Email = request.Email.Trim();
            user.PhoneNumber = request.PhoneNumber.Trim();
            user.Address = request.Address?.Trim();

            await userRepository.SaveChangesAsync();

            var response = mapper.Map<UserProfileResponse>(user);

            return ApiResponse<UserProfileResponse>.Ok(
                response,
                MessageKeys.UPDATE_PROFILE_SUCCESS,
                MessageDescriptions.UPDATE_PROFILE_SUCCESS);
        }

        public async Task<ApiResponse<bool>> ChangePasswordAsync(ChangePasswordRequest request)
        {
            // Validate confirm password
            if (request.NewPassword != request.ConfirmNewPassword)
                throw new BadRequestException(
                    MessageKeys.PASSWORD_CONFIRM_MISMATCH,
                    MessageDescriptions.PASSWORD_CONFIRM_MISMATCH);

            // Validate min length
            if (request.NewPassword.Length < 6)
                throw new BadRequestException(
                    MessageKeys.PASSWORD_TOO_SHORT,
                    MessageDescriptions.PASSWORD_TOO_SHORT);

            var userId = currentUser.UserId;

            var user = await userRepository.GetByIdWithRolesAsync(userId)
                ?? throw new NotFoundException(
                    MessageKeys.USER_NOT_FOUND,
                    MessageDescriptions.USER_NOT_FOUND);

            // Verify current password
            if (!BCrypt.Net.BCrypt.Verify(request.CurrentPassword, user.PasswordHash))
                throw new BadRequestException(
                    MessageKeys.CURRENT_PASSWORD_INCORRECT,
                    MessageDescriptions.CURRENT_PASSWORD_INCORRECT);

            // Không cho đặt lại password cũ
            if (BCrypt.Net.BCrypt.Verify(request.NewPassword, user.PasswordHash))
                throw new BadRequestException(
                    MessageKeys.PASSWORD_SAME_AS_OLD,
                    MessageDescriptions.PASSWORD_SAME_AS_OLD);

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);

            await userRepository.SaveChangesAsync();

            return ApiResponse<bool>.Ok(
                true,
                MessageKeys.CHANGE_PASSWORD_SUCCESS,
                MessageDescriptions.CHANGE_PASSWORD_SUCCESS);
        }
    }
}
