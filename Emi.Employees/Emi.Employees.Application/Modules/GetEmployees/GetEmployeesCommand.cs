using Emi.Employees.Application.Abstraction.Responses;
using ErrorOr;
using MediatR;

namespace Emi.Employees.Application.Modules.GetEmployees;

public class GetEmployeesCommand : IRequest<ErrorOr<List<EmployeeResponse>>>
{
    public int? EmployeeId { get; set; }
}
