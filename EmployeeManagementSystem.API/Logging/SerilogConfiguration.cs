using Serilog;

namespace EmployeeManagementSystem.API.Logging;

public static class SerilogConfiguration
{
    public static void ConfigureLogging()
    {
        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.File(
                new Serilog.Formatting.Compact.CompactJsonFormatter(),
                path: $"Logs/log-{DateTime.UtcNow:yyyy-MM-dd}.json",
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 7,
                shared: true)
            .CreateLogger();
    }
}