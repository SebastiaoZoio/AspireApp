namespace AspireApp.ApiService.Domain;

public class Collaborator
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;


    private Collaborator() { } 

    public Collaborator(string name)
    {
        Id = Guid.NewGuid();
        Name = name; 
    }
}

