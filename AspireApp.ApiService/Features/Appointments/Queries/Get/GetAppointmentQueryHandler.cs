using AspireApp.ApiService.Persistence.Interfaces;
using AspireApp.ApiService.Domain;
using MediatR;

namespace AspireApp.ApiService.Features.Appointments.Queries.Get;

public class GetAppointmentQueryHandler : IRequestHandler<GetAppointmentQuery, Appointment>
{
    private readonly IAppointmentRepository _repository;

    public GetAppointmentQueryHandler(IAppointmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<Appointment> Handle(GetAppointmentQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(request.Id);
    }
}
