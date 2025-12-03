using WebApi.Shared.Domain.Errors;
using WebApi.Shared.Domain.ValueObjects;

namespace WebApiTest.Shared.Domain.ValueObjects;

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
        Assert.True(result.IsFailure);
        var error = result.GetError();
        Assert.Equal(PhoneError.Empty, error);
    }

    [Fact]
    public void Create_ReturnsError_WhenPhoneTooLong()
    {
        // Arrange
        var input = new string('9', Phone.MaxLength + 1);

        // Act
        var result = Phone.Create(input);

        // Assert
        Assert.True(result.IsFailure);
        var error = result.GetError();
        Assert.Equal(PhoneError.TooLong, error);
    }

    [Theory]
    [InlineData("+1 (234) 567-8901")]
    [InlineData("1234567")]
    [InlineData("00123456789")]
    [InlineData("+44 20 7946 0958")]
    public void Create_ReturnsError_WhenInvalid(string input)
    {
        // Act
        var result = Phone.Create(input);

        // Assert
        Assert.True(result.IsFailure);
        var error = result.GetError();
        Assert.Equal(PhoneError.Invalid, error);
    }

    [Theory]
    [InlineData("(11) 99999-9999", "11999999999")]
    [InlineData("11999999999", "11999999999")]
    [InlineData("+55 (11) 99999-9999", "11999999999")]
    [InlineData("5511999999999", "11999999999")]
    [InlineData("11-99999-9999", "11999999999")]
    [InlineData("(21) 8765-4321", "21987654321")]
    [InlineData("21 8765-4321", "21987654321")]
    [InlineData("2187654321", "21987654321")]
    public void Create_ReturnsPhone_WhenValid(string input, string expected)
    {
        // Act
        var result = Phone.Create(input);

        // Assert
        Assert.True(result.IsSuccess);
        var phone = result.GetValue();
        Assert.Equal(expected, phone.Value);
    }

    [Fact]
    public void Create_TrimsLeadingAndTrailingSpaces_BeforeStorage()
    {
        // Arrange
        var input = "   (11) 99999-9999   ";

        // Act
        var result = Phone.Create(input);

        // Assert
        Assert.True(result.IsSuccess);
        var phone = result.GetValue();
        Assert.Equal("11999999999", phone.Value);
    }

    [Fact]
    public void Create_AddLeadingNine_BeforeStorage()
    {
        // Arrange
        var input = "(37)99862716";

        // Act
        var result = Phone.Create(input);

        // Assert
        Assert.True(result.IsSuccess);
        var phone = result.GetValue();
        Assert.Equal("37999862716", phone.Value);
    }

    [Fact]
    public void Create_RemoveCountryCode_BeforeStorage()
    {
        // Arrange
        var input = "5537999862716";

        // Act
        var result = Phone.Create(input);

        // Assert
        Assert.True(result.IsSuccess);
        var phone = result.GetValue();
        Assert.Equal("37999862716", phone.Value);
    }
}
