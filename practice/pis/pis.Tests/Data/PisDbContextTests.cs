using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using pis.Data;
using Serilog;

namespace pis.Tests.Data;
[TestFixture]
public class PisDbContextTests
{
    private PisDbContext _context;

    [SetUp]
    public void SetUp()
    {
        _context = TestInitializer.ServiceProvider.GetRequiredService<PisDbContext>();
    }

    [Test]
    public void should_Seed()
    {
        Assert.That(_context.Drugs.AsNoTracking().Any);

        foreach (var drug in _context.Drugs.AsNoTracking().OrderBy(x=>x.Name))
            Log.Information($"{drug}");

    }
}