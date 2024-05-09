using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PropertyRentalManagement.Models;

public partial class User
{
    public int UserId { get; set; }

    [Display(Name = "Treatment Pronoun")]
    public string TreatmentPronoun { get; set; } = null!;

    [Display(Name = "First Name")]
    public string FirstName { get; set; } = null!;

    [Display(Name = "Last Name")]
    public string LastName { get; set; } = null!;

    [Display(Name = "E-mail")]
    public string Email { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Role { get; set; } = null!;

    public virtual ICollection<Apartment> ApartmentManagers { get; set; } = new List<Apartment>();

    public virtual ICollection<Apartment> ApartmentOwners { get; set; } = new List<Apartment>();

    public virtual ICollection<Apartment> ApartmentTenants { get; set; } = new List<Apartment>();

    public virtual ICollection<Appointment> AppointmentManagers { get; set; } = new List<Appointment>();

    public virtual ICollection<Appointment> AppointmentTenants { get; set; } = new List<Appointment>();

    public virtual ICollection<Building> Buildings { get; set; } = new List<Building>();

    public virtual ICollection<Message> MessageReceivers { get; set; } = new List<Message>();

    public virtual ICollection<Message> MessageSenders { get; set; } = new List<Message>();
}
