using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PropertyRentalManagement.Models;

public partial class City
{
    public int CityId { get; set; }

    [Display(Name = "City")]
    public string CityName { get; set; } = null!;

    [Display(Name = "Province")]
    public string ProvinceId { get; set; } = null!;

    public virtual ICollection<Building> Buildings { get; set; } = new List<Building>();

    public virtual Province Province { get; set; } = null!;
}
