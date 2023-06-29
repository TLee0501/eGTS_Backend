using System;
using System.Collections.Generic;

namespace eGTS_Backend.Data.Models;

public partial class Message
{
    public Guid Id { get; set; }

    public Guid SenderId { get; set; }

    public Guid RecieverId { get; set; }

    public string Message1 { get; set; } = null!;

    public virtual Account Reciever { get; set; } = null!;

    public virtual Account Sender { get; set; } = null!;
}
