using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WargearTracker.Api.DTOs;
using WargearTracker.Core;
using WargearTracker.Data;

namespace WargearTracker.Api.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(WebApplication app)
    {
        app.MapPost("/api/auth/register", async (AuthDto.AuthRequest request, WargearDbContext db,IConfiguration config) =>
        {
            if (await db.Users.AnyAsync(u => u.Email == request.Email))
            {
                return Results.BadRequest("Email already exists.");
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };

            db.Users.Add(user);
            await db.SaveChangesAsync();

            var token = GenerateToken(user, config);
            return Results.Ok(new
            {
                token
            });
        });

        app.MapPost("/api/auth/login", async (AuthDto.AuthRequest request, WargearDbContext db, IConfiguration config) =>
        {
            var user = await db.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return Results.BadRequest("Invalid email or password.");
            }
            var token = GenerateToken(user, config);
            return Results.Ok(new
            {
                token
            });
        });
    }

    private static string GenerateToken(User user, IConfiguration config)
    {
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Email, user.Email)
    };

        var token = new JwtSecurityToken(
            issuer: config["Jwt:Issuer"],
            audience: config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}
