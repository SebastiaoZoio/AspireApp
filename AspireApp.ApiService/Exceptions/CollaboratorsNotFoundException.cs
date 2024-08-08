namespace AspireApp.ApiService.Exceptions;

public class CollaboratorsNotFoundException : Exception
{
    public IEnumerable<string> CollaboratorNames { get; }
    public CollaboratorsNotFoundException(IEnumerable<string> collaboratorNames)
        : base(GenerateMessage(collaboratorNames))
    {
        CollaboratorNames = collaboratorNames;
    }

    private static string GenerateMessage(IEnumerable<string> collaboratorNames)
    {
        var namesList = string.Join(", ", collaboratorNames);
        return $"Collaborators with names [{namesList}] were not found.";
    }
}
