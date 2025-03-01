using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using pis.Application.Commands;
using pis.Application.Dtos;
using pis.Data;
using pis.Tests.TestArtifacts;

namespace pis.Tests.Application.Commands;
[TestFixture]
public class GenerateRequestTests
{
    private IMediator _mediator;
    private IMapper _mapper;

    [SetUp]
    public void SetUp()
    {
        _mediator = TestInitializer.ServiceProvider.GetRequiredService<IMediator>();
        _mapper = TestInitializer.ServiceProvider.GetRequiredService<IMapper>();
    }
    
    [Test]
    public async Task should_Generate()
    {
        var newMedicationRequestDto = GetRequest();
        
        var res = await _mediator.Send(new GenerateRequest(newMedicationRequestDto));
        Assert.That(res.IsSuccess,Is.True);
    }

    private NewMedicationRequestDto GetRequest()
    {
        var ctx = TestInitializer.ServiceProvider.GetRequiredService<PisDbContext>();
        var medications = ctx.Drugs.AsNoTracking().Take(2).Select(x => new
            MedicationDto()
            {
                RefId = Guid.NewGuid().ToString(),
                PrescriptionDrug = x.Name,
                PrescriptionDate = DateTime.Now.AddHours(-1)
            }).ToList();
        return new NewMedicationRequestDto()
        {
            Client = _mapper.Map<ClientRequestDto>(TestData.GetClients(1).First()),
            Medications = medications
        };
    }
}
