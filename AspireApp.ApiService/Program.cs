using AspireApp.ApiService.Domain;
using AspireApp.ApiService.Features.Collaborators.Commands.Create;
using AspireApp.ApiService.Features.Collaborators.Commands.Delete;
using AspireApp.ApiService.Features.Collaborators.Queries.Get;
using AspireApp.ApiService.Features.Collaborators.Queries.List;
using AspireApp.ApiService.Persistence;
using Google.Protobuf.Compiler;
using MediatR;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

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

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connection));

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.MapGet("/products/{id:guid}", async (Guid id, ISender mediatr) =>
//{
//    var product = await mediatr.Send(new GetProductQuery(id));
//    if (product == null) return Results.NotFound();
//    return Results.Ok(product);
//});

//app.MapGet("/products", async (ISender mediatr) =>
//{
//    var products = await mediatr.Send(new ListProductsQuery());
//    return Results.Ok(products);
//});

app.MapPost("/collaborator", async (CreateCollaboratorCommand command, IMediator mediatr) =>
{
    var collaboratorId = await mediatr.Send(command);
    if (Guid.Empty == collaboratorId) return Results.BadRequest();
    return Results.Created($"/collaborator/{collaboratorId}", new { id = collaboratorId });
});

//app.MapDelete("/products/{id:guid}", async (Guid id, ISender mediatr) =>
//{
//    await mediatr.Send(new DeleteProductCommand(id));
//    return Results.NoContent();
//});

app.UseHttpsRedirection();
app.Run();
