using AspireApp.ApiService.Features.Collaborators.Dtos;
using AspireApp.ApiService.Features.Collaborators.Responses;
using AspireApp.ApiService.Persistence;
using AspireApp.ApiService.Persistence.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AspireApp.ApiService.Features.Collaborators.Queries.List;

public class ListCollaboratorsQueryHandler : IRequestHandler<ListCollaboratorsQuery, ListCollaboratorsResponse>
{    
    private readonly ICollaboratorRepository _repository;

    public ListCollaboratorsQueryHandler(ICollaboratorRepository repository)
    {
        _repository = repository;
    }

    public async Task<ListCollaboratorsResponse> Handle(ListCollaboratorsQuery query, CancellationToken cancellationToken)
    {
        return await _repository.ListAsync(query.Filters, query.PageNumber, query.PageSize, query.SortString, query.SortDirection);


    }
}