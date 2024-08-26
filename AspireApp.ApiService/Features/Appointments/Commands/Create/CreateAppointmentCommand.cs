using MediatR;

namespace AspireApp.ApiService.Features.Appointments.Commands.Create;

public class CreateAppointmentCommand : IRequest<Guid>
{
    public Guid CollaboratorId { get; set; }
    public int AppointmentTypeId { get; set; }
    public DateTime BeginDate { get; set; }
    public DateTime EndDate { get; set; }

    public CreateAppointmentCommand(Guid collaboratorId, int appointmentTypeId, DateTime beginDate, DateTime endDate)
    {
        CollaboratorId = collaboratorId;
        AppointmentTypeId = appointmentTypeId;
        BeginDate = beginDate;
        EndDate = endDate;
    }
}