using System.Net;
using System.Text;
using System.Text.Json;
using DunderMifflin.Mcp.Remote.Tools;
using DunderMifflin.Shared.Models;

namespace DunderMifflin.Tests;

public class EmployeesToolRemoteTests
{
    // Create a testable wrapper for HttpMessageHandler
    private class TestHttpMessageHandler : HttpMessageHandler
    {
        private readonly Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> _sendAsync;

        public TestHttpMessageHandler(Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> sendAsync)
        {
            _sendAsync = sendAsync;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return _sendAsync(request, cancellationToken);
        }
    }

    private static List<Employee> GetTestEmployees()
    {
        return new List<Employee>
        {
            new() { Employeeid = 1, Firstname = "Michael", Lastname = "Scott", Title = "Regional Manager" },
            new() { Employeeid = 2, Firstname = "Dwight", Lastname = "Schrute", Title = "Assistant Regional Manager" },
            new() { Employeeid = 3, Firstname = "Jim", Lastname = "Halpert", Title = "Sales Representative" },
            new() { Employeeid = 4, Firstname = "Pam", Lastname = "Beesly", Title = "Receptionist" },
            new() { Employeeid = 5, Firstname = "Ryan", Lastname = "Howard", Title = "Temp" }
        };
    }

    private static HttpClient CreateMockedHttpClient(List<Employee> employees)
    {
        var json = JsonSerializer.Serialize(employees);
        var handler = new TestHttpMessageHandler((request, cancellationToken) =>
        {
            return Task.FromResult(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            });
        });

        return new HttpClient(handler);
    }

    private static HttpClient CreateMockedHttpClient(HttpStatusCode statusCode, string content = "")
    {
        var handler = new TestHttpMessageHandler((request, cancellationToken) =>
        {
            return Task.FromResult(new HttpResponseMessage
            {
                StatusCode = statusCode,
                Content = new StringContent(content, Encoding.UTF8, "application/json")
            });
        });

        return new HttpClient(handler);
    }

    [Fact]
    public async Task GetEmployees_WithoutLimit_ReturnsAllEmployees()
    {
        // Arrange
        var employees = GetTestEmployees();
        var httpClient = CreateMockedHttpClient(employees);
        var tool = new EmployeesTool(httpClient);

        // Act
        var result = await tool.GetEmployees();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(5, result.Count);
    }

    [Fact]
    public async Task GetEmployees_WithValidLimit_ReturnsLimitedEmployees()
    {
        // Arrange
        var employees = GetTestEmployees();
        var httpClient = CreateMockedHttpClient(employees);
        var tool = new EmployeesTool(httpClient);

        // Act
        var result = await tool.GetEmployees(limit: 3);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(3, result.Count);
        Assert.Equal(1, result[0].Employeeid);
        Assert.Equal(2, result[1].Employeeid);
        Assert.Equal(3, result[2].Employeeid);
    }

    [Fact]
    public async Task GetEmployees_WithZeroLimit_ReturnsAllEmployees()
    {
        // Arrange
        var employees = GetTestEmployees();
        var httpClient = CreateMockedHttpClient(employees);
        var tool = new EmployeesTool(httpClient);

        // Act
        var result = await tool.GetEmployees(limit: 0);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(5, result.Count);
    }

    [Fact]
    public async Task GetEmployees_WithNegativeLimit_ReturnsAllEmployees()
    {
        // Arrange
        var employees = GetTestEmployees();
        var httpClient = CreateMockedHttpClient(employees);
        var tool = new EmployeesTool(httpClient);

        // Act
        var result = await tool.GetEmployees(limit: -1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(5, result.Count);
    }

    [Fact]
    public async Task GetEmployees_WithLimitGreaterThanTotalEmployees_ReturnsAllEmployees()
    {
        // Arrange
        var employees = GetTestEmployees();
        var httpClient = CreateMockedHttpClient(employees);
        var tool = new EmployeesTool(httpClient);

        // Act
        var result = await tool.GetEmployees(limit: 10);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(5, result.Count);
    }

    [Fact]
    public async Task GetEmployees_WithNullResponse_ReturnsEmptyList()
    {
        // Arrange
        var httpClient = CreateMockedHttpClient(HttpStatusCode.OK, "null");
        var tool = new EmployeesTool(httpClient);

        // Act
        var result = await tool.GetEmployees();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetEmployees_WhenHttpRequestFails_ThrowsHttpRequestException()
    {
        // Arrange
        var httpClient = CreateMockedHttpClient(HttpStatusCode.InternalServerError);
        var tool = new EmployeesTool(httpClient);

        // Act & Assert
        await Assert.ThrowsAsync<HttpRequestException>(() => tool.GetEmployees());
    }
}
