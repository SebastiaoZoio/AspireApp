using AspireApp.ApiService.Features.Collaborators.Dtos;
using AspireApp.ApiService.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AspireApp.ApiService.Features.Collaborators.Queries.List;

public class ListCollaboratorsQueryHandler(AspireAppDbContext context) : IRequestHandler<ListCollaboratorsQuery, List<CollaboratorDto>>
{
    public async Task<List<CollaboratorDto>> Handle(ListCollaboratorsQuery query, CancellationToken cancellationToken)
    {
        return await context.Collaborators
            .Select(c => new CollaboratorDto(c.Id, c.Name, c.IsActive))
            .ToListAsync();
    }
}