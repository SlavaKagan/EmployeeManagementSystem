using EmployeeManagementSystem.Application.Interfaces;
using EmployeeManagementSystem.Domain.Entities;
using EmployeeManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Infrastructure.Repositories
{
    public class EmployeeRepository(AppDbContext context) : IEmployeeRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<List<Employee>> GetAllAsync()
        {
            return await _context.Employees
                .Where(e => !e.IsDeleted)
                .Include(e => e.Department)
                .ToListAsync();
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _context.Employees
                .Where(e => !e.IsDeleted)
                .Include(e => e.Department)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task AddAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Employee employee)
        {
            employee.UpdatedAt = DateTime.UtcNow;
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                employee.IsDeleted = true;
                employee.UpdatedAt = DateTime.UtcNow;
                _context.Employees.Update(employee);
                await _context.SaveChangesAsync();
            }
        }

        public IQueryable<Employee> Query()
            => _context.Employees
            .Where(e=>!e.IsDeleted)
            .AsQueryable();
    }
}
