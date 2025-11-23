namespace Web_E_Commerce.Exceptions
{
    public class BadRequestException(string key, string description) : BaseException(key, description)
    {
    }
}
