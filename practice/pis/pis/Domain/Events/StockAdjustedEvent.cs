using MediatR;

namespace pis.Domain.Events;

public class StockAdjustedEvent:INotification
{
    public Guid DrugId { get; set; }
    public DateTime Occurred { get; }

    public StockAdjustedEvent(Guid drugId)
    {
        DrugId = drugId;
        Occurred = DateTime.Now;
    }
}