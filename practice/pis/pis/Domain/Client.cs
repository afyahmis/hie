using CSharpFunctionalExtensions;

namespace pis.Domain;

public class Client : Entity<Guid>
{
    public string RefId { get; set; }
    public string RefNo { get; set; }
    public string FullName { get; set; }
    public string Sex { get; set; }
    public DateTime BirthDate { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
    public ICollection<MedicationRequest> Requests { get; set; } = new List<MedicationRequest>();
    
    private Client()
    {
        Id = Guid.NewGuid();
    }

    public override string ToString()
    {
        return $"{FullName}|{Sex}";
    }
}