using System;
using System.Collections.Generic;

namespace eGTS_Backend.Data.Models;

public partial class Workout
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public Guid VidieoId { get; set; }

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
}
