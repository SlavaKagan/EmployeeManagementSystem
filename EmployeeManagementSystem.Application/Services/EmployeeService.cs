using AutoMapper;
using EmployeeManagementSystem.Application.Common;
using EmployeeManagementSystem.Application.DTOs;
using EmployeeManagementSystem.Application.Interfaces;
using EmployeeManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Application.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper _mapper;

    public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
    {
        _employeeRepository = employeeRepository;
        _mapper = mapper;
    }

    public async Task<List<EmployeeDTO>> GetAllAsync()
    {
        var employees = await _employeeRepository.GetAllAsync();
        return _mapper.Map<List<EmployeeDTO>>(employees);
    }

    public async Task<EmployeeDTO?> GetByIdAsync(int id)
    {
        var employee = await _employeeRepository.GetByIdAsync(id);
        return employee == null ? null : _mapper.Map<EmployeeDTO>(employee);
    }

    public async Task AddAsync(EmployeeDTO dto)
    {
        var employee = _mapper.Map<Employee>(dto);
        await _employeeRepository.AddAsync(employee);
    }

    public async Task UpdateAsync(EmployeeDTO dto)
    {
        var employee = _mapper.Map<Employee>(dto);
        await _employeeRepository.UpdateAsync(employee);
    }

    public async Task DeleteAsync(int id)
    {
        await _employeeRepository.DeleteAsync(id);
    }

    public async Task<PagedResult<EmployeeDTO>> GetPagedAsync(
    int page, int pageSize, string? sortBy, bool desc, string? search)
    {
        page = page <= 0 ? 1 : page;
        pageSize = pageSize <= 0 ? 10 : pageSize;

        var query = _employeeRepository.Query();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var s = $"%{search.Trim()}%";
            query = query.Where(e =>
                EF.Functions.Like(e.FirstName, s) ||
                EF.Functions.Like(e.LastName, s) ||
                EF.Functions.Like(e.Email, s) ||
                (e.Department != null && EF.Functions.Like(e.Department.Name, s)));
        }

        query = sortBy?.ToLower() switch
        {
            "firstname" => (desc ? query.OrderByDescending(e => e.FirstName) : query.OrderBy(e => e.FirstName)),
            "lastname" => (desc ? query.OrderByDescending(e => e.LastName) : query.OrderBy(e => e.LastName)),
            "email" => (desc ? query.OrderByDescending(e => e.Email) : query.OrderBy(e => e.Email)),
            "hiredate" => (desc ? query.OrderByDescending(e => e.HireDate) : query.OrderBy(e => e.HireDate)),
            "salary" => (desc ? query.OrderByDescending(e => e.Salary) : query.OrderBy(e => e.Salary)),
            "department" => (desc ? query.OrderByDescending(e => e.Department!.Name) : query.OrderBy(e => e.Department!.Name)),
            _ => query.OrderBy(e => e.Id)
        };

        var total = await query.CountAsync();
        var items = await query
            .AsNoTracking()
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Include(e => e.Department)
            .ToListAsync();

        var dtos = _mapper.Map<List<EmployeeDTO>>(items);

        return new PagedResult<EmployeeDTO>
        {
            Items = dtos,
            Page = page,
            PageSize = pageSize,
            TotalItems = total
        };
    }
}
