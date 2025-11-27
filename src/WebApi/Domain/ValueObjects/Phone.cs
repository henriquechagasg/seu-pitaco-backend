using WebApi.Domain.Abstractions;

namespace WebApi.Domain.ValueObjects;

public class Phone : ValueObject
{
    public const int MaxLength = 50;
    public string Value { get; }

    private Phone(string value)
    {
        Value = value;
    }

    public static Result<Phone> Create(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
        {
            return new Error("Phone.Empty", "Telefone vazio.");
        }

        if (phone.Length > MaxLength)
        {
            return new Error("Phone.TooLong", "Telefone excede o limite de caracteres.");
        }

        return new Phone(phone);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
