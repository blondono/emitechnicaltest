using System.Text.Json.Serialization;
using Emi.Employees.Application.Common;
using Emi.Employees.Domain.ValueObjects;

namespace Emi.Employees.Application.Abstraction.Request;

public class EmployeeRequest
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("email")]
    public required string Email { get; set; }

    [JsonPropertyName("password")]
    public required string Password { get; set; }

    [JsonPropertyName("position")]
    public required int PositionId { get; set; }

    [JsonPropertyName("salary")]
    public required decimal Salary { get; set; }

    [JsonPropertyName("departmentId")]
    public required int DepartmentId { get; set; }

    [JsonPropertyName("projectId")]
    public required int ProjectId { get; set; }
}
