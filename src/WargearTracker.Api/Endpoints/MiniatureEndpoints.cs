using Microsoft.EntityFrameworkCore;
using WargearTracker.Data;

namespace WargearTracker.Api.Endpoints;

public static class MiniatureEndpoints
{
    public static void MapMiniatureEndpoints(this WebApplication app)
    {
        app.MapGet("/miniatures", async (WargearDbContext db) =>
        {
            return await db.Miniatures.ToListAsync();
        });
    }
}
