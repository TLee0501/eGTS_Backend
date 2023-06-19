using System;
using System.Collections.Generic;

namespace eGTS_Backend.Data.Models;

public partial class Package
{
    public Guid Id { get; set; }

    public Guid GymerId { get; set; }

    public Guid? Ptid { get; set; }

    public Guid? Neid { get; set; }

    public double Price { get; set; }

    public DateTime OrderDate { get; set; }

    public virtual Account Gymer { get; set; } = null!;

    public virtual Account? Ne { get; set; }

    public virtual Account? Pt { get; set; }
}
