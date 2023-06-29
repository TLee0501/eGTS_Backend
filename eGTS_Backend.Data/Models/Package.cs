using System;
using System.Collections.Generic;

namespace eGTS_Backend.Data.Models;

public partial class Package
{
    public Guid Id { get; set; }

    public bool HasPt { get; set; }

    public bool HasNe { get; set; }

    public short NumberOfsession { get; set; }

    public double Price { get; set; }

    public virtual ICollection<PackageGymer> PackageGymers { get; set; } = new List<PackageGymer>();
}
