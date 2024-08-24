using AspireApp.ApiService.Domain;
using AspireApp.ApiService.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AspireApp.ApiService.Persistence.Repositories;

public class AppointmentTypeRepository : IAppointmentTypeRepository
{
    private readonly AspireAppDbContext _context;

    public AppointmentTypeRepository(AspireAppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AppointmentType>> GetAllAsync()
    {
        return await _context.AppointmentTypes.ToListAsync();
    }

    public async Task<AppointmentType> GetByIdAsync(int id)
    {
        return await _context.AppointmentTypes.FindAsync(id);
    }

    public async Task AddAsync(AppointmentType appointmentType)
    {
        _context.AppointmentTypes.Add(appointmentType);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(AppointmentType appointmentType)
    {
        _context.AppointmentTypes.Update(appointmentType);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var appointmentType = await _context.AppointmentTypes.FindAsync(id);
        if (appointmentType != null)
        {
            _context.AppointmentTypes.Remove(appointmentType);
            await _context.SaveChangesAsync();
        }
    }
}