using System.ComponentModel;
using DunderMifflin.Shared.Data;
using DunderMifflin.Shared.Models;
using Microsoft.EntityFrameworkCore;
using ModelContextProtocol.Server;

namespace DunderMifflin.Mcp.Local.Tools;

[McpServerToolType]
public static class EmployeesTool
{
    [McpServerTool(Name = "GetEmployees")]
    [Description(
        "Gets a list of Dunder Mifflin employees. Optionally takes a limit of the number of employees to return.")]
    public static async Task<List<Employee>> GetEmployees(DunderMifflinDbContext dbContext, int? limit = null)
    {
        var query = dbContext.Employees.AsQueryable();

        if (limit is > 0) query = query.Take(limit.Value);

        return await query.ToListAsync();
    }
}
