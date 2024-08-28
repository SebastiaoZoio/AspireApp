using AspireApp.Web.Services.Enums;
using AspireApp.Web.Services.Models;

namespace AspireApp.Web.Services.Results;

public class ListAppointmentTypesResult
{
    public bool Success { get; set; } = true;
    public IEnumerable<AppointmentType>? AppointmentTypes { get; set; }
    public string? ErrorMessage { get; set; }
    public ErrorType? ErrorType { get; set; }
}
