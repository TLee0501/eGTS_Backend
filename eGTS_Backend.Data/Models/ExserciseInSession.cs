using System;
using System.Collections.Generic;

namespace eGTS_Backend.Data.Models;

public partial class ExserciseInSession
{
    public Guid Id { get; set; }

    public Guid SessionId { get; set; }

    public Guid ExerciseId { get; set; }

    public ExserciseInSession(Guid id, Guid sessionId, Guid exerciseId)
    {
        Id = id;
        SessionId = sessionId;
        ExerciseId = exerciseId;
    }

    public virtual Excercise Exercise { get; set; } = null!;

    public virtual Session Session { get; set; } = null!;
}
