using AspireApp.Web.Services.Models;

namespace AspireApp.Web.Services.Requests;

public class DeleteCollaboratorsRequest
{
    public List<Collaborator> collaborators { get; set; }
}
