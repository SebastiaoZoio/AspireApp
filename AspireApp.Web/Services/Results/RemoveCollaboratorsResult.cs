namespace AspireApp.Web.Services.Results
{
    public class RemoveCollaboratorsResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<string> NotFoundCollaborators { get; set; } = new List<string>();
    }
}
