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
        // Search up the directory tree for the misc folder (with max depth limit)
        var baseDirectory = AppContext.BaseDirectory;
        var currentDir = new DirectoryInfo(baseDirectory);
        const int maxDepth = 10; // Reasonable limit to prevent excessive traversal
        var depth = 0;
        
        while (currentDir != null && depth < maxDepth)
        {
            var miscPath = Path.Combine(currentDir.FullName, "misc", "employee_handbook.md");
            if (File.Exists(miscPath))
            {
                return File.ReadAllText(miscPath, Encoding.UTF8);
            }
            currentDir = currentDir.Parent;
            depth++;
        }
        
        throw new FileNotFoundException("Handbook not found. Searched up from: " + baseDirectory);
    }
}
