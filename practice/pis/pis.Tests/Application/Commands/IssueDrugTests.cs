using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
    private IMapper _mapper;
    private IMediator _mediator;
    private MedicationRequest _request;

    [OneTimeSetUp]
    public void Init()
    {
        _mediator = TestInitializer.ServiceProvider.GetRequiredService<IMediator>();
        _mapper = TestInitializer.ServiceProvider.GetRequiredService<IMapper>();
        InitData();
    }

    [Test]
    public async Task should_Dispense()
    {
        var preIssueStock = FindDrug(_request.PrescriptionDrugCode);
        Assert.That(_request.Status, Is.EqualTo(RequestStatus.Pending));
        var newDispenseDtos = new IssueDrug(new IssueMedicationDto()
        {
            Id = _request.Id, DrugId =preIssueStock.Id
        });
       

        var res = await _mediator.Send(newDispenseDtos);
        Assert.That(res.IsSuccess,Is.True);
        
        var ctx = TestInitializer.ServiceProvider.GetRequiredService<PisDbContext>();
        var savedRequest = ctx.Requests.Find(_request.Id);
        var updatedStock = ctx.Drugs.Find(preIssueStock.Id);
        Assert.That(savedRequest.Status, Is.EqualTo(RequestStatus.Dispensed));
        Assert.That(updatedStock.QuantityInStock, Is.LessThan(preIssueStock.QuantityInStock));
    }

    private void InitData()
    {
        var req = NewRequest();
        var res= _mediator.Send(new GenerateRequest(req)).Result;
        _request = FindClient(req.Client.RefId).Requests.First();
    }
    
    private NewMedicationRequestDto NewRequest()
    {
       var client = TestData.GetClientsWithRequests(1).First();
        
        return new NewMedicationRequestDto()
        {
            Client = _mapper.Map<RequestClientDto>(client),
            Medications = _mapper.Map<List<MedicationDto>>(client.Requests),
        };
    }
    
    private Client? FindClient(string refId)
    {
        var ctx = TestInitializer.ServiceProvider.GetRequiredService<PisDbContext>();
        
        return ctx.Clients
            .Include(x => x.Requests)
            .AsNoTracking()
            .FirstOrDefault(x => x.RefId.ToLower() == refId.ToLower());
    }
    private Drug? FindDrug(string code)
    {
        var ctx = TestInitializer.ServiceProvider.GetRequiredService<PisDbContext>();
        
        return ctx.Drugs
            .AsNoTracking()
            .FirstOrDefault(x => x.Code.ToLower() == code.ToLower());
    }
}