using pis.Domain;
using pis.Tests.TestArtifacts;
using Serilog;

namespace pis.Tests.Domain;
[TestFixture]
public class MedicationRequestTests
{
    [Test]
    public void should_have_Id()
    {
        var drug = TestData.NewDrug();
        var medicationRequest = new MedicationRequest(
            Guid.NewGuid().ToString(),
            drug.Code,
            drug.Name,
            DateTime.Now, Guid.NewGuid()
        );
        
        Assert.That(medicationRequest.Id,Is.Not.EqualTo(default(Guid)));
        Assert.That(medicationRequest.Updated,Is.Null);
        Log.Information($"{medicationRequest}");
    }
    [Test]
    public void should_Issue()
    {
        var drug = TestData.NewDrug();
        var request = new MedicationRequest(
            Guid.NewGuid().ToString(),
            drug.Code,
            drug.Name,
            DateTime.Now, Guid.NewGuid()
        );
        
        Assert.That(request.Status,Is.EqualTo(RequestStatus.Pending));
        request.Issue(Guid.NewGuid());
        Assert.That(request.DispenseDate,Is.GreaterThan(DateTime.Now.AddHours(-1)));
        Assert.That(request.Updated,Is.GreaterThan(DateTime.Now.AddHours(-1)));
        Assert.That(request.Status,Is.EqualTo(RequestStatus.Dispensed));
        Log.Information($"{request}");
    }
}