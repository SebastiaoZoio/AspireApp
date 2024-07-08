using AspireApp.ApiService.Domain;
using AspireApp.ApiService.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AspireApp.ApiService.Persistence.Repositories;

public class CollaboratorRepository : ICollaboratorRepository
{
    private readonly AspireAppDbContext _context;

    public CollaboratorRepository(AspireAppDbContext context)
    {
        _context = context;
    }

    public async Task<Collaborator> GetByIdAsync(Guid id)
    {
        return await _context.Collaborators.FindAsync(id);
    }

    public async Task AddAsync(Collaborator collaborator)
    {
        _context.Collaborators.Add(collaborator);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Collaborator collaborator)
    {
        _context.Collaborators.Remove(collaborator);
        await _context.SaveChangesAsync();
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<Collaborator>> GetAllAsync()
    {
        return await _context.Collaborators.ToListAsync();
    }
}