namespace Web_E_Commerce.Exceptions;

public class ValidationException(IEnumerable<string> errors) : Exception("Validation failed")
{
    public List<string> Errors { get; } = errors.ToList();
}
