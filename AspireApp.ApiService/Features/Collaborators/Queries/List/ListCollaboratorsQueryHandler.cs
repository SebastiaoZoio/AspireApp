using AspireApp.ApiService.Features.Collaborators.Dtos;
using AspireApp.ApiService.Persistence;
using AspireApp.ApiService.Persistence.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AspireApp.ApiService.Features.Collaborators.Queries.List;

public class ListCollaboratorsQueryHandler : IRequestHandler<ListCollaboratorsQuery, IEnumerable<CollaboratorDto>>
{    
    private readonly ICollaboratorRepository _repository;

    public ListCollaboratorsQueryHandler(ICollaboratorRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<CollaboratorDto>> Handle(ListCollaboratorsQuery query, CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync();


    }
}