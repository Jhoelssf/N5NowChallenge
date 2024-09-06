using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

public class ApiIntegrationTests
{
    private readonly HttpClient _client;

    public ApiIntegrationTests()
    {
        // Initialize HttpClient to test the API
        _client = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5253")
        };
    }

    [Fact]
    public async Task TestGetEndpoint()
    {
        // Act
        var response = await _client.GetAsync("/api/permissions");

        // Assert
        response.EnsureSuccessStatusCode();
        var responseData = await response.Content.ReadAsStringAsync();
    }

    [Fact]
    public async Task TestPostEndpoint()
    {
        // Arrange
        var newPermission = new
        {
            EmployeeForename = "John",
            EmployeeSurname = "Doe",
            PermissionTypeId = 1,
            PermissionDate = "2024-09-05"
        };

        var content = new StringContent(
            JsonSerializer.Serialize(newPermission),
            Encoding.UTF8,
            "application/json");

        // Act
        var response = await _client.PostAsync("/api/permissions", content);

        // Assert
        response.EnsureSuccessStatusCode();
        var responseData = await response.Content.ReadAsStringAsync();
    }

    [Fact]
    public async Task TestPutEndpoint()
    {
        // Arrange
        var updatedPermission = new
        {
            EmployeeForename = "Jane",
            EmployeeSurname = "Doe",
            PermissionTypeId = 2,
            PermissionDate = "2024-09-06"
        };

        var content = new StringContent(
            JsonSerializer.Serialize(updatedPermission),
            Encoding.UTF8,
            "application/json");

        // Act
        var response = await _client.PutAsync("/api/permissions/1", content);

        // Assert
        response.EnsureSuccessStatusCode();
        var responseData = await response.Content.ReadAsStringAsync();
    }

}
