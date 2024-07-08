using AspireApp.ApiService.Domain;

namespace AspireApp.ApiService.Persistence.Interfaces
{
    public interface ICollaboratorRepository
    {
        Task<Collaborator> GetByIdAsync(Guid id);
        Task AddAsync(Collaborator collaborator); 
        Task DeleteAsync(Collaborator collaborator);
        Task SaveChangesAsync(CancellationToken cancellationToken);
        Task<IEnumerable<Collaborator>> GetAllAsync();
    }
}
