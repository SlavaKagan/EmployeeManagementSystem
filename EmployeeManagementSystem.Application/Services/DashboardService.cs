using EmployeeManagementSystem.Application.DTOs;
using EmployeeManagementSystem.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Application.Services
{
    public class DashboardService(IEmployeeRepository employeeRepo) : IDashboardService
    {
        private readonly IEmployeeRepository _employeeRepo = employeeRepo;

        public async Task<DashboardViewModel> GetAsync(string? search, CancellationToken ct = default)
        {
            var q = _employeeRepo.Query()
                                      .Include(e => e.Department)
                                      .Where(e => !e.IsDeleted);

            if (!string.IsNullOrWhiteSpace(search))
            {
                var s = $"%{search.Trim()}%";
                q = q.Where(e =>
                    EF.Functions.Like(e.FirstName, s) ||
                    EF.Functions.Like(e.LastName, s) ||
                    EF.Functions.Like(e.Email, s) ||
                    (e.Department != null && EF.Functions.Like(e.Department.Name, s)));
            }

            var totalEmployees = await q.CountAsync(ct);

            var byDept = await q
                .GroupBy(e => e.Department != null ? e.Department.Name : "Unassigned")
                .Select(g => new DepartmentCountItem
                {
                    DepartmentName = g.Key!,
                    Count = g.Count()
                })
                .OrderByDescending(x => x.Count)
                .ToListAsync(ct);

            var since = DateTime.UtcNow.Date.AddDays(-30);
            var recent = await q
                .Where(e => e.HireDate >= since)
                .OrderByDescending(e => e.HireDate)
                .Take(50)
                .Select(e => new RecentHireItem
                {
                    Id = e.Id,
                    FullName = e.FirstName + " " + e.LastName,
                    Email = e.Email,
                    DepartmentName = e.Department != null ? e.Department.Name : "Unassigned",
                    HireDate = e.HireDate
                })
                .ToListAsync(ct);

            return new DashboardViewModel
            {
                TotalEmployees = totalEmployees,
                EmployeesByDepartment = byDept,
                RecentHires = recent,
                Search = search
            };
        }
    }
}
