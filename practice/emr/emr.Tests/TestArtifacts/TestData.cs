using Bogus;
using Bogus.Extensions.Belgium;
using emr.Domain;
using FizzWare.NBuilder;

namespace emr.Tests.TestArtifacts;

public static class TestData
{
    public static List<Patient> GetPatients(int count = 2)
    {
        var faker = new Faker().Person;
        var list = Builder<Patient>.CreateListOfSize(count)
            .All()
            .WithFactory(()=> new Patient(
                faker.NationalNumber(false),
                faker.FullName,
                faker.Gender.ToString(),
                faker.DateOfBirth
            ))
            .Build()
            .ToList();
        
        return list;
    }
    
    public static Patient GetPatient()
    {
        return GetPatients(1).First();
    }
    
    public static Patient GetPatientWithPrescription()
    {
        var p = GetPatient();
        p.Prescribe(GetDrug());
        p.Prescribe(GetDrug());
        return p;
    }
    
    public static List<Prescription> GetPrescriptions(Guid patientId, int count = 2)
    {
        var list = Builder<Prescription>.CreateListOfSize(count)
            .All()
            .WithFactory(()=> new Prescription(GetDrug(),patientId))
            .Build()
            .ToList();
        
        return list;
    }
    
    public static List<Prescription> GetPrescriptionWithDispense(Guid patientId, int count = 2)
    {
        var list = Builder<Prescription>.CreateListOfSize(count)
            .All()
            .WithFactory(()=> new Prescription(GetDrug(),patientId))
            .Build()
            .ToList();

        foreach (var prescription in list)
        {
            prescription.Dispense(prescription.Drug,DateTime.Now,new Faker().Finance.Account());
        }
        return list;
    }

    public static string GetDrug()
    {
        return Pick<string>.RandomItemFrom(ListOfDrugs());
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