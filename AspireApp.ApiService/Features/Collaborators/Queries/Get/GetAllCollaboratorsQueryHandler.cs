using AspireApp.ApiService.Features.Collaborators.Dtos;
using AspireApp.ApiService.Features.Collaborators.Queries.List;
using AspireApp.ApiService.Persistence;
using AspireApp.ApiService.Persistence.Interfaces;
using MediatR;

namespace AspireApp.ApiService.Features.Collaborators.Queries.Get;

public class GetAllCollaboratorsQueryHandler : IRequestHandler<GetAllCollaboratorsQuery, IEnumerable<CollaboratorDto>>
{
    private readonly ICollaboratorRepository _repository;

    public GetAllCollaboratorsQueryHandler(ICollaboratorRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<CollaboratorDto>> Handle(GetAllCollaboratorsQuery query, CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync();


    }
}