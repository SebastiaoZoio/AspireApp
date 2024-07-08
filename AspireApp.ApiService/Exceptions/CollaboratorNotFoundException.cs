namespace AspireApp.ApiService.Exceptions;

public class CollaboratorNotFoundException : Exception
{
    public CollaboratorNotFoundException(Guid collaboratorId)
        : base($"Collaborator with ID '{collaboratorId}' was not found.")
    {
    }
}