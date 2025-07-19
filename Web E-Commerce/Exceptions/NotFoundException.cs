namespace Web_E_Commerce.Exceptions
{
    public class NotFoundException(string message, string? description = null) : BaseException(message, description)
    {
    }
}
