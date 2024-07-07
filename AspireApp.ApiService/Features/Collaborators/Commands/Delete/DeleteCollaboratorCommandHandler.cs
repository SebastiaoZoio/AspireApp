using AspireApp.ApiService.Domain;
using AspireApp.ApiService.Persistence;
using MediatR;

namespace AspireApp.ApiService.Features.Collaborators.Commands.Delete;

public class DeleteCollaboratorCommandHandler(AppDbContext context) : IRequestHandler<DeleteCollaboratorCommand>
{
    public async Task Handle(DeleteCollaboratorCommand request, CancellationToken cancellationToken)
    {
        var collaborator = await context.Collaborators.FindAsync(request.Id);
        if (collaborator is null) return;
        context.Collaborators.Remove(collaborator);
        await context.SaveChangesAsync();
    }
}

