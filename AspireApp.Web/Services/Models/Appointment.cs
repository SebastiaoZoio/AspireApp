namespace AspireApp.Web.Services.Models;

public class Appointment
{
    public Guid Id { get; set; }

    public Guid CollaboratorId { get; set; }

    public int AppointmentTypeId { get; set; }

    public DateTime BeginDate { get; set; }
    public DateTime EndDate { get; set; }

    public Collaborator? Collaborator { get; set; }
    public AppointmentType? AppointmentType { get; set; }
}