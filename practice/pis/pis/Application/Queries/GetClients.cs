using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using pis.Application.Dtos;
using Serilog;

namespace pis.Application.Queries;

public class GetClients:IRequest<Result<List<ClientDto>>>
{
    public Guid? Id { get; }
    public string? RefId { get; }

    public GetClients()
    {
    }

    public GetClients(Guid id)
    {
        Id = id;
    }
    public GetClients(string refId)
    {
        RefId = refId;
    }
}

public class GetClientsHandler : IRequestHandler<GetClients, Result<List<ClientDto>>>
{
    private readonly IMapper _mapper;
    private readonly IPisDbContext _context;

    public GetClientsHandler(IMapper mapper, IPisDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<Result<List<ClientDto>>> Handle(GetClients request, CancellationToken cancellationToken)
    {
        try
        {
            var query = _context.Clients
                .Include(p => p.Requests)
                .AsNoTracking();

            if (request.Id.HasValue)
                query = query.Where(x => x.Id == request.Id);
            
            if (!string.IsNullOrWhiteSpace(request.RefId))
                query = query.Where(x => x.RefId.ToLower() == request.RefId.ToLower());
            
            var clients = await query.ToListAsync(cancellationToken);

            var dto = _mapper.Map<List<ClientDto>>(clients);
            return Result.Success(dto);

        }
        catch (Exception e)
        {
            Log.Error(e, "Error reading");
            return Result.Failure<List<ClientDto>>(e.Message);
        }
    }
}