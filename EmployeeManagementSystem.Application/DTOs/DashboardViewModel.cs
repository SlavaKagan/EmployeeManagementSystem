namespace EmployeeManagementSystem.Application.DTOs
{
    public class DashboardViewModel
    {
        public int TotalEmployees { get; set; }

        public List<DepartmentCountItem> EmployeesByDepartment { get; set; } = new();

        public List<RecentHireItem> RecentHires { get; set; } = new();

        public string? Search { get; set; }
    }

    public class DepartmentCountItem
    {
        public string DepartmentName { get; set; } = "";
        public int Count { get; set; }
    }

    public class RecentHireItem
    {
        public int Id { get; set; }
        public string FullName { get; set; } = "";
        public string Email { get; set; } = "";
        public string DepartmentName { get; set; } = "";
        public DateTime HireDate { get; set; }
    }
}
