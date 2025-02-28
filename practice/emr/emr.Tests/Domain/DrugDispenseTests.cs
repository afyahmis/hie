using emr.Domain;
using emr.Tests.TestArtifacts;
using Serilog;

namespace emr.Tests.Domain;

public class DrugDispenseTests
{
    private Patient _patient;

    [SetUp]
    public void Setup()
    {
        _patient = TestData.GetPatientWithPrescription();
    }
    
    [Test]
    public void should_have_Id()
    {
        var prescription = _patient.Prescriptions.First();
        prescription.Dispense(prescription.Drug, DateTime.Now, "D1111-123");
        var drugDispense = prescription.Dispenses.First();
        Assert.That(drugDispense.Id,Is.Not.EqualTo(default(Guid)));
        Assert.That(drugDispense.PrescriptionId,Is.EqualTo(prescription.Id));
        Log.Information($"{drugDispense}");
    }

    [Test]
    public void should_support_MultiDispense()
    {
        var prescription = _patient.Prescriptions.First();
        prescription.Dispense($"{prescription.Drug} X1", DateTime.Now, "D1111-1");
        prescription.Dispense($"{prescription.Drug} X2", DateTime.Now, "D1111-2");
        Assert.That(prescription.Dispenses.Count, Is.EqualTo(2));
        foreach (var drugDispense in prescription.Dispenses)
            Log.Information($"{drugDispense}");
    }
}