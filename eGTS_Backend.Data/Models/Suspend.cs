using System;
using System.Collections.Generic;

namespace eGTS_Backend.Data.Models;

public partial class Suspend
{
    public Guid Id { get; set; }

    public Guid PackageGymerId { get; set; }

    public DateTime From { get; set; }

    public DateTime To { get; set; }

    public string Reson { get; set; } = null!;

    public bool IsDelete { get; set; }

    public virtual PackageGymer PackageGymer { get; set; } = null!;
}
