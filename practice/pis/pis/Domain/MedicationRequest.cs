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
    public string RefId { get;private set; }
    public string PrescriptionDrugCode { get;private set; }
    public string PrescriptionDrug { get;private set; }
    public DateTime PrescriptionDate { get;private set; }
    public Guid? DispenseDrugId { get;private set; }
    public DateTime? DispenseDate { get;private set; }
    public Guid ClientId { get;private  set; }
    public DateTime Created { get; private set;}
    public DateTime? Updated { get;private  set;}
    [NotMapped] public RequestStatus Status => GetStatus();

    private MedicationRequest()
    {
        Id = Guid.NewGuid();
        Created = DateTime.Now;
    }

    public MedicationRequest(string refId, string prescriptionDrugCode, string prescriptionDrug,
        DateTime prescriptionDate, Guid clientId)
        : this()
    {
        RefId = refId;
        PrescriptionDrugCode = prescriptionDrugCode;
        PrescriptionDrug = prescriptionDrug;
        PrescriptionDate = prescriptionDate;
        ClientId = clientId;
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