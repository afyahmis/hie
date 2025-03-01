using Microsoft.EntityFrameworkCore;
using pis.Application;
using pis.Domain;

namespace pis.Data;

public class PisDbContext: DbContext, IPisDbContext
{
    public Task<int> Commit(CancellationToken cancellationToken)
    {
        return SaveChangesAsync(cancellationToken);
    }
    public DbSet<Drug> Drugs => Set<Drug>();
    public DbSet<Client> Clients => Set<Client>();
    public DbSet<MedicationRequest> Requests => Set<MedicationRequest>();
    


    public PisDbContext(DbContextOptions<PisDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Drug>().HasData(
            new Drug("Diethylcarbamazine (citrate) Tablet 100mg","D101",500.0),
            new Drug("Praziquantel Tablet, Film-coated 600mg","D102",500.0),
            new Drug("Albendazole Tablet 400mg","D103",500.0),
            new Drug("Mebendazole Tablets, Chewable 500mg","D104",500.0),
            new Drug("Ivermectin Tablet 3mg","D105",500.0),
            new Drug("Ivermectin Tablet 3mg","D106",500.0),
            new Drug("Praziquantel Tablet, Film-coated 600mg","D107",500.0),
            new Drug("Albendazole Tablets, Chewable 400mg","D108",500.0),
            new Drug("Praziquantel Tablet, Film-coated 600mg","D109",500.0),
            new Drug("Albendazole Tablets, Chewable 400mg","D110",500.0),
            new Drug("Albendazole Tablets, Chewable 400mg","D111",500.0),
            new Drug("Miltefosine Capsules, hard 50mg","D112",500.0),
            new Drug("Azithromycin Tablet, Film-coated 500mg","D113",500.0),
            new Drug("Levonorgestrel Tablet, coated 30mcg","D114",500.0)
        );
    }
}