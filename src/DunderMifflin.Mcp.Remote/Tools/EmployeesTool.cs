using System.ComponentModel;
using System.Text.Json;
using DunderMifflin.Api.Models;
using ModelContextProtocol.Server;

namespace DunderMifflin.Mcp.Remote.Tools;

[McpServerToolType]
public static class EmployeesTool
{
    [McpServerTool(Name = "GetEmployees")]
    [Description(
        "Gets a list of Dunder Mifflin employees. Optionally takes a limit of the number of employees to return.")]
    public static async Task<List<Employee>> GetEmployees(int? limit = null)
    {
        using var httpClient = new HttpClient();
        var response = await httpClient.GetAsync("https://dundermifflin-api.azurewebsites.net/employees");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        var employees = JsonSerializer.Deserialize<List<Employee>>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (limit is > 0) employees = employees?.Take(limit.Value).ToList();

        return employees ?? new List<Employee>();
    }
}
