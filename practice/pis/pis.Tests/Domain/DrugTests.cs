using pis.Domain;
using pis.Tests.TestArtifacts;
using Serilog;

namespace pis.Tests.Domain;

public class DrugTests
{
    private List<Drug> _drugs;

    [SetUp]
    public void Setup()
    {
        _drugs = TestData.GetDrugs();
    }
    
    [Test]
    public void should_have_Id_Updated_Date()
    {
        var drugDispense = new Drug(TestData.GetDrugName(), TestData.GetDrugCode(),100);
        Assert.That(drugDispense.Id,Is.Not.EqualTo(default(Guid)));
        Assert.That(drugDispense.Updated,Is.GreaterThan(DateTime.Now.AddHours(-1)));
        Log.Information($"{drugDispense}");
    }

    [Test]
    public void should_Dispense()
    {
        var drug = _drugs.First();
        var currQuantity = drug.QuantityInStock;
        drug.Dispense(2);
        Assert.That(drug.QuantityInStock, Is.EqualTo(currQuantity-2));
        Log.Information($"{currQuantity} >> -2 >> {drug} ");
    }
    
    [Test]
    public void should_Adjust_Down()
    {
        var drug = _drugs.Last();
        var currQuantity = drug.QuantityInStock;
        drug.Adjust(-5);
        Assert.That(drug.QuantityInStock, Is.EqualTo(currQuantity-5));
        Log.Information($"{currQuantity} >> -5 >> {drug} ");
    }
    [Test]
    public void should_Adjust_Up()
    {
        var drug = _drugs.First();
        var currQuantity = drug.QuantityInStock;
        drug.Adjust(11);
        Assert.That(drug.QuantityInStock, Is.EqualTo(currQuantity+11));
        Log.Information($"{currQuantity} >> +11 >> {drug} ");
    }
}