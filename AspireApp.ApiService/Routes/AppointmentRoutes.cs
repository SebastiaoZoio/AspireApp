using AspireApp.ApiService.Features.Appointments.Commands.Create;
using AspireApp.ApiService.Features.Appointments.Commands.Delete;
using AspireApp.ApiService.Features.Appointments.Queries.Get;
using AspireApp.ApiService.Features.Appointments.Queries.List;
using AspireApp.ApiService.Exceptions;
using MediatR;
using System.Text.Json;

namespace AspireApp.ApiService.Routes;

public static class AppointmentRoutes
{
    public static void MapAppointmentEndpoints(this IEndpointRouteBuilder app)
    {
        // Get a specific appointment by ID
        app.MapGet("/appointments/{id:guid}", async (Guid id, ISender mediatr) =>
        {
            var appointment = await mediatr.Send(new GetAppointmentQuery(id));
            if (appointment == null) return Results.NotFound();
            return Results.Ok(appointment);
        });

        // Get all appointments
        app.MapGet("/appointments", async (ISender mediatr) =>
        {
            var appointments = await mediatr.Send(new ListAppointmentsQuery());
            return Results.Ok(appointments);
        });


        // Create a new appointment
        app.MapPost("/appointment", async (CreateAppointmentCommand command, IMediator mediatr) =>
        {
            var appointmentId = await mediatr.Send(command);
            if (Guid.Empty == appointmentId) return Results.BadRequest();
            return Results.Created($"/appointment/{appointmentId}", new { id = appointmentId });
        });

        // Delete a specific appointment by ID
        app.MapPost("/delete-appointment/{id:guid}", async (Guid id, ISender mediatr) =>
        {
            try
            {
                await mediatr.Send(new DeleteAppointmentCommand(id));
                return Results.Ok();
            }
            catch (AppointmentNotFoundException ex)
            {
                return Results.NotFound(new { message = ex.Message });
            }
        });

        
    }
}
