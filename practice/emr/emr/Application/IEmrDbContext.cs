using emr.Domain;
using Microsoft.EntityFrameworkCore;

namespace emr.Application;

public interface IEmrDbContext
{
    Task<int> Commit(CancellationToken cancellationToken);
    DbSet<Patient> Patients { get; }   
    DbSet<Prescription> Prescriptions { get; }  
    DbSet<DrugDispense> DrugDispenses { get; }  
}