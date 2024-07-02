using Emi.Employees.Application.Abstraction.Responses;
using ErrorOr;
using MediatR;

namespace Emi.Employees.Application.Modules.PositionHistory.GetPositionHistory;

public class GetPositionHistoryCommand : IRequest<ErrorOr<PositionHistoryResponse>>
{
    public int EmployeeId { get; set; } 
}
