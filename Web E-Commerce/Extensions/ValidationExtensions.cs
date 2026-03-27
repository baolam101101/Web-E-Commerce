using System.Text.RegularExpressions;
using Web_E_Commerce.DTOs.Shared.Constants;

namespace Web_E_Commerce.Extensions
{
    public static class ValidationExtensions
    {
        public static bool IsValidPhoneNumber(this string phone)
        {
            return Regex.IsMatch(phone, ValidationPatterns.PhoneNumber);
        }
    }
}