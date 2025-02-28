namespace emr.Application.Dtos;

public class PatientDto
{
    public Guid Id { get; set; }
    public string ClinicNo { get;  set; }
    public string Name { get;  set;}
    public string Sex { get;  set;}
    public DateTime BirthDate { get; set; }
    public List<PrescriptionDto> Prescriptions { get; set; } = new();
}