using AspireApp.ApiService.Exceptions;
using AspireApp.ApiService.Features.Collaborators.Commands.Create;
using AspireApp.ApiService.Features.Collaborators.Commands.Delete;
using AspireApp.ApiService.Features.Collaborators.Queries.Get;
using AspireApp.ApiService.Features.Collaborators.Queries.List;
using AspireApp.ApiService.Persistence;
using AspireApp.ApiService.Persistence.Interfaces;
using AspireApp.ApiService.Persistence.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.AddScoped<ICollaboratorRepository, CollaboratorRepository>();

var connection = String.Empty;
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddEnvironmentVariables().AddJsonFile("appsettings.Development.json");
    connection = builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
}
else
{
    connection = Environment.GetEnvironmentVariable("AZURE_SQL_CONNECTIONSTRING");
}

builder.Services.AddDbContext<AspireAppDbContext>(options =>
    options.UseSqlServer(connection));

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/collaborators/{id:guid}", async (Guid id, ISender mediatr) =>
{
    var collaborator = await mediatr.Send(new GetCollaboratorQuery(id));
    if (collaborator == null) return Results.NotFound();
    return Results.Ok(collaborator);
});

//app.MapGet("/collaborators", async (ISender mediatr) =>
//{
//    var collaborators = await mediatr.Send(new GetAllCollaboratorsQuery());
//    return Results.Ok(collaborators);
//});

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

app.UseHttpsRedirection();
app.Run();
