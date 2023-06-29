﻿using System;
using System.Collections.Generic;

namespace eGTS_Backend.Data.Models;

public partial class PackageGymer
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public Guid GymerId { get; set; }

    public Guid? PackageId { get; set; }

    public Guid? Ptid { get; set; }

    public Guid? Neid { get; set; }

    public short NumberOfSession { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<ExcerciseSchedule> ExcerciseSchedules { get; set; } = new List<ExcerciseSchedule>();

    public virtual ICollection<FeedBack> FeedBacks { get; set; } = new List<FeedBack>();

    public virtual Account Gymer { get; set; } = null!;

    public virtual Account? Ne { get; set; }

    public virtual ICollection<NutritionSchedule> NutritionSchedules { get; set; } = new List<NutritionSchedule>();

    public virtual Package? Package { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Account? Pt { get; set; }

    public virtual ICollection<Suspend> Suspends { get; set; } = new List<Suspend>();
}
