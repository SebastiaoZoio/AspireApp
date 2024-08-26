using AspireApp.ApiService.Domain;
using Microsoft.EntityFrameworkCore;

namespace AspireApp.ApiService.Persistence;

public class AspireAppDbContext : DbContext
{
    public AspireAppDbContext(DbContextOptions<AspireAppDbContext> options) : base(options)
    {
    }
    public DbSet<Collaborator> Collaborators { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<AppointmentType> AppointmentTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Collaborator)
                .WithMany()
                .HasForeignKey(a => a.CollaboratorId);

        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.AppointmentType)
            .WithMany()
            .HasForeignKey(a => a.AppointmentTypeId);

        base.OnModelCreating(modelBuilder);
    }
}
