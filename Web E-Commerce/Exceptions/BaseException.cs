namespace Web_E_Commerce.Exceptions
{
    public class BaseException : Exception
    {
        public string Key { get; }
        public string? Description { get; }

        public BaseException(string key, string? description = null)
            : base(key)
        {
            Key = key;
            Description = description;
        }
    }
}