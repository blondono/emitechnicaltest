using AutoMapper;
using Emi.Employees.Application.Abstraction.Responses;
using Emi.Employees.Domain.Entities;
using Emi.Employees.Domain.Shared;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Emi.Employees.Application.Modules.Employees.GetEmployees;

internal class GetEmployeesHandler(
    IRepository<Employee> repository,
    IMapper mapper
    )
    : IRequestHandler<GetEmployeesCommand, ErrorOr<List<EmployeeResponse>>>
{
    public async Task<ErrorOr<List<EmployeeResponse>>> Handle(GetEmployeesCommand request, CancellationToken cancellationToken)
    =>
        mapper.Map<List<EmployeeResponse>>(
            await repository.GetAllAsync(
                x => x.Id == (request.EmployeeId.HasValue ? request.EmployeeId.Value : x.Id),
                        include: q => q.Include(e => e.Department)
                                        .Include(e => e.Project)
                                        .Include(e => e.Position), null, 
                        cancellationToken)
            );
}
