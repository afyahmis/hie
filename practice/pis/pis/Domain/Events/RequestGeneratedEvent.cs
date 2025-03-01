using MediatR;

namespace pis.Domain.Events;

public class RequestGeneratedEvent:INotification
{
    public Guid ClientId { get; set; }
    public Guid[] PrescriptionIds { get;  }
    public DateTime Occurred { get; }
    
    public RequestGeneratedEvent(Guid clientId, Guid[] prescriptionIds)
    {
        ClientId = clientId;
        PrescriptionIds = prescriptionIds;
        Occurred = DateTime.Now;
    }
}