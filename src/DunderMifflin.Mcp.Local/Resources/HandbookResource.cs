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
        // Search up the directory tree for the misc folder
        var baseDirectory = AppContext.BaseDirectory;
        var currentDir = new DirectoryInfo(baseDirectory);
        
        while (currentDir != null)
        {
            var miscPath = Path.Combine(currentDir.FullName, "misc", "employee_handbook.md");
            if (File.Exists(miscPath))
            {
                return File.ReadAllText(miscPath, Encoding.UTF8);
            }
            currentDir = currentDir.Parent;
        }
        
        throw new FileNotFoundException("Handbook not found. Searched up from: " + baseDirectory);
    }
}
