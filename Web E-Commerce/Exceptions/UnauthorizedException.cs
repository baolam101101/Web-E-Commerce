namespace Web_E_Commerce.Exceptions
{
    public class UnauthorizedException(string message, string? description = null) : BaseException(message, description)
    {
    }
}
