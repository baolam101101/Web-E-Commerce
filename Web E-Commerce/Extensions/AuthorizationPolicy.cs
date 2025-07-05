using Microsoft.AspNetCore.Authorization;

namespace Web_E_Commerce.Extensions
{
    public static class AuthorizationPolicy
    {
        public static void AddPoliciesFromEnum<TEnum>(this AuthorizationOptions options) where TEnum : Enum
        {
            var roles = Enum.GetNames(typeof(TEnum));

            foreach (var role in roles)
            {
                options.AddPolicy(role, policy => policy.RequireRole(role));
            }
        }
    }
}
