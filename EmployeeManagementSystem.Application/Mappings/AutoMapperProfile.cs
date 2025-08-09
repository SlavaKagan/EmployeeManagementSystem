using AutoMapper;
using EmployeeManagementSystem.Application.DTOs;
using EmployeeManagementSystem.Domain.Entities;

namespace EmployeeManagementSystem.Application.Mappings;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Employee, EmployeeDTO>()
            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department != null ? src.Department.Name : string.Empty))
            .ReverseMap();

        CreateMap<EmployeeDTO, Employee>()
            .ForMember(d => d.Department, o => o.Ignore());

        CreateMap<Department, DepartmentDTO>().ReverseMap();
    }
}