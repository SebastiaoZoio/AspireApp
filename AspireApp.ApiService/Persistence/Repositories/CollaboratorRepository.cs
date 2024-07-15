﻿using AspireApp.ApiService.Domain;
using AspireApp.ApiService.Domain.Enums;
using AspireApp.ApiService.Features.Collaborators.Dtos;
using AspireApp.ApiService.Features.Collaborators.Queries.List;
using AspireApp.ApiService.Features.Collaborators.Responses;
using AspireApp.ApiService.Persistence.Interfaces;
using AspireApp.ApiService.Utils;
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

    public async Task<ListCollaboratorsResponse> ListAsync(IEnumerable<FilterDescriptor> filters, int pageNumber, int pageSize, string sortString, SortDirection sortDirection)
    {
        var query = _context.Collaborators.AsQueryable();

        foreach (var filter in filters)
        {
            if (filter.Operator == "Contains" && filter.Value is string stringValue)
            {
                query = query.Where(c => EF.Property<string>(c, filter.PropertyName).Contains(stringValue));
            }
            // Add other filter types as needed
        }

        if (!string.IsNullOrEmpty(sortString))
        {
            query = sortDirection == SortDirection.Ascending
                ? query.OrderBy(c => EF.Property<object>(c, sortString))
                : query.OrderByDescending(c => EF.Property<object>(c, sortString));
        }

        var totalCount = await query.CountAsync();

        query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

        var data = await query.Select(c => new CollaboratorDto(c.Id, c.Name, c.IsActive)).ToListAsync();

        var response = new ListCollaboratorsResponse() 
        {
            Collaborators = data, 
            TotalCount = totalCount
        };

        return response;
    }
}