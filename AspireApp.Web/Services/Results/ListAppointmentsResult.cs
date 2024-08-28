using AspireApp.Web.Services.Enums;
using AspireApp.Web.Services.Models;

namespace AspireApp.Web.Services.Results;

public class ListAppointmentsResult
{
    public bool Success { get; set; } = true;
    public IEnumerable<Appointment>? Appointments { get; set; }
    public string? ErrorMessage { get; set; }
    public ErrorType? ErrorType { get; set; }
}
