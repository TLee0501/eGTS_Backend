using System;
using System.Collections.Generic;

namespace eGTS_Backend.Data.Models;

public partial class Qualification
{
    public Guid ExpertId { get; set; }

    public string? Certificate { get; set; }

    public short? Experience { get; set; }

    public bool IsCetifide { get; set; }

    public virtual Account Expert { get; set; } = null!;
}
