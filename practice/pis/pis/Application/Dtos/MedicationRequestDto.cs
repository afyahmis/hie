using pis.Domain;

namespace pis.Application.Dtos;

public class MedicationRequestDto
{
    public Guid Id { get; set; }
    public string RefId { get; set; }
    public string PrescriptionDrugCode { get; set; }
    public string PrescriptionDrug { get; set; }
    public DateTime PrescriptionDate { get; set; }
    public Guid? DispenseDrugId { get; set; }
    public DateTime? DispenseDate { get; set; }
    public Guid ClientId { get;  set; }
    public DateTime Created { get;  set;}=DateTime.Now;
    public DateTime? Updated { get;  set;}
}

public class NewMedicationRequestDto
{
    public RequestClientDto Client { get; set; }
    public List<MedicationDto> Medications { get; set; } = new();

    public List<MedicationRequest> Create(Guid clientId)
    {
        return Medications.Select(x => new MedicationRequest(
            x.RefId, x.PrescriptionDrugCode, x.PrescriptionDrug, x.PrescriptionDate, clientId
        )).ToList();
    }
}

public class IssueMedicationDto
{
    public Guid Id { get; set; }
    public Guid DrugId { get; set; }
}

public class MedicationDto
{
    public string RefId { get; set; } 
    public string PrescriptionDrugCode { get; set; }
    public string PrescriptionDrug { get; set; }
    public DateTime PrescriptionDate { get; set; }
}