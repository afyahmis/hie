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
            Guid clientId;

            #region client
            var client = await _context.Clients
                .AsNoTracking()
                .FirstOrDefaultAsync(x=>x.RefId==
                                        request.Prescription.Client.RefId, cancellationToken);

            if (null == client)
            {
                var newClient = _mapper.Map<Client>(request.Prescription.Client);
                await _context.Clients.AddAsync(newClient,cancellationToken);
                clientId = newClient.Id;
            }
            else
            {
                clientId = client.Id;
            }
            #endregion

            var prescriptions = _mapper.Map<List<MedicationRequest>>(request.Prescription.Medications);
            prescriptions.ForEach(x => x.ClientId = clientId);
            
            await _context.Requests.AddRangeAsync(prescriptions,cancellationToken);
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
}