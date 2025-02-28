using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CSharpFunctionalExtensions;

namespace emr.Domain;

public class Patient:Entity<Guid>
{
    private readonly List<Prescription> _prescriptions = new();
    public string ClinicNo { get; private set; }
    public string Name { get; private set;}
    public string Sex { get; private set;}
    public DateTime BirthDate { get; private set;}
   
    [NotMapped] public string Age => CalculateAge();
    public ICollection<Prescription> Prescriptions => _prescriptions;

    private Patient()
    {
        Id=Guid.NewGuid();
    }

    public Patient(string clinicNo, string name, string sex, DateTime birthDate)
        :this()
    {
        ClinicNo = clinicNo;
        Name = name;
        Sex = sex;
        BirthDate = birthDate;
    }

    public Prescription Prescribe(string drug)
    {
        var newP = new Prescription(drug, Id);
        _prescriptions.Add(newP);
        return newP;
    }
    
    private string CalculateAge()
    {
        return $"{DateTime.Now.Year - BirthDate.Year} y";
    }


    public override string ToString()
    {
        return $"{ClinicNo}|{Name}|{Sex}|{Age}";
    }
}