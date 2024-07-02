using AutoMapper;
using Emi.Employees.Application.Abstraction.Responses;
using Emi.Employees.Domain.Shared;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Emi.Employees.Application.Modules.PositionHistory.GetPositionHistory;

public class GetPositionHistoryHandler(
    IRepository<Domain.Entities.PositionHistory> positionHistoryRepository,
    IRepository<Domain.Entities.Employee> employeeRepository,
    IMapper mapper
    )
    : IRequestHandler<GetPositionHistoryCommand, ErrorOr<PositionHistoryResponse>>
{
    public async Task<ErrorOr<PositionHistoryResponse>> Handle(GetPositionHistoryCommand request, CancellationToken cancellationToken)
    {
        var employeeQuery = await employeeRepository.GetAllAsync(x => x.Id == request.EmployeeId,
            include: i => i.Include(d => d.Department)
                            .Include(p => p.Project)
                            .Include(p => p.Position), null,
            cancellationToken);

        if (!employeeQuery.Any())
            return new ErrorOr<PositionHistoryResponse>();

        var employee = employeeQuery.FirstOrDefault();

        var result = await positionHistoryRepository.GetAllAsync(x => x.EmployeeId == request.EmployeeId,
            include: q => q.Include(e => e.Department)
                            .Include(e => e.Employee)
                            .Include(e => e.Project)
                            .Include(e => e.Position),
            orderBy: q => q.OrderByDescending(e => e.StartDate)
            );

        var response = new PositionHistoryResponse()
        {
            Name = employee.Name,
            History = mapper.Map<List<History>>(result)
        };

        return response;
    }
}
