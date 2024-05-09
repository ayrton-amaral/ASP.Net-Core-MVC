using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PropertyRentalManagement.Models;

public partial class Province
{
    public string ProvinceId { get; set; } = null!;

    [Display(Name = "Province")]
    public string ProvinceName { get; set; } = null!;

    public virtual ICollection<City> Cities { get; set; } = new List<City>();
}
