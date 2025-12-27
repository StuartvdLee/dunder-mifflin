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
        // Don't forget to update the path when demoing!
        var path = "/Users/stuart/Repositories/DunderMifflin/src/misc/employee_handbook.md";
        if (!File.Exists(path))
            throw new FileNotFoundException("Handbook not found", path);

        return File.ReadAllText(path, Encoding.UTF8);
    }
}
