using CarShop.Domain.Entities;

namespace CarShop.Domain.Exceptions;

public class ValidationException : Exception
{
    public ValidationException(IEnumerable<ValidationError> errors)
        : base("Validation exception")
    {
        Errors = errors;
    }

    public IEnumerable<ValidationError> Errors { get; }
}