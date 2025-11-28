using System.Text.RegularExpressions;
using WebApi.Domain.Abstractions;
using WebApi.Domain.Errors;

namespace WebApi.Domain.ValueObjects;

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

        if (!PhoneNumber().IsMatch(phone))
        {
            return PhoneError.Invalid;
        }

        return null;
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    [GeneratedRegex("\\D")]
    private static partial Regex OnlyDigits();

    [GeneratedRegex("^(?:55)?[1-9][0-9](?:9\\d{8}|\\d{8})$")]
    private static partial Regex PhoneNumber();
}
