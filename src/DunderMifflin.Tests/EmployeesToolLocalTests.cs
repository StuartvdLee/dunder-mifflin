using DunderMifflin.Mcp.Local.Tools;
using DunderMifflin.Shared.Data;
using DunderMifflin.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace DunderMifflin.Tests;

public class EmployeesToolLocalTests
{
    private static DunderMifflinDbContext CreateInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<DunderMifflinDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new DunderMifflinDbContext(options);
    }

    private static void SeedTestData(DunderMifflinDbContext context)
    {
        var employees = new List<Employee>
        {
            new Employee { Employeeid = 1, Firstname = "Michael", Lastname = "Scott", Title = "Regional Manager" },
            new Employee { Employeeid = 2, Firstname = "Dwight", Lastname = "Schrute", Title = "Assistant Regional Manager" },
            new Employee { Employeeid = 3, Firstname = "Jim", Lastname = "Halpert", Title = "Sales Representative" },
            new Employee { Employeeid = 4, Firstname = "Pam", Lastname = "Beesly", Title = "Receptionist" },
            new Employee { Employeeid = 5, Firstname = "Ryan", Lastname = "Howard", Title = "Temp" }
        };

        context.Employees.AddRange(employees);
        context.SaveChanges();
    }

    [Fact]
    public async Task GetEmployees_WithoutLimit_ReturnsAllEmployees()
    {
        // Arrange
        using var context = CreateInMemoryDbContext();
        SeedTestData(context);

        // Act
        var result = await EmployeesTool.GetEmployees(context, null);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(5, result.Count);
    }

    [Fact]
    public async Task GetEmployees_WithValidLimit_ReturnsLimitedEmployees()
    {
        // Arrange
        using var context = CreateInMemoryDbContext();
        SeedTestData(context);

        // Act
        var result = await EmployeesTool.GetEmployees(context, 3);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(3, result.Count);
    }

    [Fact]
    public async Task GetEmployees_WithZeroLimit_ReturnsAllEmployees()
    {
        // Arrange
        using var context = CreateInMemoryDbContext();
        SeedTestData(context);

        // Act
        var result = await EmployeesTool.GetEmployees(context, 0);

        // Assert - zero is not > 0, so no limit is applied and all employees are returned
        Assert.NotNull(result);
        Assert.Equal(5, result.Count);
    }

    [Fact]
    public async Task GetEmployees_WithNegativeLimit_ReturnsAllEmployees()
    {
        // Arrange
        using var context = CreateInMemoryDbContext();
        SeedTestData(context);

        // Act
        var result = await EmployeesTool.GetEmployees(context, -1);

        // Assert - negative is not > 0, so no limit is applied and all employees are returned
        Assert.NotNull(result);
        Assert.Equal(5, result.Count);
    }

    [Fact]
    public async Task GetEmployees_WithLimitGreaterThanTotalEmployees_ReturnsAllEmployees()
    {
        // Arrange
        using var context = CreateInMemoryDbContext();
        SeedTestData(context);

        // Act
        var result = await EmployeesTool.GetEmployees(context, 10);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(5, result.Count);
    }

    [Fact]
    public async Task GetEmployees_WithEmptyDatabase_ReturnsEmptyList()
    {
        // Arrange
        using var context = CreateInMemoryDbContext();

        // Act
        var result = await EmployeesTool.GetEmployees(context, null);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }
}
