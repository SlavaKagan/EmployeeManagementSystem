using EmployeeManagementSystem.Application.DTOs;
using EmployeeManagementSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.API.Controllers
{
    public class EmployeeController(IEmployeeService employeeService, IDepartmentService departmentService) : Controller
    {
        private readonly IEmployeeService _employeeService = employeeService;
        private readonly IDepartmentService _departmentService = departmentService;

        public async Task<IActionResult> Create()
        {
            ViewBag.Departments = await _departmentService.GetAllAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeDTO dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Departments = await _departmentService.GetAllAsync();
                return View(dto);
            }

            await _employeeService.AddAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _employeeService.GetByIdAsync(id);
            if (employee == null) return NotFound();

            ViewBag.Departments = await _departmentService.GetAllAsync();
            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeDTO dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Departments = await _departmentService.GetAllAsync();
                return View(dto);
            }

            await _employeeService.UpdateAsync(dto);
            TempData["Success"] = $"Employee '{dto.FirstName} {dto.LastName}' updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _employeeService.GetByIdAsync(id);
            if (employee == null) return NotFound();

            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _employeeService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Index(
            int page = 1, int pageSize = 10, string? sortBy = null, bool desc = false, string? search = null)
        {
            var result = await _employeeService.GetPagedAsync(page, pageSize, sortBy, desc, search);

            ViewBag.SortBy = sortBy;
            ViewBag.Desc = desc;
            ViewBag.Search = search;
            ViewBag.PageSize = pageSize;
            ViewBag.PageSizeOptions = new[] { 10, 25, 50, 100 };

            return View(result);
        }
    }
}