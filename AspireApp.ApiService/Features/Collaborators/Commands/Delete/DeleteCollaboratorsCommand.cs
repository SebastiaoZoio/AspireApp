using AspireApp.ApiService.Features.Collaborators.Dtos;
using MediatR;

namespace AspireApp.ApiService.Features.Collaborators.Commands.Delete;

public record DeleteCollaboratorsCommand(List<CollaboratorDto> Collaborators) : IRequest;
