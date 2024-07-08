using AspireApp.ApiService.Domain;
using MediatR;
using AspireApp.ApiService.Persistence;
using AspireApp.ApiService.Persistence.Interfaces;

namespace AspireApp.ApiService.Features.Collaborators.Commands.Create;

public class CreateCollaboratorCommandHandler : IRequestHandler<CreateCollaboratorCommand, Guid>
{
    private readonly ICollaboratorRepository _repository;
    public CreateCollaboratorCommandHandler(ICollaboratorRepository repository)
    {
        _repository = repository;
    }
    public async Task<Guid> Handle(CreateCollaboratorCommand command, CancellationToken cancellationToken)
    {
        var collaborator = new Collaborator(command.Name);
        await _repository.AddAsync(collaborator);
        return collaborator.Id;
    }
}
