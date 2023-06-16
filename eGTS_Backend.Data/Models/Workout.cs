using System;
using System.Collections.Generic;

namespace eGTS_Backend.Data.Models;

public partial class Workout
{
    public Guid Id { get; set; }

    public Guid ScheduleId { get; set; }

    public string Name { get; set; } = null!;

    public TimeSpan Time { get; set; }

    public string Description { get; set; } = null!;

    public Guid VidieoId { get; set; }

    public virtual Schedule Schedule { get; set; } = null!;
}
