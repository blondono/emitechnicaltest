using AutoMapper;
using Emi.Employees.Application.Abstraction.Request;
using Emi.Employees.Application.Abstraction.Responses;
using Emi.Employees.Application.Modules.PositionHistory.SavePositionHistory;
using Emi.Employees.Domain.Entities;

namespace Emi.Employees.Application.MapperProfiles;

public class PositionHistoryProfile : Profile
{
    public PositionHistoryProfile()
    {
        CreateMap<PositionHistory, History>()
            .ForMember(dest => dest.Department, opts => opts.MapFrom(src => src.Department.Name))
            .ForMember(dest => dest.Project, opts => opts.MapFrom(src => src.Project.Name))
            .ForMember(dest => dest.Position, opts => opts.MapFrom(src => src.Position.Name));

        CreateMap<Employee, History>()
            .ForMember(dest => dest.Department, opts => opts.MapFrom(src => src.Department.Name))
            .ForMember(dest => dest.Project, opts => opts.MapFrom(src => src.Project.Name))
            .ForMember(dest => dest.Position, opts => opts.MapFrom(src => src.Position.Name));

        CreateMap<SavePositionHistoryCommand, PositionHistory>()
            .ForMember(dest => dest.StartDate, opts => opts.MapFrom(src => DateTime.Now));

    }
}
