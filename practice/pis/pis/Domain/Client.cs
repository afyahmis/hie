using CSharpFunctionalExtensions;

namespace pis.Domain;

public class Client : Entity<Guid>
{
    public string RefId { get;private set; }
    public string RefNo { get;private set; }
    public string FullName { get;private set; }
    public string Sex { get;private set; }
    public DateTime BirthDate { get;private set; }
    public DateTime Created { get;private set; }
    public ICollection<MedicationRequest> Requests { get; set; } = new List<MedicationRequest>();
    
    private Client()
    {
        Id = Guid.NewGuid();
        Created=DateTime.Now;
    }

    public Client(string refId, string refNo, string fullName, string sex, DateTime birthDate)
        :this()
    {
        RefId = refId;
        RefNo = refNo;
        FullName = fullName;
        Sex = sex;
        BirthDate = birthDate;
    }

    public override string ToString()
    {
        return $"{FullName}|{Sex}";
    }
}