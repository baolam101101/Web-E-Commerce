using System.Text.RegularExpressions;

namespace Web_E_Commerce.Utilities
{
    public static class SlugHelper
    {
        public static string Generate(string input)
        {
            var slug = input.ToLower().Trim();

            slug = Regex.Replace(slug, @"\s+", "-");

            slug = Regex.Replace(slug, @"[^a-z0-9\-]", "");

            return slug;
        }
    }
}
