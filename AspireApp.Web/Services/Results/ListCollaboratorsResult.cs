using AspireApp.Web.Services.Enums;
using AspireApp.Web.Services.Models;

namespace AspireApp.Web.Services.Results;

public class ListCollaboratorsResult
{
    public bool Success { get; set; } = true;
    public IEnumerable<Collaborator>? Collaborators { get; set; }
    public int? TotalCount { get; set; }
    public string? ErrorMessage { get; set; }
    public ErrorType? ErrorType { get; set; }
}
