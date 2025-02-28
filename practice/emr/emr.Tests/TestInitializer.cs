using emr.Application;
using emr.Application.Dtos;
using emr.Application.Queries;
using emr.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace emr.Tests;

[SetUpFixture]
public class TestInitializer
{
    public static ServiceProvider ServiceProvider;
    public static IConfiguration Configuration;

    [OneTimeSetUp]
    public void Init()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Console()
            .CreateLogger();
        SetupDependencyInjection();
    }

    private void SetupDependencyInjection()
    {
        var config = Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.Test.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        var services = new ServiceCollection();
        services.AddAutoMapper(typeof(PatientDto));
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(GetPatients).Assembly);
        });
        services.AddSingleton(config);
        var connectionString = config.GetConnectionString("LiveConnection");
        services.AddDbContext<EmrDbContext>(x => x
            .EnableSensitiveDataLogging(true)
            .UseSqlite(connectionString)
        );
        services.AddScoped<IEmrDbContext>(provider => provider.GetRequiredService<EmrDbContext>());
        ServiceProvider = services.BuildServiceProvider();
        
        DbInit(ServiceProvider);
    }

    private static void DbInit(ServiceProvider sp)
    {
        // Apply migrations on startup
        using (var scope =sp.CreateScope())
        {
            var svc = scope.ServiceProvider;
            var context = svc.GetRequiredService<EmrDbContext>();
            context.Database.Migrate();
        }
    }
}