using MediatR;
using AspireApp.ApiService.Domain;

namespace AspireApp.ApiService.Features.Appointments.Queries.Get;

public class GetAppointmentQuery : IRequest<Appointment>
{
    public Guid Id { get; set; }

    public GetAppointmentQuery(Guid id)
    {
        Id = id;
    }
}