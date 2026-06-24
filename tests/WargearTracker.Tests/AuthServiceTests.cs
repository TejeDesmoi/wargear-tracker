using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WargearTracker.Api.Services;
using WargearTracker.Data;

namespace WargearTracker.Tests;

public class AuthServiceTests
{
    private AuthService CreateAuthService(WargearDbContext db)
    {
        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                { "Jwt:Key", "TestKey12345678901234567890123456" },
                { "Jwt:Issuer", "test" },
                { "Jwt:Audience", "test" }
            })
            .Build();

        return new AuthService(db, config);
    }

    [Fact]
    public async Task RegisterAsync_WithNewEmail_ReturnsToken()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<WargearDbContext>()
            .UseInMemoryDatabase(databaseName: "Register_NewEmail")
            .Options;
        using var db = new WargearDbContext(options);
        var service = CreateAuthService(db);

        // Act
        var result = await service.RegisterAsync("test@test.com", "password123");

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task RegisterAsync_WithExistingEmail_ReturnsNull()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<WargearDbContext>()
            .UseInMemoryDatabase(databaseName: "Register_ExistingEmail")
            .Options;
        using var db = new WargearDbContext(options);
        var service = CreateAuthService(db);
        await service.RegisterAsync("test@test.com", "password123");

        // Act
        var result = await service.RegisterAsync("test@test.com", "otrapassword");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task LoginAsync_WithCorrectCredentials_ReturnsToken()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<WargearDbContext>()
            .UseInMemoryDatabase(databaseName: "Login_CorrectCredentials")
            .Options;
        using var db = new WargearDbContext(options);
        var service = CreateAuthService(db);
        await service.RegisterAsync("test@test.com", "password123");

        // Act
        var result = await service.LoginAsync("test@test.com", "password123");

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task LoginAsync_WithWrongPassword_ReturnsNull()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<WargearDbContext>()
            .UseInMemoryDatabase(databaseName: "Login_WrongPassword")
            .Options;
        using var db = new WargearDbContext(options);
        var service = CreateAuthService(db);
        await service.RegisterAsync("test@test.com", "password123");

        // Act
        var result = await service.LoginAsync("test@test.com", "passwordincorrecta");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task RegisterAsync_CorrectTokenUserClaims()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<WargearDbContext>()
            .UseInMemoryDatabase(databaseName: "Register_CorrectTokenClaims")
            .Options;
        using var db = new WargearDbContext(options);
        var service = CreateAuthService(db);
        // Act
        var token = await service.RegisterAsync("test@test.com", "otrapassword");
        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(token);
        var userIdClaim = jwt.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);


        // Assert
        Assert.Equal("test@test.com", jwt.Claims.First(c => c.Type == ClaimTypes.Email).Value);
        Assert.NotNull(userIdClaim);
        Assert.False(string.IsNullOrEmpty(userIdClaim.Value));
        Assert.Equal("test", jwt.Issuer);
        Assert.Equal("test", jwt.Audiences.First());
    }

    [Fact]
    public async Task RegisterAsync_TokenExpiresInSevenDays()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<WargearDbContext>()
            .UseInMemoryDatabase(databaseName: "Register_TokenExpiry")
            .Options;
        using var db = new WargearDbContext(options);
        var service = CreateAuthService(db);

        // Act
        var token = await service.RegisterAsync("test@test.com", "password123");
        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(token);

        // Assert
        var expectedExpiry = DateTime.UtcNow.AddDays(7);
        Assert.True(jwt.ValidTo > expectedExpiry.AddMinutes(-1));
        Assert.True(jwt.ValidTo < expectedExpiry.AddMinutes(1));
    }

    [Fact]
    public async Task RegisterAsync_TokenWithWrongKey_FailsValidation()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<WargearDbContext>()
            .UseInMemoryDatabase(databaseName: "Register_WrongKey")
            .Options;
        using var db = new WargearDbContext(options);
        var service = CreateAuthService(db);
        var token = await service.RegisterAsync("test@test.com", "password123");
        var handler = new JwtSecurityTokenHandler();

        // Act & Assert
        Assert.ThrowsAny<Exception>(() =>
            handler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes("ClaveIncorrectaQueNoCoincide12345"))
            }, out _));
    }
}