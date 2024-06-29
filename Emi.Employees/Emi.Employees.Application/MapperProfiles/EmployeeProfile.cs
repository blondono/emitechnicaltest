using AutoMapper;
using Emi.Employees.Application.Abstraction.Request;
using Emi.Employees.Application.Abstraction.Responses;
using Emi.Employees.Domain.Entities;

namespace Emi.Employees.Application.MapperProfiles;

public class EmployeeProfile : Profile
{
    public EmployeeProfile()
    {
        CreateMap<EmployeeRequest, Employee>()
            .ForMember(dest => dest.Id, opts => opts.Ignore())
            .ForMember(dest => dest.DepartmentId, opts => opts.MapFrom(src => src.DepartmentId))
            .ForMember(dest => dest.ProjectId, opts => opts.MapFrom(src => src.ProjectId))
            .ForMember(dest => dest.CurrentPosition, opts => opts.MapFrom(src => src.Position))
            .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
            .ForMember(dest => dest.Salary, opts => opts.MapFrom(src => src.Salary));

        CreateMap<Employee, EmployeeResponse>();

    }
}
