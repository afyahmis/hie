using emr.Application;
using emr.Domain;
using Microsoft.EntityFrameworkCore;

namespace emr.Data;

public class EmrDbContext: DbContext, IEmrDbContext
{
    public Task<int> Commit(CancellationToken cancellationToken)
    {
        return SaveChangesAsync(cancellationToken);
    }
    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<Prescription> Prescriptions => Set<Prescription>();
    public DbSet<DrugDispense> DrugDispenses => Set<DrugDispense>();


    public EmrDbContext(DbContextOptions<EmrDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Patient>().HasData(
            new Patient("J01","John Doe","M",new DateTime(1990,2,17)),
            new Patient("M03","Mary Jane","F",new DateTime(1992,5,14)),
            new Patient("A02","Ali Jones","M",new DateTime(2020,11,12))
        );
    }
}