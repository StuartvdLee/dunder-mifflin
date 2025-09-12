using System.ComponentModel;
using ModelContextProtocol.Server;

namespace DunderMifflin.Mcp.Remote.Resources;

[McpServerResourceType]
public class Resources
{
    [McpServerResource(MimeType = "text/plain")]
    [Description("Returns the API URI")]
    public static string GetApiUri()
    {
        return "https://dundermifflin-api.azurewebsites.net";
    }
}
