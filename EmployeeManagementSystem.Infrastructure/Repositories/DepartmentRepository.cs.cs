using EmployeeManagementSystem.Application.Interfaces;
using EmployeeManagementSystem.Domain.Entities;
using EmployeeManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Infrastructure.Repositories
{
    public class DepartmentRepository(AppDbContext context) : IDepartmentRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<List<Department>> GetAllAsync()
        {
            return await _context.Departments
                .Where(d => !d.IsDeleted)
                .ToListAsync();
        }

        public async Task<Department?> GetByIdAsync(int id)
        {
            return await _context.Departments
                .Where(d => !d.IsDeleted)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task AddAsync(Department department)
        {
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Department department)
        {
            department.UpdatedAt = DateTime.UtcNow;
            _context.Departments.Update(department);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department != null)
            {
                department.IsDeleted = true;
                department.UpdatedAt = DateTime.UtcNow;
                _context.Departments.Update(department);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> HasEmployeesAsync(int departmentId)
        {
            return await _context.Employees.AnyAsync(e => e.DepartmentId == departmentId);
        }

        public IQueryable<Department> Query()
            => _context.Departments
            .Where(p => !p.IsDeleted)
            .AsQueryable();
    }
}
