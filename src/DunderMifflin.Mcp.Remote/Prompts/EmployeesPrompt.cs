using System.ComponentModel;
using Microsoft.Extensions.AI;
using ModelContextProtocol.Server;

namespace DunderMifflin.Mcp.Remote.Prompts;

[McpServerPromptType]
public class EmployeesPrompt
{
    [McpServerPrompt]
    [Description("Prompt to get a list of Dunder Mifflin employees with a maximum limit.")]
    public ChatMessage GetEmployees([Description("Employee limit")] int limit)
    {
        return new ChatMessage(ChatRole.User, $"Get a list of {limit} Dunder Mifflin employees");
    }
}
