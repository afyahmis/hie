namespace pis.Application.Dtos;

public class MedicationRequestDto
{
    public Guid Id { get; set; }
    public string RefId { get; set; }
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
    public ClientRequestDto Client { get; set; }
    public List<MedicationDto> Medications { get; set; } = new();
}

public class IssueMedicationDto
{
    public Guid Id { get; set; }
    public string DrugName { get; set; }
}

public class MedicationDto
{
    public string RefId { get; set; }
    public string PrescriptionDrug { get; set; }
    public DateTime PrescriptionDate { get; set; }
}