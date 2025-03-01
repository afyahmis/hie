using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using pis.Application.Dtos;
using pis.Domain;
using pis.Domain.Events;
using Serilog;

namespace pis.Application.Commands;

public class GenerateRequest:IRequest<Result>
{
    public NewMedicationRequestDto Prescription { get; }

    public GenerateRequest(NewMedicationRequestDto prescription)
    {
        Prescription = prescription;
    }
}

public class GenerateRequestHandler : IRequestHandler<GenerateRequest, Result>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IPisDbContext _context;

    public GenerateRequestHandler(IMediator mediator, IPisDbContext context, IMapper mapper)
    {
        _mediator = mediator;
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result> Handle(GenerateRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var clientId = await SaveClient(request, cancellationToken);

            var prescriptions = request.Prescription.Create(clientId);
            await _context.Requests.AddRangeAsync(prescriptions, cancellationToken);
            await _context.Commit(cancellationToken);

            await _mediator.Publish(new RequestGeneratedEvent(
                    clientId,
                    prescriptions
                        .Select(x => x.Id)
                        .ToArray()),
                cancellationToken);

            return Result.Success();

        }
        catch (Exception e)
        {
            Log.Error(e, "Error saving");
            return Result.Failure(e.Message);
        }
    }

    private async Task<Guid> SaveClient(GenerateRequest request, CancellationToken cancellationToken)
    {
        var refId = request.Prescription.Client.RefId.ToLower();
        
        var client = await _context.Clients.AsNoTracking()
            .FirstOrDefaultAsync(x => x.RefId.ToLower() == refId, cancellationToken);

        if (null == client)
        {
            var newClient = _mapper.Map<Client>(request.Prescription.Client);
            await _context.Clients.AddAsync(newClient, cancellationToken);
            await _context.Commit(cancellationToken);
            return newClient.Id;
        }

        return client.Id;
    }
}