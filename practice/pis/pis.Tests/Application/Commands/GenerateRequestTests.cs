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
        var request = GetRequest();
        
        var res = await _mediator.Send(new GenerateRequest(request));
        Assert.That(res.IsSuccess,Is.True);
        
        var ctx = TestInitializer.ServiceProvider.GetRequiredService<PisDbContext>();
        var saved = FindClient(ctx, request);
        Assert.That(saved,Is.Not.Null);
        Assert.That(saved.Requests.Any(),Is.True);
        Assert.That(saved.Requests.Count(),Is.EqualTo(request.Medications.Count));
    }

    private NewMedicationRequestDto GetRequest()
    {
        var client = TestData.GetClientsWithRequests(1).First();
        
        return new NewMedicationRequestDto()
        {
            Client = _mapper.Map<RequestClientDto>(client),
            Medications = _mapper.Map<List<MedicationDto>>(client.Requests),
        };
    }
    private static Client? FindClient(PisDbContext ctx, NewMedicationRequestDto newMedicationRequestDto)
    {
        return ctx.Clients
            .Include(x => x.Requests)
            .AsNoTracking()
            .FirstOrDefault(x => x.RefId == newMedicationRequestDto.Client.RefId);
    }
}
