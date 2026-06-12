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
        }).RequireAuthorization();

        app.MapPost("/miniatures", async (WargearDbContext db, Miniature miniature) =>
        {
            db.Miniatures.Add(miniature);
            await db.SaveChangesAsync();
            return Results.Created($"/miniatures/{miniature.Id}", miniature);
        }).RequireAuthorization();

        app.MapPatch("/miniatures/{id}/status", async (WargearDbContext db, Guid id, PaintStatus status) =>
        {
            var miniature = await db.Miniatures.FindAsync(id);
            if (miniature == null)
            {
                return Results.NotFound();
            }
            miniature.Status = status;
            await db.SaveChangesAsync();
            return Results.NoContent();
        }).RequireAuthorization();

        app.MapDelete("/miniatures/{id}", async (WargearDbContext db, Guid id) =>
        {
            var miniature = await db.Miniatures.FindAsync(id);
            if (miniature == null)
            {
                return Results.NotFound();
            }
            db.Miniatures.Remove(miniature);
            await db.SaveChangesAsync();
            return Results.NoContent();
        }).RequireAuthorization();
    }
}
