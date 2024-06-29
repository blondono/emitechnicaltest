using System.Text.Json.Serialization;

namespace Emi.Employees.Application.Abstraction.Request;

public class EmployeeRequest
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("position")]
    public required string Position { get; set; }

    [JsonPropertyName("salary")]
    public required decimal Salary { get; set; }

    [JsonPropertyName("departmentId")]
    public required int DepartmentId { get; set; }

    [JsonPropertyName("projectId")]
    public required int ProjectId { get; set; }
}
