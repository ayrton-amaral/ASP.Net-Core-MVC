using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PropertyRentalManagement.Models;

public partial class Availability
{
    public int AvailabilityId { get; set; }

    [Display(Name = "Availability")]
    public string AvailabilityDescription { get; set; } = null!;

    public virtual ICollection<Apartment> Apartments { get; set; } = new List<Apartment>();
}
