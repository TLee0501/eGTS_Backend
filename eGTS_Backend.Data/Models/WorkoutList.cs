using System;
using System.Collections.Generic;

namespace eGTS_Backend.Data.Models;

public partial class WorkoutList
{
    public Guid ScheduleId { get; set; }

    public Guid WorkoutId { get; set; }

    public virtual Schedule Schedule { get; set; } = null!;

    public virtual Workout Workout { get; set; } = null!;
}
