﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspireApp.ApiService.Domain
{
    public class Appointment
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [ForeignKey("Collaborator")]
        public Guid CollaboratorId { get; set; } = default!;

        [ForeignKey("AppointmentType")]
        public int AppointmentTypeId { get; set; }

        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }

        public Collaborator Collaborator;
        public AppointmentType AppointmentType;

        public Appointment() { }

        public Appointment(Guid collaboratorId, int appointmentTypeId, DateTime beginDate, DateTime endDate)
        {
            Id = Guid.NewGuid();
            CollaboratorId = collaboratorId;
            AppointmentTypeId = appointmentTypeId;
            BeginDate = beginDate;
            EndDate = endDate;
        }
    }    
}
