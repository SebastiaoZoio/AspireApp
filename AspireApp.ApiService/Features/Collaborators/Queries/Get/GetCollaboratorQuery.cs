using AspireApp.ApiService.Features.Collaborators.Dtos;
using MediatR;

namespace AspireApp.ApiService.Features.Collaborators.Queries.Get;

public record GetCollaboratorQuery(Guid Id): IRequest<CollaboratorDto>;
