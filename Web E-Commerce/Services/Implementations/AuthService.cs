using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Web_E_Commerce.Data;
using Web_E_Commerce.DTOs.Auth.Requests;
using Web_E_Commerce.DTOs.Auth.Responses;
using Web_E_Commerce.DTOs.Shared;
using Web_E_Commerce.DTOs.Shared.Constants;
using Web_E_Commerce.Exceptions;
using Web_E_Commerce.Models;
using Web_E_Commerce.Services.Interfaces;

namespace Web_E_Commerce.Services.Implementations
{
    public class AuthService(AppDbContext context, IConfiguration config) : IAuthService
    {
        public async Task<ApiResponse<AuthResponse>> Register(AuthRequest request)
        {
            if (await context.Users.AnyAsync(u => u.UserName == request.UserName))
                throw new BadRequestException("Username already exists");

            var customerRole = await context.Roles
                .FirstOrDefaultAsync(r => r.Name == "Customer")
                ?? throw new NotFoundException("Default role 'Customer' not found");

            var user = new User
            {
                UserName = request.UserName,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                UserRoles = new List<UserRole>
                {
                    new UserRole {
                        RoleId = customerRole.Id,
                        Role = customerRole }
                }
            };

            // Create Refresh token
            var refreshToken = GenerateRefreshToken();

            var refreshTokenEntity = new RefreshToken
            {
                Token = refreshToken,
                ExpiryDate = DateTime.UtcNow.AddDays(7),
                IsRevoked = false,
                User = user
            };

            context.Users.Add(user);
            context.RefreshTokens.Add(refreshTokenEntity);

            await context.SaveChangesAsync();

            // Create Access Token AFTER SaveChanges (user.Id now exists)
            var token = CreateAccessToken(user);

            return ApiResponse<AuthResponse>.Ok(
                new AuthResponse
                {
                    AccessToken = token,
                    RefreshToken = refreshToken
                },
                MessageKeys.REGISTER_SUCCESS,
                MessageDescriptions.USER_REGISTRATION_SUCCESS
            );
        }

        public async Task<ApiResponse<AuthResponse>> Login(AuthRequest request)
        {
            var user = await context.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.UserName == request.UserName)
                ?? throw new UnauthorizedException("Invalid username or password");

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                throw new UnauthorizedException("Invalid username or password");

            // Create Access token
            var token = CreateAccessToken(user);

            // Create Refresh token
            var refreshToken = GenerateRefreshToken();

            var refreshTokenEntity = new RefreshToken
            {
                Token = refreshToken,
                ExpiryDate = DateTime.UtcNow.AddDays(7),
                IsRevoked = false,
                UserId = user.Id
            };

            context.RefreshTokens.Add(refreshTokenEntity);

            await context.SaveChangesAsync();

            return ApiResponse<AuthResponse>.Ok(
                new AuthResponse
                {
                    AccessToken = token,
                    RefreshToken = refreshToken
                },
                MessageKeys.LOGIN_SUCCESS,
                MessageDescriptions.USER_LOGIN_SUCCESS
            );
        }

        private string GenerateRefreshToken()
        {
            var randomBytes = new byte[64];

            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);

            return Convert.ToBase64String(randomBytes);
        }

        private string CreateAccessToken(User user)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Name, user.UserName)
            };

            foreach (var role in user.UserRoles.Select(ur => ur.Role.Name))
                claims.Add(new Claim(ClaimTypes.Role, role));

            var jwtKey = config["Jwt:Key"]
                ?? throw new InvalidOperationException("JWT Key not configured");

            var expiryInMinutes = int.Parse(
                config["Jwt:ExpiryInMinutes"]
                ?? throw new InvalidOperationException("JWT Expiry not configured")
            );

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

            var creds = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha512Signature
            );

            var token = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"],
                audience: config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiryInMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<ApiResponse<AuthResponse>> RefreshTokenAsync(string refreshToken)
        {
            var tokenInDb = await context.RefreshTokens
                .Include(rt => rt.User)
                    .ThenInclude(u => u.UserRoles)
                        .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(rt => rt.Token == refreshToken);

            if (tokenInDb == null)
                throw new UnauthorizedException("Invalid refresh token");

            if (tokenInDb.IsRevoked)
                throw new UnauthorizedException("Refresh token revoked");

            if (tokenInDb.ExpiryDate < DateTime.UtcNow)
                throw new UnauthorizedException("Refresh token expired");

            // Option: revoke token cũ (khuyến nghị)
            tokenInDb.IsRevoked = true;

            // Tạo Access Token mới
            var newAccessToken = CreateAccessToken(tokenInDb.User);

            // Rotate Refresh Token
            var newRefreshToken = GenerateRefreshToken();

            var newRefreshTokenEntity = new RefreshToken
            {
                Token = newRefreshToken,
                ExpiryDate = DateTime.UtcNow.AddDays(7),
                UserId = tokenInDb.UserId,
                IsRevoked = false
            };

            context.RefreshTokens.Add(newRefreshTokenEntity);

            await context.SaveChangesAsync();

            return ApiResponse<AuthResponse>.Ok(
                new AuthResponse
                {
                    AccessToken = newAccessToken,
                    RefreshToken = newRefreshToken
                },
                MessageKeys.LOGIN_SUCCESS,
                MessageDescriptions.USER_LOGIN_SUCCESS
            );
        }
    }
}