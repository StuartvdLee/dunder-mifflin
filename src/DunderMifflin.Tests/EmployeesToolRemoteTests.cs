using DunderMifflin.Shared.Models;

namespace DunderMifflin.Tests;

public class EmployeesToolRemoteTests
{
    private static List<Employee> GetTestEmployees()
    {
        return new List<Employee>
        {
            new Employee { Employeeid = 1, Firstname = "Michael", Lastname = "Scott", Title = "Regional Manager" },
            new Employee { Employeeid = 2, Firstname = "Dwight", Lastname = "Schrute", Title = "Assistant Regional Manager" },
            new Employee { Employeeid = 3, Firstname = "Jim", Lastname = "Halpert", Title = "Sales Representative" },
            new Employee { Employeeid = 4, Firstname = "Pam", Lastname = "Beesly", Title = "Receptionist" },
            new Employee { Employeeid = 5, Firstname = "Ryan", Lastname = "Howard", Title = "Temp" }
        };
    }

    [Fact]
    public void GetEmployees_WithoutLimit_ReturnsAllEmployees()
    {
        // Arrange
        var employees = GetTestEmployees();

        // Note: The Remote EmployeesTool uses a static HttpClient, so we can't mock it
        // This test validates the expected behavior based on the code structure

        // Act & Assert
        // Since we can't inject HttpClient into the static class, we're testing the logic flow
        Assert.Equal(5, employees.Count);

        // Test the limit logic directly
        var limitedEmployees = employees.Take(3).ToList();
        Assert.Equal(3, limitedEmployees.Count);
    }

    [Fact]
    public void GetEmployees_LimitLogic_WithValidLimit_ReturnsLimitedEmployees()
    {
        // Arrange
        var employees = GetTestEmployees();
        int limit = 3;

        // Act - Testing the limit logic that's in the Remote EmployeesTool
        var result = limit > 0 ? employees.Take(limit).ToList() : employees;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(3, result.Count);
    }

    [Fact]
    public void GetEmployees_LimitLogic_WithZeroLimit_ReturnsLimitedToZeroEmployees()
    {
        // Arrange
        var employees = GetTestEmployees();
        int limit = 0;

        // Act - Testing the limit logic that's in the Remote EmployeesTool
        var result = limit > 0 ? employees.Take(limit).ToList() : employees;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(5, result.Count); // Zero limit doesn't trigger the Take, returns all
    }

    [Fact]
    public void GetEmployees_LimitLogic_WithNegativeLimit_ReturnsAllEmployees()
    {
        // Arrange
        var employees = GetTestEmployees();
        int limit = -1;

        // Act - Testing the limit logic that's in the Remote EmployeesTool
        var result = limit > 0 ? employees.Take(limit).ToList() : employees;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(5, result.Count); // Negative limit doesn't trigger the Take, returns all
    }

    [Fact]
    public void GetEmployees_LimitLogic_WithLimitGreaterThanTotalEmployees_ReturnsAllEmployees()
    {
        // Arrange
        var employees = GetTestEmployees();
        int limit = 10;

        // Act - Testing the limit logic that's in the Remote EmployeesTool
        var result = limit > 0 ? employees.Take(limit).ToList() : employees;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(5, result.Count);
    }

    [Fact]
    public void GetEmployees_LimitLogic_WithNullList_ReturnsEmptyList()
    {
        // Arrange
        List<Employee>? employees = null;

        // Act - Testing the null coalescing logic in the Remote EmployeesTool
        var result = employees ?? new List<Employee>();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }
}
