using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WargearTracker.Core;
using WargearTracker.Data;

namespace WargearTracker.Api.Endpoints;
public static class ArmyEnpoints
{
    public static void MapArmyEndpoints(this WebApplication app)
    {
        app.MapGet("/armies", async (WargearDbContext db) =>
        {
            return await db.Armies.ToListAsync();
        });

        app.MapPost("/armies", async (WargearDbContext db, Army army) =>
        {
            db.Armies.Add(army);
            await db.SaveChangesAsync();
            return Results.Created($"/armies/{army.Id}", army);
        });

        app.MapGet("/armies/{id}", async (WargearDbContext db, int id) =>
        {
            var army = await db.Armies.FindAsync(id);
            return army != null ? Results.Ok(army) : Results.NotFound();
        });
    }
}
