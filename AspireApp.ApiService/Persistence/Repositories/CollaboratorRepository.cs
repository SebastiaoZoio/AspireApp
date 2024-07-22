using AspireApp.ApiService.Domain;
using AspireApp.ApiService.Features.Collaborators.Dtos;
using AspireApp.ApiService.Features.Collaborators.Responses;
using AspireApp.ApiService.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using BlazorBootstrap;

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

    public async Task<IEnumerable<CollaboratorDto>> GetAllAsync()
    {
        return await _context.Collaborators
            .Select(c => new CollaboratorDto(c.Id, c.Name, c.IsActive))
            .ToListAsync();
    }

    public async Task<ListCollaboratorsResponse> ListAsync(IEnumerable<FilterItem> filters, int pageNumber, int pageSize, string sortString, SortDirection sortDirection)
    {
        var query = _context.Collaborators.AsQueryable();

        // Apply filters
        if (filters != null)
        {
            query = filters.Aggregate(query, (currentQuery, filter) => GetFilteredQuery(currentQuery, filter));
        }

        // Apply sorting
        if (!string.IsNullOrEmpty(sortString))
        {
            query = sortDirection == SortDirection.Ascending
                ? query.OrderBy(c => EF.Property<object>(c, sortString))
                : query.OrderByDescending(c => EF.Property<object>(c, sortString));
        }

        // Apply pagination
        query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

        // Fetch data
        var data = await query.Select(c => new CollaboratorDto(c.Id, c.Name, c.IsActive)).ToListAsync();

        // Create response
        return new ListCollaboratorsResponse
        {
            Collaborators = data,
            TotalCount = data.Count()
        };
    }

    private IQueryable<Collaborator> GetFilteredQuery(IQueryable<Collaborator> query, FilterItem filter)
    {
        switch (filter.Operator)
        {
            case FilterOperator.Equals:
                return query.Where(p => EF.Property<string>(p, filter.PropertyName).Equals(filter.Value));
            case FilterOperator.NotEquals:
                return query.Where(p => !EF.Property<string>(p, filter.PropertyName).Equals(filter.Value));
            case FilterOperator.Contains:
                return query.Where(p => EF.Property<string>(p, filter.PropertyName).Contains(filter.Value));
            case FilterOperator.StartsWith:
                return query.Where(p => EF.Property<string>(p, filter.PropertyName).StartsWith(filter.Value));
            case FilterOperator.EndsWith:
                return query.Where(p => EF.Property<string>(p, filter.PropertyName).EndsWith(filter.Value));
            case FilterOperator.DoesNotContain:
                return query.Where(p => !EF.Property<string>(p, filter.PropertyName).Contains(filter.Value));
            case FilterOperator.IsNull:
                return query.Where(p => EF.Property<string>(p, filter.PropertyName) == null);
            case FilterOperator.IsEmpty:
                return query.Where(p => EF.Property<string>(p, filter.PropertyName) == string.Empty);
            case FilterOperator.IsNotNull:
                return query.Where(p => EF.Property<string>(p, filter.PropertyName) != null);
            case FilterOperator.IsNotEmpty:
                return query.Where(p => EF.Property<string>(p, filter.PropertyName).Length > 0);
            default:
                return query;
        }
    }
}