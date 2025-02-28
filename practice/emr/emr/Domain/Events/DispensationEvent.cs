using MediatR;

namespace emr.Domain.Events;

public class DispensationEvent:INotification
{
    public Guid[] DispensationIds { get;  }
    public DateTime Occurred { get; }

    public DispensationEvent(Guid[] dispensationIds)
    {
        DispensationIds = dispensationIds;
        Occurred = DateTime.Now;
    }
}