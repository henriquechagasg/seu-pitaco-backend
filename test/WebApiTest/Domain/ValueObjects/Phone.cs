using WebApi.Domain.ValueObjects;
using Xunit.Sdk;

namespace WebApiTest.Domain.ValueObjects;

public class PhoneTests
{
    [Fact]
    public void Create_ReturnsError_WhenPhoneIsNullOrWhitespace()
    {
        // Arrange
        string? input = "";

        // Act
        var result = Phone.Create(input);

        // Assert
        var error = result.Match(_ => throw new XunitException("Expected error result"), e => e);

        Assert.Equal("Phone.Empty", error.Code);
    }

    [Fact]
    public void Create_ReturnsError_WhenPhoneTooLong()
    {
        // Arrange
        var input = new string('9', Phone.MaxLength + 1);

        // Act
        var result = Phone.Create(input);

        // Assert
        var error = result.Match(_ => throw new XunitException("Expected error result"), e => e);
        Assert.Equal("Phone.TooLong", error.Code);
    }

    [Fact]
    public void Create_ReturnsPhone_WhenValid()
    {
        // Arrange
        var input = "(11) 99999-9999";

        // Act
        var result = Phone.Create(input);

        // Assert
        var phone = result.Match(_ => _, e => throw new XunitException("Expected success result"));
        Assert.Equal(input, phone.Value);
    }
}
