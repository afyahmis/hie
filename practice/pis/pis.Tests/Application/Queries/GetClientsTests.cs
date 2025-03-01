using MediatR;
using Microsoft.Extensions.DependencyInjection;
using pis.Application.Queries;
using pis.Data;
using pis.Domain;
using pis.Tests.TestArtifacts;

namespace pis.Tests.Application.Queries;

[TestFixture]
public class GetClientsTests
{
    private List<Client> _clients = new();
    private IMediator _mediator;
    [OneTimeSetUp]
    public void Init()
    {
        InitData();
    }
    [SetUp]
    public void SetUp()
    {
        _mediator = TestInitializer.ServiceProvider.GetRequiredService<IMediator>();
    }
    
    [Test]
    public async Task should_GetAll()
    {
        var res = await _mediator.Send(new GetClients());
        Assert.That(res.IsSuccess,Is.True);
        Assert.That(res.Value.Any(),Is.True);
    }
    [Test]
    public async Task should_GetBy_Id()
    {
        var res = await _mediator.Send(new GetClients(_clients[2].Id));
        Assert.That(res.IsSuccess,Is.True);
        Assert.That(res.Value.Count,Is.EqualTo(1));
    }
    [Test]
    public async Task should_GetBy_Ref()
    {
        var res = await _mediator.Send(new GetClients(_clients[3].RefId));
        Assert.That(res.IsSuccess,Is.True);
        Assert.That(res.Value.Count,Is.EqualTo(1));
    }
    private void InitData()
    {
        _clients = TestData.GetClients(5);
        
        var ctx= TestInitializer.ServiceProvider.GetRequiredService<PisDbContext>();
        ctx.AddRange(_clients);
        ctx.SaveChanges();
    }
}
