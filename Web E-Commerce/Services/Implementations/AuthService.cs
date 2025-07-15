using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Web_E_Commerce.Data;
using Web_E_Commerce.Enums;
using Web_E_Commerce.Models;
using Web_E_Commerce.Services.Interfaces;

namespace Web_E_Commerce.Services.Implementations
{
    public class AuthService(AppDbContext context, IConfiguration config) : IAuthService
    {
        public async Task<bool> UserExists(string username) =>
            await context.Users.AnyAsync(u => u.UserName == username);

        public async Task<User> Register(string username, string password)
        {
            var user = new User
            {
                UserName = username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password)
            };

            var customerRole = await context.Roles.FirstOrDefaultAsync(r => r.Name == "Customer") ?? throw new Exception("Default role 'Customer' not found");

            user.UserRoles = [new UserRole { RoleId = customerRole.Id }];

            context.Users.Add(user);
            await context.SaveChangesAsync();
            return user;
        }

        public async Task<string?> Login(string username, string password)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.UserName == username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash)) return null;

            return CreateAccessToken(user);
        }

        // Helper method to create JWT access token
        private string CreateAccessToken(User user)
        {
            var roles = context.UserRoles
                .Include(ur => ur.Role)
                .Where(ur => ur.UserId == user.Id)
                .Select(ur => ur.Role.Name)
                .ToList();

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Name, user.UserName),
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var accessToken = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"],
                audience: config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(accessToken);
        }
    }
}
