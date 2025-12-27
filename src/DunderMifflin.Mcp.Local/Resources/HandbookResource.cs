using System.ComponentModel;
using System.Text;
using ModelContextProtocol.Server;

namespace DunderMifflin.Mcp.Local.Resources;

[McpServerResourceType]
public class HandbookResource
{
    [McpServerResource(MimeType = "text/markdown", Name = "EmployeeHandbook")]
    [Description("Employee handbook overview")]
    public string EmployeeHandbook()
    {
        // Calculate path relative to the application's base directory
        var baseDirectory = AppContext.BaseDirectory;
        var path = Path.Combine(baseDirectory, "..", "..", "..", "..", "misc", "employee_handbook.md");
        var normalizedPath = Path.GetFullPath(path);
        
        if (!File.Exists(normalizedPath))
            throw new FileNotFoundException("Handbook not found", normalizedPath);

        return File.ReadAllText(normalizedPath, Encoding.UTF8);
    }
}
