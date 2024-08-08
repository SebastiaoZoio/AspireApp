using AspireApp.ApiService.Domain;
using AspireApp.ApiService.Exceptions;
using AspireApp.ApiService.Persistence.Interfaces;
using MediatR;

namespace AspireApp.ApiService.Features.Collaborators.Commands.Delete;

public class DeleteCollaboratorsCommandHandler : IRequestHandler<DeleteCollaboratorsCommand>
{
    private readonly ICollaboratorRepository _repository;

    public DeleteCollaboratorsCommandHandler(ICollaboratorRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteCollaboratorsCommand request, CancellationToken cancellationToken)
    {
        var collaborators = await _repository.GetByIdsAsync(request.Collaborators.Select(c => c.Id).ToList());

        foreach (var collaborator in collaborators)
        {
            collaborator.IsActive = false;
        }

        await _repository.SaveChangesAsync(cancellationToken);


        if (collaborators.Count != request.Collaborators.Count)
        {
            HashSet<Guid> collaboratorIds = new HashSet<Guid>(collaborators.Select(c => c.Id));

            var notFoundCollaborators = request.Collaborators
                .Where(dto => !collaboratorIds.Contains(dto.Id))
                .Select(dto => dto.Name)
                .ToList();

            throw new CollaboratorsNotFoundException(notFoundCollaborators);
        }
    }
}