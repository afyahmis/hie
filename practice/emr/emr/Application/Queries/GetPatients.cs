using AutoMapper;
using CSharpFunctionalExtensions;
using emr.Application.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace emr.Application.Queries;

public class GetPatients:IRequest<Result<List<PatientDto>>>
{
    public string? ClinicNo { get; }

    public GetPatients(string? clinicNo = null)
    {
        ClinicNo = clinicNo;
    }
}

public class GetPatientsHandler : IRequestHandler<GetPatients, Result<List<PatientDto>>>
{
    private readonly IMapper _mapper;
    private readonly IEmrDbContext _context;

    public GetPatientsHandler(IMapper mapper, IEmrDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<Result<List<PatientDto>>> Handle(GetPatients request, CancellationToken cancellationToken)
    {
        try
        {
            var query = _context.Patients
                .Include(p => p.Prescriptions)
                .ThenInclude(x => x.Dispenses)
                .AsNoTracking();

            var patients = string.IsNullOrWhiteSpace(request.ClinicNo)
                ? await query.ToListAsync(cancellationToken)
                : await query.Where(x => x.ClinicNo.ToLower() == request.ClinicNo.Trim().ToLower()).ToListAsync(cancellationToken);

            var dto = _mapper.Map<List<PatientDto>>(patients);
            return Result.Success(dto);

        }
        catch (Exception e)
        {
            Log.Error(e, "Error reading");
            return Result.Failure<List<PatientDto>>(e.Message);
        }
    }
}