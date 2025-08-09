using AutoMapper;
using EmployeeManagementSystem.Application.DTOs;
using EmployeeManagementSystem.Application.Interfaces;
using EmployeeManagementSystem.Domain.Entities;

namespace EmployeeManagementSystem.Application.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepo;
        private readonly IMapper _mapper;

        public DepartmentService(IDepartmentRepository departmentRepo, IMapper mapper)
        {
            _departmentRepo = departmentRepo;
            _mapper = mapper;
        }

        public async Task<List<DepartmentDTO>> GetAllAsync()
        {
            var list = await _departmentRepo.GetAllAsync();
            return _mapper.Map<List<DepartmentDTO>>(list);
        }

        public async Task<DepartmentDTO?> GetByIdAsync(int id)
        {
            var dept = await _departmentRepo.GetByIdAsync(id);
            return dept == null ? null : _mapper.Map<DepartmentDTO>(dept);
        }

        public async Task AddAsync(DepartmentDTO dto)
        {
            var entity = _mapper.Map<Department>(dto);
            await _departmentRepo.AddAsync(entity);
        }

        public async Task UpdateAsync(DepartmentDTO dto)
        {
            var entity = _mapper.Map<Department>(dto);
            await _departmentRepo.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _departmentRepo.DeleteAsync(id);
        }

        public async Task<bool> HasEmployeesAsync(int departmentId)
        {
            return await _departmentRepo.HasEmployeesAsync(departmentId);
        }
    }
}
