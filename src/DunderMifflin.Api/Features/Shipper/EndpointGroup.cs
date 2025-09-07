using DunderMifflin.Shared.Data;
using Microsoft.EntityFrameworkCore;

namespace DunderMifflin.Api.Features.Shipper;

public class EndpointGroup : IEndpointGroup
{
    public void Map(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/shippers");

        group.MapGet("", async (DunderMifflinDbContext db) =>
            await db.Shippers.ToListAsync());

        group.MapGet("/{id:int}", async (int id, DunderMifflinDbContext db) =>
            await db.Shippers.FindAsync(id) is var s && s != null
                ? Results.Ok(s)
                : Results.NotFound());
    }
}
