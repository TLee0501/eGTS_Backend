using System;
using System.Collections.Generic;

namespace eGTS_Backend.Data.Models;

public partial class Request
{
    public Guid Id { get; set; }

    public Guid GymerId { get; set; }

    public Guid ReceiverId { get; set; }

    public Guid PackageGymerId { get; set; }

    public bool IsPt { get; set; }

    public bool? IsAccepted { get; set; }

    public bool IsDelete { get; set; }

    public virtual Account Gymer { get; set; } = null!;

    public virtual PackageGymer PackageGymer { get; set; } = null!;

    public virtual Account Receiver { get; set; } = null!;
}
