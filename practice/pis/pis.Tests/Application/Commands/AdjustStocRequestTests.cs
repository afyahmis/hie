using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using pis.Application.Commands;
using pis.Data;
using pis.Domain;

namespace pis.Tests.Application.Commands;
[TestFixture]
public class AdjustStockTests
{
    private IMediator _mediator;

    [SetUp]
    public void SetUp()
    {
        _mediator = TestInitializer.ServiceProvider.GetRequiredService<IMediator>();
    }
    
    [Test]
    public async Task should_Adjust()
    {
        var drug = GetDrugs().First();

        var adjustStock = new AdjustStock(drug.Id, -12);
        
        var res = await _mediator.Send(adjustStock);
        Assert.That(res.IsSuccess,Is.True);

        var ctx = TestInitializer.ServiceProvider.GetRequiredService<PisDbContext>();
        var saved = ctx.Drugs.Find(drug.Id);
        Assert.That(saved.QuantityInStock,Is.EqualTo(drug.QuantityInStock-12));

    }
    
    private List<Drug> GetDrugs()
    {
        var ctx = TestInitializer.ServiceProvider.GetRequiredService<PisDbContext>();
        return ctx.Drugs.AsNoTracking().ToList();
    }
}
