using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PropertyRentalManagement.Models;

public partial class Building
{
    [Display(Name = "Building")]
    public int BuildingId { get; set; }

    [Display(Name = "Building Name")]
    public string BuildingName { get; set; } = null!;

    [Display(Name = "Address")]
    public string Address { get; set; } = null!;

    [Display(Name = "Postal Code")]
    public string PostalCode { get; set; } = null!;

    [Display(Name = "City")]
    public int CityId { get; set; }

    [Display(Name = "Manager")]
    public int ManagerId { get; set; }

    public virtual ICollection<Apartment> Apartments { get; set; } = new List<Apartment>();

    public virtual City City { get; set; } = null!;

    public virtual User Manager { get; set; } = null!;
}
