//using AspireApp.ApiService.Features.Collaborators.Commands.Create;
//using AspireApp.ApiService.Features.Collaborators.Commands.Delete;
//using AspireApp.ApiService.Features.Collaborators.Queries.Get;
//using AspireApp.ApiService.Features.Collaborators.Queries.List;
using AspireApp.ApiService.Persistence;
using MediatR;
using Microsoft.AspNetCore.Hosting.Server;
using System;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
//builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

var app = builder.Build();
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

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

//app.MapPost("/products", async (CreateProductCommand command, IMediator mediatr) =>
//{
//    var productId = await mediatr.Send(command);
//    if (Guid.Empty == productId) return Results.BadRequest();
//    await mediatr.Publish(new ProductCreatedNotification(productId));
//    return Results.Created($"/products/{productId}", new { id = productId });
//});

//app.MapDelete("/products/{id:guid}", async (Guid id, ISender mediatr) =>
//{
//    await mediatr.Send(new DeleteProductCommand(id));
//    return Results.NoContent();
//});

app.UseHttpsRedirection();
app.Run();
