namespace Web_E_Commerce.Utilities
{
    public class TransactionIdHelper
    {
        public static string Generate(string prefix)
        {
            var timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
            var unique = Guid.NewGuid().ToString("N")[..6];

            return $"{prefix}_{timestamp}_{unique}";
        }
    }
}
