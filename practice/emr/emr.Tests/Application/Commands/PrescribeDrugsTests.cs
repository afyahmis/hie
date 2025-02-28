using emr.Application.Commands;
using emr.Application.Dtos;
using emr.Data;
using emr.Domain;
using emr.Tests.TestArtifacts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace emr.Tests.Application.Commands;
[TestFixture]
public class PrescribeDrugsTests
{
    private IMediator _mediator;
    private List<Patient> _patients = new();

    [OneTimeSetUp]
    public void Init()
    {
        InitData();
    }

    [SetUp]
    public void SetUp()
    {
        _mediator = TestInitializer.ServiceProvider.GetRequiredService<IMediator>();
    }
    
    [Test]
    public async Task should_Prescribe()
    {
        var prescriptionDto = new NewPrescriptionDto()
        {
            PatientId = _patients.First().Id,
            Medications = GetList()
        };
        var res = await _mediator.Send(new PrescribeDrugs(prescriptionDto));
        Assert.That(res.IsSuccess,Is.True);

        var saved = FindPatient(prescriptionDto.PatientId).Prescriptions.ToList();
        Assert.That(saved.Any(x=>x.Drug==prescriptionDto.Medications.First().Drug),Is.True);

    }

    private List<DrugDto> GetList()
    {
        var drugs = TestData.ListOfDrugs().Take(5);

        return drugs.Select(x => new DrugDto()
        {
            Drug = x
        }).ToList();
    }

    private void InitData()
    {
        _patients = TestData.GetPatients(5);
        
       var ctx= TestInitializer.ServiceProvider.GetRequiredService<EmrDbContext>();
       ctx.AddRange(_patients);
       ctx.SaveChanges();
    }
    
    private Patient? FindPatient(Guid id)
    {
       var ctx= TestInitializer.ServiceProvider.GetRequiredService<EmrDbContext>();
        var p= ctx.Patients.AsNoTracking()
            .Include(x=>x.Prescriptions)
            .ThenInclude(p=>p.Dispenses)
            .FirstOrDefault(x=>x.Id==id);
        return p;
    }
}
