using DunderMifflin.Shared.Data;
using Microsoft.EntityFrameworkCore;

namespace DunderMifflin.Api.Features.OrderDetail;

public class EndpointGroup : IEndpointGroup
{
    public void Map(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/orderdetails");

        group.MapGet("", async (DunderMifflinDbContext db) =>
            await db.Orderdetails.ToListAsync());
    }
}
