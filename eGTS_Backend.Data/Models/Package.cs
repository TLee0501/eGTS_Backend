using System;
using System.Collections.Generic;

namespace eGTS_Backend.Data.Models;

public partial class Package
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public bool HasPt { get; set; }

    public bool HasNe { get; set; }

    public short NumerOfMonth { get; set; }

    public double Price { get; set; }

    public bool Status { get; set; }

    public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();
}
