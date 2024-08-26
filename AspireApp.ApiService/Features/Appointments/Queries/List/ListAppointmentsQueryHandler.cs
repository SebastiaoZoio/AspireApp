using AspireApp.ApiService.Persistence.Interfaces;
using AspireApp.ApiService.Domain;
using MediatR;

namespace AspireApp.ApiService.Features.Appointments.Queries.List;

public class ListAppointmentsQueryHandler : IRequestHandler<ListAppointmentsQuery, List<Appointment>>
{
    private readonly IAppointmentRepository _repository;

    public ListAppointmentsQueryHandler(IAppointmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Appointment>> Handle(ListAppointmentsQuery request, CancellationToken cancellationToken)
    {
        return (List<Appointment>)await _repository.GetAllAsync();
    }
}
