using System;
using System.Collections.Generic;

namespace eGTS_Backend.Data.Models;

public partial class WorkoutResult
{
    public Guid Id { get; set; }

    public Guid ContractId { get; set; }

    public DateTime CreateDate { get; set; }

    public string Description { get; set; } = null!;

    public virtual Contract Contract { get; set; } = null!;
}
