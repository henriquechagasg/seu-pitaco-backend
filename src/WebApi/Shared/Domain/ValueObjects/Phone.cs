using WebApi.Shared.Abstractions;
using WebApi.Shared.Domain.Errors;
using static WebApi.Shared.Core.Constants.Expressions;

namespace WebApi.Shared.Domain.ValueObjects;

public partial class Phone : ValueObject
{
    public const int MaxLength = 13;
    public string Value { get; }

    private Phone(string value)
    {
        Value = value;
    }

    public static Result<Phone> Create(string phone)
    {
        phone = Sanitize(phone);

        var error = Validate(phone);
        if (error != null)
        {
            return error;
        }

        return new Phone(phone);
    }

    private static string Sanitize(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
        {
            return string.Empty;
        }

        var sanitized = OnlyDigits().Replace(phone, string.Empty);

        if (sanitized.Length > 10 && sanitized.StartsWith("55"))
        {
            sanitized = sanitized[2..];
        }

        if (sanitized.Length == 10)
        {
            sanitized = string.Concat(sanitized.AsSpan(0, 2), "9", sanitized.AsSpan(2));
        }

        return sanitized;
    }

    private static Error? Validate(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
        {
            return PhoneError.Empty;
        }

        if (phone.Length > MaxLength)
        {
            return PhoneError.TooLong;
        }

        if (!PhoneRegex().IsMatch(phone))
        {
            return PhoneError.Invalid;
        }

        return null;
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
