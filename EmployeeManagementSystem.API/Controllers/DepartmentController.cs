using EmployeeManagementSystem.Application.DTOs;
using EmployeeManagementSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.API.Controllers;

public class DepartmentController(IDepartmentService departmentService) : Controller
{
    private readonly IDepartmentService _departmentService = departmentService;

    public async Task<IActionResult> Index()
    {
        var departments = await _departmentService.GetAllAsync();
        return View(departments);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(DepartmentDTO dto)
    {
        if (!ModelState.IsValid)
            return View(dto);

        await _departmentService.AddAsync(dto);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var dept = await _departmentService.GetByIdAsync(id);
        if (dept == null) return NotFound();

        return View(dept);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(DepartmentDTO dto)
    {
        if (!ModelState.IsValid)
            return View(dto);

        await _departmentService.UpdateAsync(dto);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var dept = await _departmentService.GetByIdAsync(id);
        if (dept == null) return NotFound();

        return View(dept);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var hasEmployees = await _departmentService.HasEmployeesAsync(id);
        if (hasEmployees)
        {
            ModelState.AddModelError("", "Cannot delete department with employees.");
            var dept = await _departmentService.GetByIdAsync(id);
            return View("Delete", dept);
        }

        await _departmentService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}