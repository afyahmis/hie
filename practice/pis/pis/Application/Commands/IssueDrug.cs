using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using pis.Application.Dtos;
using pis.Domain.Events;
using Serilog;

namespace pis.Application.Commands;

public class IssueDrug:IRequest<Result>
{
    public IssueMedicationDto IssueDto { get; }

    public IssueDrug(IssueMedicationDto issueDto)
    {
        IssueDto = issueDto;
    }
}

public class IssueDrugHandler : IRequestHandler<IssueDrug, Result>
{
    private readonly IMediator _mediator;
    private readonly IPisDbContext _context;

    public IssueDrugHandler(IMediator mediator, IPisDbContext context)
    {
        _mediator = mediator;
        _context = context;
    }

    public async Task<Result> Handle(IssueDrug request, CancellationToken cancellationToken)
    {
        try
        {
            // request
            var medicationRequest =await _context.Requests.FindAsync(request.IssueDto.Id, cancellationToken);

            if (null == medicationRequest)
                throw new Exception($"Request Id={request.IssueDto.Id} Does NOT exist!");
            
            // stock
            var drug =await _context.Drugs.FindAsync(request.IssueDto.DrugId,cancellationToken);
            
            if (null == drug)
                throw new Exception($"Drug Name={request.IssueDto.DrugId} Does NOT exist!");

            medicationRequest.Issue(drug.Id);
            drug.Dispense();
            
            await _context.Commit(cancellationToken);
            
            await _mediator.Publish(new DrugIssuedEvent(medicationRequest.Id), cancellationToken);
            
            return Result.Success();

        }
        catch (Exception e)
        {
            Log.Error(e, "Error saving");
            return Result.Failure(e.Message);
        }
    }
}