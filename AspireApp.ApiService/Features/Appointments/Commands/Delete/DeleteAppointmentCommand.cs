using MediatR;

namespace AspireApp.ApiService.Features.Appointments.Commands.Delete;

public class DeleteAppointmentCommand : IRequest<bool>
{
    public Guid Id { get; set; }

    public DeleteAppointmentCommand(Guid id)
    {
        Id = id;
    }
}
