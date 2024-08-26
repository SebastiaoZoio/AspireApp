namespace AspireApp.Web.Services.Requests;

public class NewAppointmentRequest
{
    public Guid CollaboratorId { get; set; }
    public int AppointmentTypeId { get; set; }
    public DateTime BeginDate { get; set; }
    public DateTime EndDate { get; set; }
}
