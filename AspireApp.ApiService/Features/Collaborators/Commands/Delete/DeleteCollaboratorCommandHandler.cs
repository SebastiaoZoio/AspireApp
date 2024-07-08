using AspireApp.ApiService.Exceptions;
using AspireApp.ApiService.Persistence.Interfaces;
using MediatR;

namespace AspireApp.ApiService.Features.Collaborators.Commands.Delete;

public class DeleteCollaboratorCommandHandler: IRequestHandler<DeleteCollaboratorCommand>
{
    private readonly ICollaboratorRepository _repository;

    public DeleteCollaboratorCommandHandler(ICollaboratorRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteCollaboratorCommand request, CancellationToken cancellationToken)
    {
        var collaborator = await _repository.GetByIdAsync(request.Id);

        if (collaborator == null)
        {
            throw new CollaboratorNotFoundException(request.Id);
        }

        collaborator.IsActive = false;
        await _repository.SaveChangesAsync(cancellationToken);

        return;
    }
}

