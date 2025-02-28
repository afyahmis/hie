namespace emr.Application.Dtos;

public class DrugDispenseDto
{
    public Guid Id { get; set; }
    public string Drug { get; set; }
    public DateTime Date { get; set; }
    public string DispenseRef { get; set; }
    public Guid PrescriptionId { get; set; }
}

public class NewDispenseDto
{
    public string Drug { get; set; }
    public DateTime Date { get; set; }
    public string DispenseRef { get; set; }
    public Guid PrescriptionId { get; set; }
}