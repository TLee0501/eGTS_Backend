﻿using System;
using System.Collections.Generic;

namespace eGTS_Backend.Data.Models;

public partial class Session
{
    public Guid Id { get; set; }

    public Guid ScheduleId { get; set; }

    public DateTime DateAndTime { get; set; }

    public virtual ICollection<ExserciseInSession> ExserciseInSessions { get; set; } = new List<ExserciseInSession>();

    public virtual ExcerciseSchedule Schedule { get; set; } = null!;

    public virtual ICollection<SessionResult> SessionResults { get; set; } = new List<SessionResult>();
}