using AspireApp.ApiService.Domain;
using MediatR;
using AspireApp.ApiService.Persistence;

namespace AspireApp.ApiService.Features.Collaborators.Commands.Create;

public class CreateCollaboratorCommandHandler(AppDbContext context) : IRequestHandler<CreateCollaboratorCommand, Guid>
{
    public async Task<Guid> Handle(CreateCollaboratorCommand command, CancellationToken cancellationToken)
    {
        var collaborator = new Collaborator(command.Name);
        await context.Collaborators.AddAsync(collaborator);
        await context.SaveChangesAsync();
        return collaborator.Id;
    }
}
