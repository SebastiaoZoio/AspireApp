using AspireApp.ApiService.Features.Collaborators.Dtos;
using AspireApp.ApiService.Persistence;
using MediatR;

namespace AspireApp.ApiService.Features.Collaborators.Queries.Get;

public class GetProductQueryHandler(AppDbContext context)
    : IRequestHandler<GetCollaboratorQuery, CollaboratorDto?>
{
    public async Task<CollaboratorDto?> Handle(GetCollaboratorQuery request, CancellationToken cancellationToken)
    {
        var product = await context.Collaborators.FindAsync(request.Id);
        if (product is null) return null;
        return new CollaboratorDto(product.Id, product.Name);
    }
}
