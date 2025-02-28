using CSharpFunctionalExtensions;
using emr.Application.Dtos;
using emr.Domain;
using emr.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace emr.Application.Commands;

public class DispenseDrugs:IRequest<Result>
{
    public List<NewDispenseDto> Dispenses { get; }

    public DispenseDrugs(List<NewDispenseDto> dispenses)
    {
        Dispenses = dispenses;
    }
}

public class DispenseDrugsHandler : IRequestHandler<DispenseDrugs, Result>
{
    private readonly IMediator _mediator;
    private readonly IEmrDbContext _context;

    public DispenseDrugsHandler(IMediator mediator, IEmrDbContext context)
    {
        _mediator = mediator;
        _context = context;
    }

    public async Task<Result> Handle(DispenseDrugs request, CancellationToken cancellationToken)
    {
        try
        {
            var ids = request.Dispenses.Select(x => x.PrescriptionId).ToList();
            
            var prescriptions =await _context.Prescriptions
                .AsNoTracking()
                .Where(x=>ids.Contains(x.Id))
                .ToListAsync(cancellationToken);

            var drugDispenses = new List<DrugDispense>();
            foreach (var prescription in prescriptions)
            {
                var dispensed = request.Dispenses.First(x => x.PrescriptionId == prescription.Id);
                var drugDispense =prescription.Dispense(dispensed.Drug, dispensed.Date, dispensed.DispenseRef);
                drugDispenses.Add(drugDispense);
            }

            await _context.DrugDispenses.AddRangeAsync(drugDispenses,cancellationToken);
            
            await _context.Commit(cancellationToken);
            
            await _mediator.Publish(new DispensationEvent(
                    drugDispenses
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