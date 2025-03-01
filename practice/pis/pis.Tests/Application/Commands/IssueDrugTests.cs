using MediatR;
using Microsoft.Extensions.DependencyInjection;
using pis.Application.Commands;
using pis.Application.Dtos;
using pis.Data;
using pis.Domain;
using pis.Tests.TestArtifacts;

namespace pis.Tests.Application.Commands;

[TestFixture]
public class IssueDrugTests
{
    private IMediator _mediator;
    private List<Client> _clients;

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
    public async Task should_Dispense()
    {
        var req = _clients.First().Requests.First();

        var newDispenseDtos = new IssueDrug(new IssueMedicationDto()
        {
            Id = req.Id, DrugName = req.PrescriptionDrug
        });
       

        var res = await _mediator.Send(newDispenseDtos);
        Assert.That(res.IsSuccess,Is.True);
    }

    private void InitData()
    {
        _clients = TestData.GetClientsWithRequests();
        var ctx= TestInitializer.ServiceProvider.GetRequiredService<PisDbContext>();
        ctx.AddRange(_clients);
        ctx.SaveChanges();
    }
}