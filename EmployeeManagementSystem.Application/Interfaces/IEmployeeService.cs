using EmployeeManagementSystem.Application.Common;
using EmployeeManagementSystem.Application.DTOs;

namespace EmployeeManagementSystem.Application.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<EmployeeDTO>> GetAllAsync();
        Task<EmployeeDTO?> GetByIdAsync(int id);
        Task AddAsync(EmployeeDTO employee);
        Task UpdateAsync(EmployeeDTO employee);
        Task DeleteAsync(int id);
        Task<PagedResult<EmployeeDTO>> GetPagedAsync(int page, int pageSize, string? sortBy, bool desc, string? search);
    }
}
