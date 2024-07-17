using AspireApp.ApiService.Features.Collaborators.Dtos;
using AspireApp.ApiService.Domain.Enums;
using AspireApp.ApiService.Features.Collaborators.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AspireApp.ApiService.Features.Collaborators.Queries.List;

public record ListCollaboratorsQuery(
    IEnumerable<FilterItem>? Filters,
    int PageNumber = 1,
    int PageSize = 10,
    string SortString = "",
    SortDirection SortDirection = SortDirection.None) : IRequest<ListCollaboratorsResponse>;