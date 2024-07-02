using ErrorOr;
using MediatR;

namespace Emi.Employees.Application.Modules.PositionHistory.SavePositionHistory;

public class SavePositionHistoryCommand : IRequest<ErrorOr<Success>>
{
    public int EmployeeId { get; set; }
    public int DepartmentId { get; set; }
    public int PositionId { get; set; }
    public int ProjectId { get; set; }
}
