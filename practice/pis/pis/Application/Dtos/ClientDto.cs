namespace pis.Application.Dtos;

public class ClientDto
{
    public Guid Id { get; set; }
    public string RefId { get; set; }
    public string RefNo { get; set; }
    public string FullName { get;  set;}
    public string Sex { get;  set;}
    public DateTime BirthDate { get;  set;}
    public DateTime Created { get;  set;}
    public List<MedicationRequestDto> Requests { get; set; } = new ();
}
public class RequestClientDto
{
    public string RefId { get; set; }
    public string RefNo { get; set; }
    public string FullName { get;  set;}
    public string Sex { get;  set;}
    public DateTime BirthDate { get;  set;}
}