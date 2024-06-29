using Emi.Employees.Application.Abstraction.Request;
using Emi.Employees.Application.Common;
using ErrorOr;
using MediatR;

namespace Emi.Employees.Application.Modules.ManageEmployees;

public class ManageEmployeesCommand : IRequest<ErrorOr<Success>>
{
    public int? EmployeeId { get; set; }
    public EmployeeRequest? Employee { get; set; }
    public OperationType OperationType { get; set; }
}
