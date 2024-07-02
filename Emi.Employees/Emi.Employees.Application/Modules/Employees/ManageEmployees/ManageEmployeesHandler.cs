using AutoMapper;
using Emi.Employees.Application.Modules.PositionHistory.SavePositionHistory;
using Emi.Employees.Domain.Entities;
using Emi.Employees.Domain.Shared;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Emi.Employees.Application.Modules.Employees.ManageEmployees;

internal class ManageEmployeesHandler(
    UserManager<Employee> repository,
    IRepository<Domain.Entities.PositionHistory> positionHistoryRepository,
    IMapper mapper,
    ISender sender
    )
    : IRequestHandler<ManageEmployeesCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(ManageEmployeesCommand request, CancellationToken cancellationToken)
    {
        Employee employee = null;
        IdentityResult identityResult = null;
        switch (request.OperationType)
        {
            case Common.OperationType.Insert:
                employee = mapper.Map<Employee>(request.Employee);
                identityResult = await repository.CreateAsync(employee, request.Employee.Password);
                if (identityResult.Errors.Any())
                    return Error.Failure("Employee.Create.Failure", string.Join(" | ", identityResult.Errors.Select(e => e.Description)));
                break;
            case Common.OperationType.Update:
                employee = await repository.FindByIdAsync(request.EmployeeId.ToString());
                if (employee == null)
                    return Error.Failure("Employee.Create.Failure", "User not found");

                employee.Salary = request.Employee.Salary;
                employee.Name = request.Employee.Name;
                employee.DepartmentId = request.Employee.DepartmentId;
                employee.ProjectId = request.Employee.ProjectId;
                employee.PositionId = request.Employee.PositionId;
                employee.Id = request.EmployeeId.Value;

                identityResult = await repository.UpdateAsync(employee);
                if (identityResult.Errors.Any())
                    return Error.Failure("Employee.Update.Failure", string.Join(" | ", identityResult.Errors.Select(e => e.Description)));
                break;
            default:
                employee = await repository.FindByIdAsync(request.EmployeeId.ToString());
                if (employee == null)
                    return Error.Failure("Employee.Create.Failure", "User not found");

                var history = await positionHistoryRepository.GetAllAsync();
                await positionHistoryRepository.DeleteRangeAsync(history);

                identityResult = await repository.DeleteAsync(employee);
                if (identityResult.Errors.Any())
                    return Error.Failure("Employee.Delete.Failure", string.Join(" | ", identityResult.Errors.Select(e => e.Description)));
                break;
        }

        if (request.OperationType != Common.OperationType.Delete)
            await sender.Send(
                new SavePositionHistoryCommand
                {
                    EmployeeId = employee.Id,
                    DepartmentId = employee.DepartmentId,
                    PositionId = employee.PositionId,
                    ProjectId = employee.ProjectId
                }, cancellationToken);

        return Result.Success;
    }
}
