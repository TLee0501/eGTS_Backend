using System;
using System.Collections.Generic;

namespace eGTS_Backend.Data.Models;

public partial class Request
{
    public Request(Guid id, Guid gymerId, Guid receiverId, Guid packageGymerId, bool isPt, bool? isAccepted)
    {
        Id = id;
        GymerId = gymerId;
        ReceiverId = receiverId;
        PackageGymerId = packageGymerId;
        IsPt = isPt;
        IsAccepted = isAccepted;
    }

    public Guid Id { get; set; }

    public Guid GymerId { get; set; }

    public Guid ReceiverId { get; set; }

    public Guid PackageGymerId { get; set; }

    public bool IsPt { get; set; }

    public bool? IsAccepted { get; set; }

    public virtual Account Gymer { get; set; } = null!;

    public virtual PackageGymer PackageGymer { get; set; } = null!;

    public virtual Account Receiver { get; set; } = null!;
}
