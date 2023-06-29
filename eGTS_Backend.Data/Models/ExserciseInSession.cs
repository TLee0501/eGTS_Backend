using System;
using System.Collections.Generic;

namespace eGTS_Backend.Data.Models;

public partial class ExserciseInSession
{
    public Guid Id { get; set; }

    public Guid SessionId { get; set; }

    public Guid ExerciseId { get; set; }

    public virtual Excercise Exercise { get; set; } = null!;

    public virtual Session Session { get; set; } = null!;
}
