namespace Web_E_Commerce.Exceptions
{
    public class NotFoundException(string key, string? description = null) : BaseException(key, description)
    {
    }
}