using MediatR;
using Microsoft.Extensions.DependencyInjection;
using pis.Application.Queries;
using pis.Data;
using pis.Domain;
using pis.Tests.TestArtifacts;
using Serilog;

namespace pis.Tests.Application.Queries;

[TestFixture]
public class GetDrugsTests
{
    private IMediator _mediator;
   
    [SetUp]
    public void SetUp()
    {
        _mediator = TestInitializer.ServiceProvider.GetRequiredService<IMediator>();
    }
    
    [Test]
    public async Task should_GetAll()
    {
        var res = await _mediator.Send(new GetDrugs());
        Assert.That(res.IsSuccess,Is.True);
        Assert.That(res.Value.Any(),Is.True);
        res.Value.OrderBy(d=>d.Name).ToList().ForEach(x=>Log.Information($"{x}"));
    }

    [Test]
    public async Task should_GetBy_Code()
    {
        var res = await _mediator.Send(new GetDrugs(TestData.NewDrug().Code));
        Assert.That(res.IsSuccess, Is.True);
        Assert.That(res.Value.Count, Is.EqualTo(1));
        res.Value.OrderBy(d=>d.Name).ToList().ForEach(x=>Log.Information($"{x}"));
    }
}
