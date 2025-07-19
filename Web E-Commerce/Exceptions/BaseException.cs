namespace Web_E_Commerce.Exceptions
{
    public abstract class BaseException(string message, string? description = null) : Exception(message)
    {
        public virtual string? Description { get; } = description;
    }
}
