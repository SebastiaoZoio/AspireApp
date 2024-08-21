using AspireApp.ApiService.Features.Collaborators.Responses;
using MediatR;
using BlazorBootstrap;

namespace AspireApp.ApiService.Features.Collaborators.Queries.Get;

public record GetFilteredCollaboratorsQuery(FilterItem? Filter) : IRequest<ListOfCollaboratorsResponse>;