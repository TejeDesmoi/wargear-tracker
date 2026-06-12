namespace WargearTracker.Api.DTOs;

public class AuthDto
{
    public record AuthRequest(string Email, string Password);
}
