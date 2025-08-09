using EmployeeManagementSystem.Application.DTOs;

namespace EmployeeManagementSystem.Application.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardViewModel> GetAsync(string? search, CancellationToken ct = default);
    }
}
