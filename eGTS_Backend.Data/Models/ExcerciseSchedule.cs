﻿using System;
using System.Collections.Generic;

namespace eGTS_Backend.Data.Models;

public partial class ExcerciseSchedule
{
    public ExcerciseSchedule(Guid id, Guid gymerId, Guid ptid, Guid packageGymerId, DateTime from, DateTime to, bool isDelete)
    {
        Id = id;
        GymerId = gymerId;
        Ptid = ptid;
        PackageGymerId = packageGymerId;
        From = from;
        To = to;
        IsDelete = isDelete;
    }

    public Guid Id { get; set; }

    public Guid GymerId { get; set; }

    public Guid Ptid { get; set; }

    public Guid PackageGymerId { get; set; }

    public DateTime From { get; set; }

    public DateTime To { get; set; }

    public bool IsDelete { get; set; }

    public virtual Account Gymer { get; set; } = null!;

    public virtual PackageGymer PackageGymer { get; set; } = null!;

    public virtual Account Pt { get; set; } = null!;

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();
}
