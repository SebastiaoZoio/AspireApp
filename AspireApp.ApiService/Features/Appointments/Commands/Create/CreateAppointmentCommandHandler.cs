using AspireApp.ApiService.Persistence.Interfaces;
using AspireApp.ApiService.Domain;
using MediatR;

namespace AspireApp.ApiService.Features.Appointments.Commands.Create;

public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, Guid>
{
    private readonly IAppointmentRepository _repository;

    public CreateAppointmentCommandHandler(IAppointmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
    {
        var appointment = new Appointment(request.CollaboratorId, request.AppointmentTypeId, request.BeginDate, request.EndDate);

        await _repository.AddAsync(appointment);

        return appointment.Id;
    }
}
