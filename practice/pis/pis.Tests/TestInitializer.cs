using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using pis.Application;
using pis.Application.Dtos;
using pis.Application.Queries;
using pis.Data;
using Serilog;

namespace pis.Tests;

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
        services.AddAutoMapper(typeof(ClientDto));
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(GetClients).Assembly);
        });
        services.AddSingleton(config);
        var connectionString = config.GetConnectionString("LiveConnection");
        services.AddDbContext<PisDbContext>(x => x
            .EnableSensitiveDataLogging(true)
            .UseSqlite(connectionString)
        );
        services.AddScoped<IPisDbContext>(provider => provider.GetRequiredService<PisDbContext>());
        ServiceProvider = services.BuildServiceProvider();
        
        DbInit(ServiceProvider);
    }

    private static void DbInit(ServiceProvider sp)
    {
        // Apply migrations on startup
        using (var scope =sp.CreateScope())
        {
            var svc = scope.ServiceProvider;
            var context = svc.GetRequiredService<PisDbContext>();
            context.Database.Migrate();
        }
    }
}