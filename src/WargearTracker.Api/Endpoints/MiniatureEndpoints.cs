using Microsoft.EntityFrameworkCore;
using WargearTracker.Core;
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

        app.MapPost("/miniatures", async (WargearDbContext db, Miniature miniature) =>
        {
            db.Miniatures.Add(miniature);
            await db.SaveChangesAsync();
            return Results.Created($"/miniatures/{miniature.Id}", miniature);
        });
    }
}
