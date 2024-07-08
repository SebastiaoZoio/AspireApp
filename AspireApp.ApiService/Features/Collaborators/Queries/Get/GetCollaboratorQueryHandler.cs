using AspireApp.ApiService.Features.Collaborators.Dtos;
using AspireApp.ApiService.Persistence;
using MediatR;

namespace AspireApp.ApiService.Features.Collaborators.Queries.Get;

public class GetProductQueryHandler(AspireAppDbContext context)
    : IRequestHandler<GetCollaboratorQuery, CollaboratorDto?>
{
    public async Task<CollaboratorDto?> Handle(GetCollaboratorQuery request, CancellationToken cancellationToken)
    {
        var collaborator = await context.Collaborators.FindAsync(request.Id);
        if (collaborator is null) return null;
        return new CollaboratorDto(collaborator.Id, collaborator.Name, collaborator.IsActive);
    }
}
