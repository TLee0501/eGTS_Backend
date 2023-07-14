using System;
using System.Collections.Generic;

namespace eGTS_Backend.Data.Models;

public partial class BodyPerameter
{
    public Guid Id { get; set; }

    public Guid GymerId { get; set; }

    public double? Weight { get; set; }

    public double? Height { get; set; }

    public double? Bmi { get; set; }

    public short? Bone { get; set; }

    public short? Fat { get; set; }

    public short? Muscle { get; set; }

    public DateTime CreateDate { get; set; }

    public bool IsDelete { get; set; }

    public virtual Account Gymer { get; set; } = null!;
}
