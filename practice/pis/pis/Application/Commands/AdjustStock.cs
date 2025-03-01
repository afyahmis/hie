using CSharpFunctionalExtensions;
using MediatR;
using pis.Domain.Events;
using Serilog;

namespace pis.Application.Commands;

public class AdjustStock:IRequest<Result>
{
    public Guid DrugId { get; }
    public int Quantity { get; }

    public AdjustStock(Guid drugId, int quantity)
    {
        DrugId = drugId;
        Quantity = quantity;
    }
}

public class AdjustStockHandler : IRequestHandler<AdjustStock, Result>
{
    private readonly IMediator _mediator;
    private readonly IPisDbContext _context;

    public AdjustStockHandler(IMediator mediator, IPisDbContext context)
    {
        _mediator = mediator;
        _context = context;
    }

    public async Task<Result> Handle(AdjustStock request, CancellationToken cancellationToken)
    {
        try
        {
            // stock
            var drug =await _context.Drugs.FindAsync(request.DrugId, cancellationToken);
            
            if (null == drug)
                throw new Exception($"Drug Id={request.DrugId} Does NOT exist!");

            drug.Adjust(request.Quantity);
            
            await _context.Commit(cancellationToken);
            
            await _mediator.Publish(new StockAdjustedEvent(drug.Id), cancellationToken);
            
            return Result.Success();

        }
        catch (Exception e)
        {
            Log.Error(e, "Error saving");
            return Result.Failure(e.Message);
        }
    }
}