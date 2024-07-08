using AspireApp.ApiService.Features.Collaborators.Dtos;
using MediatR;

namespace AspireApp.ApiService.Features.Collaborators.Queries.List;

public record ListCollaboratorsQuery : IRequest<IEnumerable<CollaboratorDto>>;