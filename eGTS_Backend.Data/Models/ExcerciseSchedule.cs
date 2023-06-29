using System;
using System.Collections.Generic;

namespace eGTS_Backend.Data.Models;

public partial class ExcerciseSchedule
{
    public Guid Id { get; set; }

    public Guid GymerId { get; set; }

    public Guid Ptid { get; set; }

    public Guid PackageGymerId { get; set; }

    public virtual Account Gymer { get; set; } = null!;

    public virtual PackageGymer PackageGymer { get; set; } = null!;

    public virtual Account Pt { get; set; } = null!;

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();
}
