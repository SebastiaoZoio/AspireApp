using AspireApp.ApiService.Domain;
using AspireApp.ApiService.Features.Collaborators.Dtos;
using AspireApp.ApiService.Features.Collaborators.Responses;
using BlazorBootstrap;

namespace AspireApp.ApiService.Persistence.Interfaces
{
    public interface ICollaboratorRepository
    {
        Task<Collaborator> GetByIdAsync(Guid id);
        Task AddAsync(Collaborator collaborator); 
        Task DeleteAsync(Collaborator collaborator);
        Task SaveChangesAsync(CancellationToken cancellationToken);
        Task<IEnumerable<CollaboratorDto>> GetAllAsync();
        Task<ListCollaboratorsResponse> ListAsync(IEnumerable<FilterItem> filters, int pageNumber, int pageSize, string sortString, SortDirection sortDirection);
    }
}
