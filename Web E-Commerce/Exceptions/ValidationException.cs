using Web_E_Commerce.DTOs.Shared.Constants;

namespace Web_E_Commerce.Exceptions;

public class ValidationException : BaseException
{
    public List<string> Errors { get; }

    public ValidationException(IEnumerable<string> errors, string? description = null)
        : base(MessageKeys.VALIDATION_ERROR, description)
    {
        Errors = errors.ToList();
    }
}