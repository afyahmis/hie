using Bogus;
using Bogus.Extensions.Belgium;
using FizzWare.NBuilder;
using pis.Domain;

namespace pis.Tests.TestArtifacts;

public static class TestData
{
    public static List<Client> GetClients(int count = 2)
    {
        var faker = new Faker().Person;
        var list = Builder<Client>.CreateListOfSize(count)
            .All()
            .With(x=>x.RefId=Guid.NewGuid().ToString())
            .With(x=>x.RefNo=faker.NationalNumber(false))
            .With(x=>x.FullName=faker.FullName)
            .With(x=>x.Sex=faker.Gender.ToString())
            .With(x=>x.BirthDate=faker.DateOfBirth)
            .Build()
            .ToList();
        
        return list;
    }
    
    public static List<Client> GetClientsWithRequests(int count = 2)
    {
        var list = Builder<Client>.CreateListOfSize(count)
            .All()
            .With(x=>x.RefId=Guid.NewGuid().ToString())
            .With(x=>x.RefNo=new Faker().Person.NationalNumber(false))
            .With(x=>x.FullName=new Faker().Person.FullName)
            .With(x=>x.Sex=new Faker().Person.Gender.ToString())
            .With(x=>x.BirthDate=new Faker().Person.DateOfBirth)
            .With(x=>x.Requests=GetRequests(x.Id))
            .Build()
            .ToList();
        
        return list;
    }
    
    public static List<MedicationRequest> GetRequests(Guid clientId, int count = 2)
    {
        var list = Builder<MedicationRequest>.CreateListOfSize(count)
            .All()
            .With(x=>x.ClientId=clientId)
            .With(x=>x.RefId=Guid.NewGuid().ToString())
            .With(x=>x.PrescriptionDrug=GetDrugName())
            .With(x=>x.DispenseDrugId=null)
            .With(x=>x.DispenseDate=null)
            .With(x=>x.Updated=null)
            .Build()
            .ToList();
        
        return list;
    }
    
    public static List<Drug> GetDrugs(int count = 2)
    {
        var list = Builder<Drug>.CreateListOfSize(count)
            .All()
            .WithFactory(x=>new Drug(
                GetDrugName(),
                GetDrugCode(),100
                ))
            .Build()
            .ToList();
        
        return list;
    }
    public static string GetDrugName()
    {
        return Pick<string>.RandomItemFrom(ListOfDrugs());
    }

    public static string GetDrugCode()
    {
        return new Faker().Random.String2(3, "ABCDEFGHIJKLMNOPQRSTUVWXYZ");
    }

    public static List<string> ListOfDrugs()
    {
        var list = new List<string>();
        list.Add("Diethylcarbamazine (citrate) Tablet 100mg");
        list.Add("Praziquantel Tablet, Film-coated 600mg");
        list.Add("Albendazole Tablet 400mg");
        list.Add("Mebendazole Tablets, Chewable 500mg");
        list.Add("Ivermectin Tablet 3mg");
        list.Add("Ivermectin Tablet 3mg");
        list.Add("Praziquantel Tablet, Film-coated 600mg");
        list.Add("Albendazole Tablets, Chewable 400mg");
        list.Add("Praziquantel Tablet, Film-coated 600mg");
        list.Add("Albendazole Tablets, Chewable 400mg");
        list.Add("Albendazole Tablets, Chewable 400mg");
        list.Add("Miltefosine Capsules, hard 50mg");
        list.Add("Azithromycin Tablet, Film-coated 500mg");
        list.Add("Levonorgestrel Tablet, coated 30mcg");
        return list;
    }

}