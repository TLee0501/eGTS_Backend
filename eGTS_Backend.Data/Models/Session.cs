using System;
using System.Collections.Generic;

namespace eGTS_Backend.Data.Models;

public partial class Session
{
    public Guid Id { get; set; }

    public Guid ScheduleId { get; set; }

    public DateTime From { get; set; }

    public DateTime To { get; set; }

    public bool IsDelete { get; set; }

    public virtual ICollection<ExerciseInSession> ExerciseInSessions { get; set; } = new List<ExerciseInSession>();

    public virtual ExerciseSchedule Schedule { get; set; } = null!;

    public virtual ICollection<SessionResult> SessionResults { get; set; } = new List<SessionResult>();
}
