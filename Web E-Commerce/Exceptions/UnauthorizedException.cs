namespace Web_E_Commerce.Exceptions
{
    public class UnauthorizedException(string key, string? description = null) : BaseException(key, description)
    {
    }
}
