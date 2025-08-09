using EmployeeManagementSystem.API;
using EmployeeManagementSystem.API.Logging;
using EmployeeManagementSystem.API.Middleware;
using EmployeeManagementSystem.Application;
using EmployeeManagementSystem.Infrastructure;
using EmployeeManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;

SerilogConfiguration.ConfigureLogging();
var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();

builder.Services
    .AddWebUi()
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate(); // creates DB / applies migrations automatically
}
Directory.CreateDirectory(Path.Combine(app.Environment.ContentRootPath, "Logs"));

if (!app.Environment.IsDevelopment())
    app.UseExceptionHandler("/Home/Error");

app.UseStaticFiles();
app.UseSerilogRequestLogging();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Employee}/{action=Index}/{id?}");

app.Run();
