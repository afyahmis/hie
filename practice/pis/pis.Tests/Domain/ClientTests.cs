using pis.Tests.TestArtifacts;
using Serilog;

namespace pis.Tests.Domain;

[TestFixture]
public class ClientTests
{
    [Test]
    public void should_have_Id_Created()
    {
        var client = TestData.GetClients(1).First();
        Assert.That(client.Id,Is.Not.EqualTo(default(Guid)));
        Assert.That(client.Created,Is.GreaterThan(DateTime.Now.AddHours(-1)));
        Log.Information($"{client}");
    }
}