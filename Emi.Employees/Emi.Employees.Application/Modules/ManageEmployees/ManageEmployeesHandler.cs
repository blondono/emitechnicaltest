using AutoMapper;
using Emi.Employees.Domain.Entities;
using Emi.Employees.Domain.Shared;
using ErrorOr;
using MediatR;

namespace Emi.Employees.Application.Modules.ManageEmployees
{
    internal class ManageEmployeesHandler(
        IRepository<Employee> repository,
        IMapper mapper
        )
        : IRequestHandler<ManageEmployeesCommand, ErrorOr<Success>>
    {
        public async Task<ErrorOr<Success>> Handle(ManageEmployeesCommand request, CancellationToken cancellationToken)
        {
            Employee employee = null;
            switch (request.OperationType)
            {
                case Common.OperationType.Insert:
                    employee = mapper.Map<Employee>(request.Employee);
                    await repository.InsertAsync(employee, cancellationToken);
                    break;
                case Common.OperationType.Update:
                    employee = mapper.Map<Employee>(request.Employee);
                    employee.Id = request.EmployeeId.Value;
                    await repository.UpdateAsync(employee, cancellationToken);
                    break;
                default:
                    employee = await repository.GetByIdAsync(request.EmployeeId.Value, cancellationToken);
                    await repository.DeleteAsync(employee, cancellationToken);
                    break;
            }
            return Result.Success;
        }
    }
}
