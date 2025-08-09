using EmployeeManagementSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.API.Controllers
{
    public class DashboardController(IDashboardService dashboardService) : Controller
    {
        private readonly IDashboardService _dashboardService = dashboardService;

        [HttpGet]
        public async Task<IActionResult> Index(string? search)
        {
            var vm = await _dashboardService.GetAsync(search);
            return View(vm);
        }
    }
}
