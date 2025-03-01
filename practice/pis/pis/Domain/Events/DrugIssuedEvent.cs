using MediatR;

namespace pis.Domain.Events;

public class DrugIssuedEvent:INotification
{
    public Guid MedicationRequestId { get; set; }
    public DateTime Occurred { get; }

    public DrugIssuedEvent(Guid medicationRequestId)
    {
        MedicationRequestId = medicationRequestId;
        Occurred = DateTime.Now;
    }
}