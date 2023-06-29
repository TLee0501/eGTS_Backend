using System;
using System.Collections.Generic;

namespace eGTS_Backend.Data.Models;

public partial class Request
{
    public Guid Id { get; set; }

    public Guid GymerId { get; set; }

    public Guid ReceiverId { get; set; }

    public bool IsAccepted { get; set; }

    public string? Reason { get; set; }

    public virtual Account Gymer { get; set; } = null!;

    public virtual Account Receiver { get; set; } = null!;
}
