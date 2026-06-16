using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.JSInterop;
using WargearTracker.Web.Models;

namespace WargearTracker.Web.Services;

public class ArmyService
{
    private readonly HttpClient _http;
    private readonly AuthService _auth;

    public ArmyService(HttpClient http, AuthService auth)
    {
        _http = http;
        _auth = auth;
    }

    public async Task<List<ArmyModel>> GetArmiesAsync()
    {
        var token = await _auth.GetTokenAsync();
        if (string.IsNullOrEmpty(token))
            throw new UnauthorizedAccessException("User is not authenticated.");

        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        return await _http.GetFromJsonAsync<List<ArmyModel>>("armies") ?? new List<ArmyModel>();
    }

    public async Task CreateArmyAsync(string name, string faction, string game)
    {
        var token = await _auth.GetTokenAsync();
        if (string.IsNullOrEmpty(token))
            throw new UnauthorizedAccessException("User is not authenticated.");
        _http.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue("Bearer", token);

        await _http.PostAsJsonAsync("armies", new
        {
            name,
            faction,
            game
        });
    }

    public async Task DeleteArmyAsync(Guid id)
    {
        var token = await _auth.GetTokenAsync();
        if (string.IsNullOrEmpty(token))
            throw new UnauthorizedAccessException("User is not authenticated.");
        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        await _http.DeleteAsync($"armies/{id}");
    }

    public async Task<ArmyModel?> GetArmyAsync(Guid id)
    {
        var token = await _auth.GetTokenAsync();
        if (string.IsNullOrEmpty(token))
            throw new UnauthorizedAccessException("User is not authenticated.");
        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        return await _http.GetFromJsonAsync<ArmyModel>($"armies/{id}");
    }

    public async Task<ArmyModel?> GetPublicArmyAsync(string slug)
    {
        try
        {
            return await _http.GetFromJsonAsync<ArmyModel>($"armies/public/{slug}");
        }
        catch (HttpRequestException)
        {
            return null;
        }
    }

}
