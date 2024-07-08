using AspireApp.ApiService.Domain;
using AspireApp.ApiService.Features.Collaborators.Dtos;

namespace AspireApp.ApiService.Persistence.Interfaces
{
    public interface ICollaboratorRepository
    {
        Task<Collaborator> GetByIdAsync(Guid id);
        Task AddAsync(Collaborator collaborator); 
        Task DeleteAsync(Collaborator collaborator);
        Task SaveChangesAsync(CancellationToken cancellationToken);
        Task<IEnumerable<CollaboratorDto>> GetAllAsync();
    }
}
