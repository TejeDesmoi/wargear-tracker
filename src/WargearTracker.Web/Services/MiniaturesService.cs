using System.Net.Http.Json;
using Microsoft.JSInterop;

namespace WargearTracker.Web.Services;

public class MiniaturesService
{
    private readonly HttpClient _http;
    private readonly AuthService _auth;

    public MiniaturesService(HttpClient http, AuthService auth)
    {
        _http = http;
        _auth = auth;
    }

    public async Task<List<MiniatureModel>> GetMiniaturesAsync(Guid armyId)
    {
        var token = await _auth.GetTokenAsync();
        if (string.IsNullOrEmpty(token))
            throw new UnauthorizedAccessException("User is not authenticated.");
        _http.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        return await _http.GetFromJsonAsync<List<MiniatureModel>>($"armies/{armyId}/miniatures") ?? new List<MiniatureModel>();
    }

    public async Task CreateMiniatureAsync(string name, string unitType, int quantity, string paintStatus, Guid armyId)
    {
        var token = await _auth.GetTokenAsync();
        if (string.IsNullOrEmpty(token))
            throw new UnauthorizedAccessException("User is not authenticated.");
        _http.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        await _http.PostAsJsonAsync("miniatures", new
        {
            name,
            unitType,
            quantity,
            paintStatus,
            armyId
        });
    }

    public async Task UpdateMiniatureAsync(Guid id,string paintStatus)
    {
        var token = await _auth.GetTokenAsync();
        if (string.IsNullOrEmpty(token))
            throw new UnauthorizedAccessException("User is not authenticated.");
        _http.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        await _http.PatchAsJsonAsync($"miniatures/{id}/status", new
        {
            paintStatus
        });
    }

    public async Task DeleteMiniatureAsync(Guid id)
    {
        var token = await _auth.GetTokenAsync();
        if (string.IsNullOrEmpty(token))
            throw new UnauthorizedAccessException("User is not authenticated.");
        _http.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        await _http.DeleteAsync($"miniatures/{id}");
    }
}
