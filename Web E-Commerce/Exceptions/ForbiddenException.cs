namespace Web_E_Commerce.Exceptions
{
    public class ForbiddenException(string key, string? description = null) : BaseException(key, description)
    {
    }
}
