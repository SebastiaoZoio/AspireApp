using AspireApp.ApiService.Exceptions;
using AspireApp.ApiService.Features.Collaborators.Commands.Create;
using AspireApp.ApiService.Features.Collaborators.Commands.Delete;
using AspireApp.ApiService.Features.Collaborators.Queries.Get;
using AspireApp.ApiService.Features.Collaborators.Queries.List;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AspireApp.ApiService.Routes;

public static class CollaboratorRoutes
{
    public static void MapCollaboratorEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/collaborators/{id:guid}", async (Guid id, ISender mediatr) =>
        {
            var collaborator = await mediatr.Send(new GetCollaboratorQuery(id));
            if (collaborator == null) return Results.NotFound();
            return Results.Ok(collaborator);
        });

        app.MapGet("/collaborators", async (ISender mediatr) =>
        {
            var collaborators = await mediatr.Send(new GetAllCollaboratorsQuery());
            return Results.Ok(collaborators);
        });

        app.MapPost("/filtered-collaborators", async ([FromBody] GetFilteredCollaboratorsQuery query, ISender mediatr) =>
        {
            var collaboratorsListResponse = await mediatr.Send(query);
            return Results.Ok(collaboratorsListResponse);
        });

        app.MapPost("/list-collaborators", async ([FromBody] ListCollaboratorsQuery query, ISender mediatr) =>
        {
            var collaboratorsListResponse = await mediatr.Send(query);
            return Results.Ok(collaboratorsListResponse);
        });

        app.MapPost("/collaborator", async (CreateCollaboratorCommand command, IMediator mediatr) =>
        {
            var collaboratorId = await mediatr.Send(command);
            if (Guid.Empty == collaboratorId) return Results.BadRequest();
            return Results.Created($"/collaborator/{collaboratorId}", new { id = collaboratorId });
        });

        app.MapPost("/delete-collaborator/{id:guid}", async (Guid id, ISender mediatr) =>
        {
            try
            {
                await mediatr.Send(new DeleteCollaboratorCommand(id));
                return Results.Ok();
            }
            catch (CollaboratorNotFoundException ex)
            {
                return Results.NotFound(new { message = ex.Message });
            }
        });

        app.MapPost("/delete-collaborators", async (DeleteCollaboratorsCommand command, IMediator mediatr) =>
        {
            try
            {
                await mediatr.Send(command);
                return Results.Ok();
            }
            catch (CollaboratorsNotFoundException ex)
            {
                return Results.NotFound(new
                {
                    message = ex.Message,
                    names = ex.CollaboratorNames
                });
            }
        });
    }
}