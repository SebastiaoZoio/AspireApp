using AspireApp.ApiService.Domain;

namespace AspireApp.ApiService.Persistence.Interfaces;

public interface IAppointmentTypeRepository
{
    Task<IEnumerable<AppointmentType>> GetAllAsync();
    Task<AppointmentType> GetByIdAsync(int id);
    Task AddAsync(AppointmentType appointmentType);
    Task UpdateAsync(AppointmentType appointmentType);
    Task DeleteAsync(int id);
}