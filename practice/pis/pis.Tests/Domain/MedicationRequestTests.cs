using pis.Domain;
using pis.Tests.TestArtifacts;
using Serilog;

namespace pis.Tests.Domain;
[TestFixture]
public class MedicationRequestTests
{
    private MedicationRequest _medicationRequest;

    [SetUp]
    public void Setup()
    {
        _medicationRequest = TestData.GetRequests(Guid.NewGuid()).First();
    }
    
    [Test]
    public void should_have_Id()
    {
        var medicationRequest =TestData.GetRequests(Guid.NewGuid()).Last();
        Assert.That(medicationRequest.Id,Is.Not.EqualTo(default(Guid)));
        Assert.That(medicationRequest.Updated,Is.Null);
        Log.Information($"{medicationRequest}");
    }
    [Test]
    public void should_Issue()
    {
        Assert.That(_medicationRequest.Status,Is.EqualTo(RequestStatus.Pending));
        _medicationRequest.Issue(Guid.NewGuid());
        Assert.That(_medicationRequest.DispenseDate,Is.GreaterThan(DateTime.Now.AddHours(-1)));
        Assert.That(_medicationRequest.Updated,Is.GreaterThan(DateTime.Now.AddHours(-1)));
        Assert.That(_medicationRequest.Status,Is.EqualTo(RequestStatus.Dispensed));
        Log.Information($"{_medicationRequest}");
    }
}