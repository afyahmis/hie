using System.ComponentModel.DataAnnotations;
using CSharpFunctionalExtensions;

namespace emr.Domain;

public class Prescription:Entity<Guid>
{
    private readonly List<DrugDispense> _dispenses = new ();
    public string Drug { get;private set; }
    public DateTime Date { get;private set; }
    public Guid PatientId { get;private set; }
    
    public IReadOnlyCollection<DrugDispense> Dispenses => _dispenses;

    private Prescription()
    {
        Id=Guid.NewGuid();
        Date=DateTime.Now;
    }

    public Prescription(string drug, Guid patientId)
        :this()
    {
        Drug = drug;
        PatientId = patientId;
    }

    public DrugDispense Dispense(string drug, DateTime date, string dispenseRef)
    {
        var disp = new DrugDispense(drug, date, dispenseRef, Id);
        _dispenses.Add(disp);
        return disp;
    }

    public override string ToString()
    {
        return $"{Drug}";
    }
}