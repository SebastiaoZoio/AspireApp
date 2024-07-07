using MediatR;

namespace AspireApp.ApiService.Features.Collaborators.Commands.Create;

public record CreateCollaboratorCommand(string Name) : IRequest<Guid>;