using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
}