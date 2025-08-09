namespace EmployeeManagementSystem.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebUi(this IServiceCollection services)
        {
            services.AddControllersWithViews();
            return services;
        }
    }
}
