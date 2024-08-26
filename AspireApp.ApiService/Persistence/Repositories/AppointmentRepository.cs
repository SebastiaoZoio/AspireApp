using AspireApp.ApiService.Persistence.Interfaces;
using AspireApp.ApiService.Domain;
using Microsoft.EntityFrameworkCore;

namespace AspireApp.ApiService.Persistence.Repositories;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly AspireAppDbContext _context;

    public AppointmentRepository(AspireAppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Appointment>> GetAllAsync()
    {
        return await _context.Appointments
            .Include(a => a.Collaborator) 
            .Include(a => a.AppointmentType) 
            .ToListAsync();
    }

    public async Task<Appointment> GetByIdAsync(Guid id)
    {
        return await _context.Appointments
            .Include(a => a.Collaborator) 
            .Include(a => a.AppointmentType) 
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task AddAsync(Appointment appointment)
    {
        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Appointment appointment)
    {
        _context.Appointments.Update(appointment);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var appointment = await _context.Appointments.FindAsync(id);
        if (appointment != null)
        {
            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
        }
    }
}
