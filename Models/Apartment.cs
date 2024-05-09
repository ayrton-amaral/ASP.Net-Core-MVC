using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PropertyRentalManagement.Models;

public partial class Apartment
{
    [Display(Name = "Apartment")]
    public int ApartmentId { get; set; }

    [Display(Name = "Apartment Number")]
    public int ApartmentNumber { get; set; }

    [Display(Name = "Apartment Floor")]
    public int ApartmentFloor { get; set; }

    [Display(Name = "Apartment Size")]
    public string ApartmentType { get; set; } = null!;

    public int AvailabilityId { get; set; }

    [Display(Name = "Rental Price")]
    public double RentPrice { get; set; }

    public int BuildingId { get; set; }

    public int OwnerId { get; set; }

    public int ManagerId { get; set; }

    public int? TenantId { get; set; }

    public virtual Availability Availability { get; set; } = null!;

    public virtual Building Building { get; set; } = null!;

    public virtual User Manager { get; set; } = null!;

    public virtual User Owner { get; set; } = null!;

    public virtual User Tenant { get; set; } = null!;
}
