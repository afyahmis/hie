using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using pis.Application.Dtos;
using Serilog;

namespace pis.Application.Queries;

public class GetDrugs:IRequest<Result<List<DrugDto>>>
{
    public string? Code { get; }

    public GetDrugs(string? code = null)
    {
        Code = code;
    }
}

public class GetDrugsHandler : IRequestHandler<GetDrugs, Result<List<DrugDto>>>
{
    private readonly IMapper _mapper;
    private readonly IPisDbContext _context;

    public GetDrugsHandler(IMapper mapper, IPisDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<Result<List<DrugDto>>> Handle(GetDrugs request, CancellationToken cancellationToken)
    {
        try
        {
            var query = _context.Drugs.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(request.Code))
                query = query.Where(x => x.Code.ToLower() == request.Code.ToLower());
            
            var drugs =await query.ToListAsync(cancellationToken);
            
            var dto = _mapper.Map<List<DrugDto>>(drugs);
            
            return Result.Success(dto);
        }
        catch (Exception e)
        {
            Log.Error(e, "Error reading");
            return Result.Failure<List<DrugDto>>(e.Message);
        }
    }
}