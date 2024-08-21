using AspireApp.ApiService.Features.Collaborators.Responses;
using AspireApp.ApiService.Persistence.Interfaces;
using MediatR;

namespace AspireApp.ApiService.Features.Collaborators.Queries.Get;

public class GetFilteredCollaboratorsQueryHandler : IRequestHandler<GetFilteredCollaboratorsQuery, ListOfCollaboratorsResponse>
{
    private readonly ICollaboratorRepository _repository;

    public GetFilteredCollaboratorsQueryHandler(ICollaboratorRepository repository)
    { 
        _repository = repository;
    }

    public async Task<ListOfCollaboratorsResponse> Handle(GetFilteredCollaboratorsQuery query, CancellationToken cancellationToken)
    {
        return await _repository.GetFilteredAsync(query.Filter);
    }
}
