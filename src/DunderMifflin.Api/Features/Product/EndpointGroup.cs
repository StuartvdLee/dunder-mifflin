using DunderMifflin.Shared.Data;
using Microsoft.EntityFrameworkCore;

namespace DunderMifflin.Api.Features.Product;

public class EndpointGroup : IEndpointGroup
{
    public void Map(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/products");

        group.MapGet("", async (DunderMifflinDbContext db) =>
            await db.Products.ToListAsync());

        group.MapGet("/{id:int}", async (int id, DunderMifflinDbContext db) =>
            await db.Products.FindAsync(id) is var p && p != null
                ? Results.Ok(p)
                : Results.NotFound());
    }
}
