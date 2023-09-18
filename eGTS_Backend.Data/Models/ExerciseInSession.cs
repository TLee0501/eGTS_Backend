using System;
using System.Collections.Generic;

namespace eGTS_Backend.Data.Models;

public partial class ExerciseInSession
{
    public Guid Id { get; set; }

    public Guid SessionId { get; set; }

    public Guid ExerciseId { get; set; }

    public virtual Exercise Exercise { get; set; } = null!;

    public virtual Session Session { get; set; } = null!;
}
