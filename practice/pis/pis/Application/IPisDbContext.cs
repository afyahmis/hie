using Microsoft.EntityFrameworkCore;
using pis.Domain;

namespace pis.Application;

public interface IPisDbContext
{
    Task<int> Commit(CancellationToken cancellationToken);
    DbSet<Drug> Drugs { get; }  
    DbSet<Client> Clients { get; }   
    DbSet<MedicationRequest> Requests { get; }  
}