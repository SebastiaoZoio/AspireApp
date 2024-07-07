using MediatR;

namespace AspireApp.ApiService.Features.Collaborators.Commands.Delete;

public record DeleteCollaboratorCommand(Guid Id) : IRequest;