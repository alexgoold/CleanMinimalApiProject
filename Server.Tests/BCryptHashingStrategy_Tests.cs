using FluentAssertions;
using Infrastructure.Security.HashingStrategy;
using Xunit;

namespace Tests;

public class BCryptHashingStrategy_Tests
{
    private BCryptHashingStrategy _sut = null!;

    public BCryptHashingStrategy_Tests()
    {
        _sut = new BCryptHashingStrategy();
    }

    [Fact]
    public void HashPassword_ShouldReturn_String_Different_From_Input()
    {
        // Arrange
        var password = "password";

        // Act
        var result = _sut.HashPassword(password);

        // Assert
        result.Should().NotBe(password);
    }

    [Fact]
    public void VerifyPassword_ShouldReturn_True_WhenGiven_A_Password_And_The_Same_Password_Hashed_By_This_Strategy()
    {
        // Arrange
        var password = "password";
        var hashedPassword = _sut.HashPassword(password);

        // Act
        var result = _sut.VerifyPassword(password, hashedPassword);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void
        VerifyPassword_ShouldReturn_False_WhenGiven_A_Password_And_A_Different_Password_Hashed_By_This_Strategy()
    {
        // Arrange
        var password = "password";
        var differentPassword = "differentPassword";
        var hashedPassword = _sut.HashPassword(differentPassword);

        // Act
        var result = _sut.VerifyPassword(password, hashedPassword);

        // Assert
        result.Should().BeFalse();
    }


}