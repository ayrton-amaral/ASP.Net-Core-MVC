using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PropertyRentalManagement.Models;

public partial class Message
{
    public int MessageId { get; set; }

    [Display(Name = "Sender")]
    public int SenderId { get; set; }

    [Display(Name = "Receiver")]
    public int ReceiverId { get; set; }

    [Display(Name = "Message Content")]
    public string MessageContent { get; set; } = null!;

    public virtual User? Receiver { get; set; }

    public virtual User? Sender { get; set; }
}
