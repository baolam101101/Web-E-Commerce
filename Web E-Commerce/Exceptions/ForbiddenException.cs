namespace Web_E_Commerce.Exceptions
{
    public class ForbiddenException(string message, string? description = null) : BaseException(message, description)
    {
    }
}
