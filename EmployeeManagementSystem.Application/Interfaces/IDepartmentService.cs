using EmployeeManagementSystem.Application.DTOs;

namespace EmployeeManagementSystem.Application.Interfaces
{
    public interface IDepartmentService
    {
        Task<List<DepartmentDTO>> GetAllAsync();
        Task<DepartmentDTO?> GetByIdAsync(int id);
        Task AddAsync(DepartmentDTO dto);
        Task UpdateAsync(DepartmentDTO dto);
        Task DeleteAsync(int id);
        Task<bool> HasEmployeesAsync(int departmentId);
    }
}
