using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PropertyRentalManagement.Models;

public partial class Appointment
{
    public int AppointmentId { get; set; }

    [Display(Name = "Appointment Date")]
    public DateOnly AppointmentDate { get; set; }

    [Display(Name = "Appointment Time")]
    public TimeOnly AppointmentTime { get; set; }

    public int TenantId { get; set; }

    public int ManagerId { get; set; }

    public virtual User Manager { get; set; } = null!;

    public virtual User Tenant { get; set; } = null!;
}
