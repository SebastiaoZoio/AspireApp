using AspireApp.ApiService.Domain;
using Microsoft.EntityFrameworkCore;

namespace AspireApp.ApiService.Persistence;

public class AspireAppDbContext : DbContext
{
    public AspireAppDbContext(DbContextOptions<AspireAppDbContext> options) : base(options)
    {
    }
    public DbSet<Collaborator> Collaborators { get; set; }

}
