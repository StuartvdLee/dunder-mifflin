using DunderMifflin.Shared.Data;
using Microsoft.EntityFrameworkCore;

namespace DunderMifflin.Api.Features.Category;

public class EndpointGroup : IEndpointGroup
{
    public void Map(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/categories");

        group.MapGet("", async (DunderMifflinDbContext db) =>
            await db.Categories.ToListAsync());

        group.MapGet("/{id:int}", async (int id, DunderMifflinDbContext db) =>
            await db.Categories.FindAsync(id) is var c && c != null
                ? Results.Ok(c)
                : Results.NotFound());
    }
}
