using AspireApp.ApiService.Domain;
using AspireApp.ApiService.Domain.Enums;
using AspireApp.ApiService.Utils;
using AspireApp.ApiService.Features.Collaborators.Dtos;
using AspireApp.ApiService.Features.Collaborators.Responses;

namespace AspireApp.ApiService.Persistence.Interfaces
{
    public interface ICollaboratorRepository
    {
        Task<Collaborator> GetByIdAsync(Guid id);
        Task AddAsync(Collaborator collaborator); 
        Task DeleteAsync(Collaborator collaborator);
        Task SaveChangesAsync(CancellationToken cancellationToken);
        Task<ListCollaboratorsResponse> ListAsync(IEnumerable<FilterDescriptor> filters, int pageNumber, int pageSize, string sortString, SortDirection sortDirection);
    }
}
