using Bogus;
using emr.Application.Commands;
using emr.Application.Dtos;
using emr.Application.Queries;
using emr.Data;
using emr.Domain;
using emr.Tests.TestArtifacts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace emr.Tests.Application.Commands;

[TestFixture]
public class DispenseDrugsTests
{
    private IMediator _mediator;
    private Patient _patient;

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
    public async Task should_Dispense()
    {
        var newDispenseDtos=GetNewDispenses();

        var res = await _mediator.Send(new DispenseDrugs(newDispenseDtos));
        Assert.That(res.IsSuccess,Is.True);

        var saved = FindPatient(_patient.Id).Prescriptions.SelectMany(x=>x.Dispenses);
        Assert.That(saved.Any(x=>x.DispenseRef==newDispenseDtos.First().DispenseRef),Is.True);
    }

    private List<NewDispenseDto> GetNewDispenses()
    {
        var list = new List<NewDispenseDto>();
        foreach (var prescription in _patient.Prescriptions)
        {
            list.Add(new NewDispenseDto()
            {
                Date = DateTime.Now,
                Drug = prescription.Drug,
                DispenseRef = new Faker().Finance.Account(),
                PrescriptionId = prescription.Id,
            });
        }
        return list;
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
        _patient = TestData.GetPatientWithPrescription();
        var ctx= TestInitializer.ServiceProvider.GetRequiredService<EmrDbContext>();
        ctx.Add(_patient);
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