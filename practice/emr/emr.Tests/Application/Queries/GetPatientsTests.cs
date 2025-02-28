using emr.Application.Queries;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace emr.Tests.Application.Queries;

[TestFixture]
public class GetPatientsTests
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
        var res = await _mediator.Send(new GetPatients());
        Assert.That(res.IsSuccess,Is.True);
        Assert.That(res.Value.Any(),Is.True);
    }
    
    [TestCase("A02",true)]
    [TestCase("J01",true)]
    [TestCase("x-1",false)]
    public async Task should_GetAll_By_Clinic(string no,bool match)
    {
        var res = await _mediator.Send(new GetPatients(no));
        Assert.That(res.IsSuccess,Is.True);
        Assert.That(res.Value.Any(),Is.EqualTo(match));
    }
}
