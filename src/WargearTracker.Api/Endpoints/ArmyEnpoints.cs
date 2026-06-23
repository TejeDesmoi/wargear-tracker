using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WargearTracker.Core;
using WargearTracker.Data;
using static WargearTracker.Api.DTOs.VisibilityDto;
using System.Text.RegularExpressions;
using WargearTracker.Core.Services;

namespace WargearTracker.Api.Endpoints;
public static class ArmyEnpoints
{
    public static void MapArmyEndpoints(this WebApplication app)
    {
        app.MapGet("/armies", async (WargearDbContext db) =>
        {
            return await db.Armies
                .Include(a => a.Miniatures)
                .ToListAsync();
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

        app.MapPatch("/armies/{id}/visibility", async (WargearDbContext db, Guid id, VisibilityRequest request) =>
        {
            var army = await db.Armies.FindAsync(id);
            if (army == null)
                return Results.NotFound();

            army.IsPublic = request.IsPublic;
            army.PublicSlug = request.IsPublic ? SlugService.GenerateSlug(army.Name) : null;

            await db.SaveChangesAsync();
            return Results.Ok(army);
        }).RequireAuthorization();

        app.MapGet("armies/public/{slug}", async (WargearDbContext db, string slug) =>
        {
            var army = await db.Armies
                .Include(a => a.Miniatures)
                .FirstOrDefaultAsync(a => a.PublicSlug == slug && a.IsPublic);

            return army != null ? Results.Ok(army) : Results.NotFound();
        });
    }
}
