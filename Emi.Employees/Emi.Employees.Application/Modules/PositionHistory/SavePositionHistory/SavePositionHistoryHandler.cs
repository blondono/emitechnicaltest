using AutoMapper;
using Emi.Employees.Domain.Shared;
using ErrorOr;
using MediatR;

namespace Emi.Employees.Application.Modules.PositionHistory.SavePositionHistory;

public class SavePositionHistoryHandler(
        IRepository<Domain.Entities.PositionHistory> repository,
        IMapper mapper
        )
        : IRequestHandler<SavePositionHistoryCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(SavePositionHistoryCommand request, CancellationToken cancellationToken)
    {
        var lastPosition = (await repository.GetAllAsync(x => x.EmployeeId == request.EmployeeId,
                                        null,
                                        orderBy: p => p.OrderByDescending(x => x.StartDate),
                                        cancellationToken)).FirstOrDefault();

        if (lastPosition != null)
        {
            lastPosition.EndDate = DateTime.UtcNow;
            await repository.UpdateAsync(lastPosition, cancellationToken);

        }
        var currentPosition = mapper.Map<Domain.Entities.PositionHistory>(request);
        await repository.InsertAsync(currentPosition, cancellationToken);

        return Result.Success;


    }
}
