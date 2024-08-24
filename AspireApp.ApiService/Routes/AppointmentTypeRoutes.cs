using AspireApp.ApiService.Domain;
using AspireApp.ApiService.Persistence.Interfaces;

public static class AppointmentTypeRoutes
{
    public static void MapAppointmentTypeEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/appointment-types", async (IAppointmentTypeRepository repo) =>
        {
            var appointmentTypes = await repo.GetAllAsync();
            return Results.Ok(appointmentTypes);
        });

        app.MapGet("/appointment-types/{id:int}", async (int id, IAppointmentTypeRepository repo) =>
        {
            var appointmentType = await repo.GetByIdAsync(id);
            if (appointmentType == null) return Results.NotFound();
            return Results.Ok(appointmentType);
        });

        app.MapPost("/appointment-types", async (AppointmentType newAppointmentType, IAppointmentTypeRepository repo) =>
        {
            await repo.AddAsync(newAppointmentType);
            return Results.Created($"/appointment-types/{newAppointmentType.Id}", newAppointmentType);
        });

        app.MapPut("/appointment-types/{id:int}", async (int id, AppointmentType updatedAppointmentType, IAppointmentTypeRepository repo) =>
        {
            var existingType = await repo.GetByIdAsync(id);
            if (existingType == null) return Results.NotFound();

            existingType.Name = updatedAppointmentType.Name;
            await repo.UpdateAsync(existingType);
            return Results.NoContent();
        });

        app.MapDelete("/appointment-types/{id:int}", async (int id, IAppointmentTypeRepository repo) =>
        {
            await repo.DeleteAsync(id);
            return Results.NoContent();
        });
    }
}
