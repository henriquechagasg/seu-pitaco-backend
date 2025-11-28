using System.Text.RegularExpressions;

namespace WebApi.Core.Constants;

public static partial class Expressions
{
    [GeneratedRegex("\\D")]
    public static partial Regex OnlyDigits();

    [GeneratedRegex("^(?:55)?[1-9][0-9](?:9\\d{8}|\\d{8})$")]
    public static partial Regex PhoneRegex();
}
