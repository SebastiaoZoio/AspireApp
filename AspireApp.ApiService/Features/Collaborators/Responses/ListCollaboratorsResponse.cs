using AspireApp.ApiService.Features.Collaborators.Dtos;

namespace AspireApp.ApiService.Features.Collaborators.Responses;

public class ListCollaboratorsResponse
{
    public IEnumerable<CollaboratorDto> Collaborators { get; set; }
    public int TotalCount { get; set; }
}
