namespace emr.Application.Dtos;

public class PrescriptionDto
{
    public Guid Id { get; set; }
    public string Drug { get; set; }
    public DateTime Date { get; set; }
    public Guid PatientId { get; set; }
    public List<DrugDispenseDto>? Dispenses { get; set; } = new();
}

public class NewPrescriptionDto
{
    public Guid PatientId { get; set; }
    public List<DrugDto> Medications { get; set; } = new();
}

public class DrugDto
{
    public string Drug { get; set; }
}