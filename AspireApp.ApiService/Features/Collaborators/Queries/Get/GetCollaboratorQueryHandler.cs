using AspireApp.ApiService.Features.Collaborators.Dtos;
using AspireApp.ApiService.Persistence;
using AspireApp.ApiService.Persistence.Interfaces;
using MediatR;

namespace AspireApp.ApiService.Features.Collaborators.Queries.Get;

public class GetProductQueryHandler : IRequestHandler<GetCollaboratorQuery, CollaboratorDto?>
{
    private readonly ICollaboratorRepository _repository;

    public GetProductQueryHandler(ICollaboratorRepository repository)
    {
    _repository = repository; 
    }

    public async Task<CollaboratorDto?> Handle(GetCollaboratorQuery request, CancellationToken cancellationToken)
    {
        var collaborator = await _repository.GetByIdAsync(request.Id);
        if (collaborator is null) return null;
        return new CollaboratorDto(collaborator.Id, collaborator.Name, collaborator.IsActive);
    }
}
