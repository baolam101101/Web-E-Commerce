namespace Web_E_Commerce.Exceptions
{
    public class BadRequestException(string message, string? description = null) : BaseException(message, description)
    {

    }
}
