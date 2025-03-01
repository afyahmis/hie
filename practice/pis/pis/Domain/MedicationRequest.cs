using System.ComponentModel.DataAnnotations.Schema;
using CSharpFunctionalExtensions;

namespace pis.Domain;

public enum RequestStatus
{
    Pending,
    Dispensed
}

public class MedicationRequest:Entity<Guid>
{
    public string RefId { get; set; }
    public string PrescriptionDrug { get; set; }
    public DateTime PrescriptionDate { get; set; }
    public Guid? DispenseDrugId { get; set; }
    public DateTime? DispenseDate { get; set; }
    public Guid ClientId { get;  set; }
    public DateTime Created { get; private set;}
    public DateTime? Updated { get;  set;}
    [NotMapped] public RequestStatus Status => GetStatus();

    private MedicationRequest()
    {
        Id = Guid.NewGuid();
        Created = DateTime.Now;
    }

    public void Issue(Guid drugId)
    {
        DispenseDrugId = drugId;
        DispenseDate = DateTime.Now;
        Updated = DateTime.Now;
    }

    private RequestStatus GetStatus()
    {
        if (DispenseDate.HasValue & DispenseDrugId.HasValue)
            return RequestStatus.Dispensed;
        
        return RequestStatus.Pending;
    }
    
    public override string ToString()
    {
        return $"{PrescriptionDrug}[{Status}]";
    }
}