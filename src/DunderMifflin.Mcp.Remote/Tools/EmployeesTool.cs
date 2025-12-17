using System.ComponentModel;
using System.Text.Json;
using DunderMifflin.Shared.Models;
using ModelContextProtocol.Server;

namespace DunderMifflin.Mcp.Remote.Tools;

[McpServerToolType]
public class EmployeesTool(HttpClient httpClient)
{
    private readonly HttpClient _httpClient = httpClient;

    [McpServerTool(Name = "GetEmployees")]
    [Description(
        "Gets a list of Dunder Mifflin employees. Optionally takes a limit of the number of employees to return.")]
    public async Task<List<Employee>> GetEmployees(int? limit = null)
    {
        var response = await _httpClient.GetAsync("https://dundermifflin-api.azurewebsites.net/employees");
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
