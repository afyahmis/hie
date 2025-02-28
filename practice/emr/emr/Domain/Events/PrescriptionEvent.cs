using MediatR;

namespace emr.Domain.Events;

public class PrescriptionEvent:INotification
{
    public Guid[] PrescriptionIds { get;  }
    public DateTime Occurred { get; }

    public PrescriptionEvent(Guid[] prescriptionIds)
    {
        PrescriptionIds = prescriptionIds;
        Occurred = DateTime.Now;
    }
}