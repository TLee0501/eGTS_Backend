using System;
using System.Collections.Generic;

namespace eGTS_Backend.Data.Models;

public partial class Schedule
{
    public Guid Id { get; set; }

    public TimeSpan Time { get; set; }

    public DateTime Date { get; set; }

    public Guid ContractId { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual Contract Contract { get; set; } = null!;

    public virtual ICollection<Workout> Workouts { get; set; } = new List<Workout>();
}
