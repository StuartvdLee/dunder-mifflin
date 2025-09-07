using DunderMifflin.Shared.Data;
using Microsoft.EntityFrameworkCore;

namespace DunderMifflin.Api.Features.Customer;

public class EndpointGroup : IEndpointGroup
{
    public void Map(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/customers");

        group.MapGet("", async (DunderMifflinDbContext db) =>
            await db.Customers.ToListAsync());

        group.MapGet("/{id:int}", async (int id, DunderMifflinDbContext db) =>
            await db.Customers.FindAsync(id) is var c && c != null
                ? Results.Ok(c)
                : Results.NotFound());
    }
}
