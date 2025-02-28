using CSharpFunctionalExtensions;
using emr.Application.Dtos;
using emr.Domain;
using emr.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace emr.Application.Commands;

public class PrescribeDrugs:IRequest<Result>
{
    public NewPrescriptionDto Prescription { get; }

    public PrescribeDrugs(NewPrescriptionDto prescription)
    {
        Prescription = prescription;
    }
}

public class PrescribeDrugsHandler : IRequestHandler<PrescribeDrugs, Result>
{
    private readonly IMediator _mediator;
    private readonly IEmrDbContext _context;

    public PrescribeDrugsHandler(IMediator mediator, IEmrDbContext context)
    {
        _mediator = mediator;
        _context = context;
    }

    public async Task<Result> Handle(PrescribeDrugs request, CancellationToken cancellationToken)
    {
        try
        {
            var patient = await _context.Patients
                .AsNoTracking()
                .FirstOrDefaultAsync(x=>x.Id==
                                        request.Prescription.PatientId, cancellationToken);

            if (null==patient)
                throw new Exception($"Patient id={request.Prescription.PatientId} Does NOT exist");
            
            var prescriptions = new List<Prescription>();
            
            foreach (var drugDto in request.Prescription.Medications)
            {
                var prescription = patient.Prescribe(drugDto.Drug);
                prescriptions.Add(prescription);
            }
            
            await _context.Prescriptions.AddRangeAsync(prescriptions,cancellationToken);
            await _context.Commit(cancellationToken);

            await _mediator.Publish(new PrescriptionEvent(
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