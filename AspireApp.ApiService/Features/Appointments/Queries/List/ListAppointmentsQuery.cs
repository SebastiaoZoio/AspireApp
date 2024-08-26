using MediatR;
using AspireApp.ApiService.Domain;

namespace AspireApp.ApiService.Features.Appointments.Queries.List;

public class ListAppointmentsQuery : IRequest<List<Appointment>>
{
}