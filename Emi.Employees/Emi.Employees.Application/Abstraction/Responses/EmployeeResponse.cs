using Emi.Employees.Domain.ValueObjects;

namespace Emi.Employees.Application.Abstraction.Responses;

public class EmployeeResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Position { get; set; }
    public decimal Salary { get; set; }
    public decimal Bonus
    {
        get
        {
            decimal bonusPercentage = Position == EmployeePosition.Manager.ToString() ? 0.20m : 0.10m;
            return Salary * bonusPercentage;
        }
    }
}
