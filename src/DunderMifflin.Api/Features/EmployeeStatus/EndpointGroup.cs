using DunderMifflin.Shared.Data;
using Microsoft.EntityFrameworkCore;

namespace DunderMifflin.Api.Features.EmployeeStatus;

public class EndpointGroup : IEndpointGroup
{
    public void Map(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/employeestatuses");

        group.MapGet("", async (DunderMifflinDbContext db) =>
            await db.Employeestatuses.ToListAsync());

        group.MapGet("/{id:int}", async (int id, DunderMifflinDbContext db) =>
            await db.Employeestatuses.FindAsync(id) is var es && es != null
                ? Results.Ok(es)
                : Results.NotFound());
    }
}
