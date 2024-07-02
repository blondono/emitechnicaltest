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
            .ForMember(dest => dest.PositionId, opt => opt.MapFrom(src => src.PositionId))
            .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
            .ForMember(dest => dest.Salary, opts => opts.MapFrom(src => src.Salary))
            .ForMember(dest => dest.Email, opts => opts.MapFrom(src => src.Email))
            .ForMember(dest => dest.UserName, opts => opts.MapFrom(src => src.Email));

        CreateMap<Employee, EmployeeResponse>()
            .ForMember(dest => dest.Position, opts => opts.MapFrom(src => src.Position.Name))
            .ForMember(dest => dest.Department, opts => opts.MapFrom(src => src.Department.Name))
            .ForMember(dest => dest.Project, opts => opts.MapFrom(src => src.Project.Name))
            .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
            .ForMember(dest => dest.Salary, opts => opts.MapFrom(src => src.Salary));

    }
}
