using CaferLib.VerifierLib;
using Xunit;

namespace CaferLib.VerifierLib.Tests;

public class VerifierTests
{
    // ======= Email tests =======

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void IsValidEmailFormat_ShouldFail_WhenEmailIsNullOrEmpty(string? email)
    {
        var result = Verifier.IsValidEmailFormat(email);

        Assert.False(result.IsValid);
        Assert.Contains("Email cannot be null or empty.", result.Errors);
    }

    [Theory]
    [InlineData("plainaddress")]
    [InlineData("missingatsign.com")]
    [InlineData("user@domain..com")]
    [InlineData("user.@domain.com")]
    public void IsValidEmailFormat_ShouldFail_WhenEmailIsInvalid(string email)
    {
        var result = Verifier.IsValidEmailFormat(email);

        Assert.False(result.IsValid);
        Assert.Contains("Email format is invalid.", result.Errors);
    }

    [Theory]
    [InlineData("user@example.com")]
    [InlineData("first.last@domain.co.uk")]
    public void IsValidEmailFormat_ShouldPass_WhenEmailIsValid(string email)
    {
        var result = Verifier.IsValidEmailFormat(email);

        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    // ======= Password tests =======

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void IsValidPasswordFormat_ShouldFail_WhenPasswordIsNullOrEmpty(string password)
    {
        var result = Verifier.IsValidPasswordFormat(password, 6, 12, true, true, true, true);

        Assert.False(result.IsValid);
        Assert.Contains("Password cannot be null or empty.", result.Errors);
    }

    [Fact]
    public void IsValidPasswordFormat_ShouldFail_WhenPasswordTooShortOrLong()
    {
        var shortPass = "Ab1!";
        var longPass = "Abcdef1234567890!";
        var resultShort = Verifier.IsValidPasswordFormat(shortPass, 6, 12, true, true, true, true);
        var resultLong = Verifier.IsValidPasswordFormat(longPass, 6, 12, true, true, true, true);

        Assert.False(resultShort.IsValid);
        Assert.Contains("Password must be between 6 and 12 characters.", resultShort.Errors);

        Assert.False(resultLong.IsValid);
        Assert.Contains("Password must be between 6 and 12 characters.", resultLong.Errors);
    }

    [Fact]
    public void IsValidPasswordFormat_ShouldFail_WhenPasswordMissingRequirements()
    {
        var password = "abcdef"; // no uppercase, number, special
        var result = Verifier.IsValidPasswordFormat(password, 6, 12, true, true, true, true);

        Assert.False(result.IsValid);
        Assert.Contains("Password must contain at least one uppercase letter.", result.Errors);
        Assert.Contains("Password must contain at least one numeric digit.", result.Errors);
        Assert.Contains("Password must contain at least one special character.", result.Errors);
    }

    [Fact]
    public void IsValidPasswordFormat_ShouldPass_WhenPasswordMeetsAllRequirements()
    {
        var password = "Abc123!";
        var result = Verifier.IsValidPasswordFormat(password, 6, 12, true, true, true, true);

        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }
}
