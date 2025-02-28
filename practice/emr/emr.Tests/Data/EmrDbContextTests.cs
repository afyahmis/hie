using emr.Data;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace emr.Tests.Data;
[TestFixture]
public class EmrDbContextTests
{
    private EmrDbContext _context;

    [SetUp]
    public void SetUp()
    {
        _context = TestInitializer.ServiceProvider.GetRequiredService<EmrDbContext>();
    }

    [Test]
    public void should_Seed()
    {
        Assert.That(_context.Patients.Any);

        foreach (var patient in _context.Patients)
            Log.Information($"{patient}");

    }
}