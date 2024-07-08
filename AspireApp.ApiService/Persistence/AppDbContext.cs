using AspireApp.ApiService.Domain;
using Microsoft.EntityFrameworkCore;

namespace AspireApp.ApiService.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<Collaborator> Collaborators { get; set; }

}
