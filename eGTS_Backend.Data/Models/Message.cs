using System;
using System.Collections.Generic;

namespace eGTS_Backend.Data.Models;

public partial class Message
{
    public Guid Id { get; set; }

    public Guid SenderId { get; set; }

    public Guid ReciverId { get; set; }

    public string Message1 { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public virtual Account Reciver { get; set; } = null!;

    public virtual Account Sender { get; set; } = null!;
}
