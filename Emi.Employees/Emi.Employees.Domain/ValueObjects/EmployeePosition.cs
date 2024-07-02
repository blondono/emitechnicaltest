using Ardalis.SmartEnum;

namespace Emi.Employees.Domain.ValueObjects;

public sealed class EmployeePosition : SmartEnum<EmployeePosition>
{
    public static readonly EmployeePosition RegularEmployee = new("REGULAR", 1);
    public static readonly EmployeePosition Manager = new("MANAGER", 2);

    public EmployeePosition(string name, int value)
        : base(name, value)
    {
    }
}
