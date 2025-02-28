using Bogus;
using Bogus.Extensions.Belgium;
using emr.Domain;
using emr.Tests.TestArtifacts;
using Serilog;

namespace emr.Tests.Domain;
[TestFixture]
public class PrescriptionTests
{
    private Patient _patient;

    [SetUp]
    public void Setup()
    {
        _patient = TestData.GetPatient();
    }
    
    [Test]
    public void should_have_Id()
    {
        _patient.Prescribe(TestData.GetDrug());
        var pres = _patient.Prescriptions.First();
        Assert.That(pres.Id,Is.Not.EqualTo(default(Guid)));
        Log.Information($"{pres}");
    }
}