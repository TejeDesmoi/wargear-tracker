using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WargearTracker.Api.DTOs;
using WargearTracker.Api.Services;
using WargearTracker.Core;
using WargearTracker.Data;
using static WargearTracker.Api.DTOs.AuthDto;

namespace WargearTracker.Api.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this WebApplication app)
    {
        app.MapPost("/api/auth/register", async (AuthService authService, AuthRequest request) =>
        {
            var token = await authService.RegisterAsync(request.Email, request.Password);
            return token != null
                ? Results.Ok(new
                {
                    token
                })
                : Results.BadRequest("Email already exists.");
        });

        app.MapPost("/api/auth/login", async (AuthService authService, AuthRequest request) =>
        {
            var token = await authService.LoginAsync(request.Email, request.Password);
            return token != null
                ? Results.Ok(new
                {
                    token
                })
                : Results.BadRequest("Invalid email or password.");
        });
    }

    

}
