using AspireApp.ApiService.Features.Collaborators.Responses;
using MediatR;
using BlazorBootstrap;

namespace AspireApp.ApiService.Features.Collaborators.Queries.List;

public record ListCollaboratorsQuery(
    IEnumerable<FilterItem>? Filters,
    int PageNumber = 1,
    int PageSize = 10,
    string SortString = "",
    SortDirection SortDirection = SortDirection.None) : IRequest<ListOfCollaboratorsResponse>;