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
        }).RequireAuthorization();

        app.MapPost("/armies", async (WargearDbContext db, Army army) =>
        {
            db.Armies.Add(army);
            await db.SaveChangesAsync();
            return Results.Created($"/armies/{army.Id}", army);
        }).RequireAuthorization();

        app.MapGet("/armies/{id}", async (WargearDbContext db, Guid id) =>
        {
            var army = await db.Armies.FindAsync(id);
            return army != null ? Results.Ok(army) : Results.NotFound();
        }).RequireAuthorization();

        app.MapDelete("/armies/{id}", async (WargearDbContext db, Guid id) =>
        {
            var army = await db.Armies.FindAsync(id);
            if (army == null)
            {
                return Results.NotFound();
            }
            db.Armies.Remove(army);
            await db.SaveChangesAsync();
            return Results.NoContent();
        }).RequireAuthorization();
    }
}
