using System.ComponentModel.DataAnnotations;
using CSharpFunctionalExtensions;

namespace emr.Domain;

public class DrugDispense:Entity<Guid>
{
    public string Drug { get;private set; }
    public DateTime Date { get;private set; }
    public string DispenseRef { get;private set; }
    public Guid PrescriptionId { get;private set; }
    
    private DrugDispense()
    {
        Id = Guid.NewGuid();
    }

    public DrugDispense(string drug, DateTime date, string dispenseRef, Guid prescriptionId)
        :this()
    {
        Drug = drug;
        Date = date;
        DispenseRef = dispenseRef;
        PrescriptionId = prescriptionId;
    }

    public override string ToString()
    {
        return $"{Drug}|{DispenseRef}";
    }
}