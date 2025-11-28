using WebApi.Domain.Abstractions;

namespace WebApi.Domain.Errors;

public static class PhoneError
{
    public static readonly Error Empty = new("Phone.Empty", "Telefone vazio.");
    public static readonly Error TooLong = new(
        "Phone.TooLong",
        "Telefone excede o limite de caracteres."
    );
    public static readonly Error Invalid = new("Phone.Invalid", "Telefone inválido.");
}
