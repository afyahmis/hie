using Bogus;
using Bogus.Extensions.Belgium;
using FizzWare.NBuilder;
using pis.Application.Dtos;
using pis.Domain;

namespace pis.Tests.TestArtifacts;

public static class TestData
{
    public static List<Client> GetClients(int count = 2)
    {
        var list = Builder<Client>.CreateListOfSize(count)
            .All()
            .WithFactory(() =>
            {
                var person = new Faker().Person;
                return new Client(
                    Guid.NewGuid().ToString(),
                    person.NationalNumber(false),
                    person.FullName,
                    person.Gender.ToString(),
                    person.DateOfBirth);
            })
            .Build()
            .ToList();
        
        return list;
    }

    public static List<Client> GetClientsWithRequests(int count = 2)
    {
        var list = GetClients(count);
        foreach (var c in list)
        foreach (var r in GetRequests(c.Id))
            c.Requests.Add(r);
        return list;
    }

    public static List<MedicationRequest> GetRequests(Guid clientId, int count = 2)
    {
        var list = Builder<MedicationRequest>.CreateListOfSize(count)
            .All()
            .WithFactory(() =>
            {
                var drug = NewDrug();
                return new MedicationRequest(Guid.NewGuid().ToString(),
                    drug.Code, drug.Name, DateTime.Now.AddHours(-1),
                    clientId
                );
            })
            .Build()
            .ToList();

        return list;
    }
    public static List<Drug> GetDrugs(int count = 2)
    {
        var list = Builder<Drug>.CreateListOfSize(count)
            .All()
            .WithFactory(x=> NewDrug())
            .Build()
            .ToList();
        
        return list;
    }
    public static Drug NewDrug()
    {
        var code = GetDrugCode();
        var name = GetDrugDict()[code];
                
        return new Drug(
            name,
            code, new Random().Next(23,129)
        );
    }
    
    private static List<string> ListOfDrugs()
    {
        var list = new List<string>();
        list.Add("Diethylcarbamazine (citrate) Tablet 100mg|D101");
        list.Add("Praziquantel Tablet, Film-coated 200mg|D102");
        list.Add("Albendazole Tablet 400mg|D103");
        list.Add("Mebendazole Tablets, Chewable 500mg|D104");
        list.Add("Ivermectin Tablet 3mg|D105");
        list.Add("Ivermectin Tablet 6mg|D106");
        list.Add("Praziquantel Tablet, Film-coated 300mg|D107");
        list.Add("Albendazole Tablets, Chewable 400mg|D108");
        list.Add("Praziquantel Tablet, Film-coated 600mg|D109");
        list.Add("Albendazole Tablets, Chewable 200mg|D110");
        list.Add("Albendazole Tablets, Chewable 100mg|D111");
        list.Add("Miltefosine Capsules, hard 50mg|D112");
        list.Add("Azithromycin Tablet, Film-coated 500mg|D113");
        list.Add("Levonorgestrel Tablet, coated 30mcg|D114");
        return list;
    }
    private static Dictionary<string, string> GetDrugDict()
    {
        var dict = new Dictionary<string, string>();
        foreach (var d in ListOfDrugs())
            dict.Add(d.Split('|')[1], d.Split('|')[0]);
        return dict;
    }
    private static string GetDrugCode()
    {
        var drug = Pick<string>.RandomItemFrom(ListOfDrugs());
        return drug.Split('|')[1];
    }
}