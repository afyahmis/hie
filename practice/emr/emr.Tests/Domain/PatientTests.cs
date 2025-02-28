using Bogus;
using Bogus.Extensions.Belgium;
using emr.Domain;
using emr.Tests.TestArtifacts;
using Serilog;

namespace emr.Tests.Domain;

[TestFixture]
public class PatientTests
{
    [Test]
    public void should_have_Id()
    {
        var faker = new Faker().Person;
        var patient = new Patient(faker.NationalNumber(false), faker.FullName, faker.Gender.ToString(),faker.DateOfBirth);
        Assert.That(patient.Id,Is.Not.EqualTo(default(Guid)));
        Log.Information($"{patient}");
    }
    [Test]
    public void should_have_Age()
    {
        var faker = new Faker().Person;
        var patient = new Patient(faker.NationalNumber(false), faker.FullName, faker.Gender.ToString(), DateTime.Now.AddYears(-22));
        Assert.That(patient.Age,Is.EqualTo("22 y"));
        Log.Information($"{patient}");
    }

    [Test]
    public void should_Prescribe()
    {
        var faker = new Faker().Person;

        var patient = TestData.GetPatients(1).First();
        patient.Prescribe(TestData.GetDrug());
        patient.Prescribe(TestData.GetDrug());
        Assert.That(patient.Prescriptions.Count, Is.EqualTo(2));
        Log.Information($"{patient}");
        foreach (var prescription in patient.Prescriptions)
            Log.Information($">>> {prescription}");
    }
}