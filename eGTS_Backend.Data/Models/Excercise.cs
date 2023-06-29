﻿using System;
using System.Collections.Generic;

namespace eGTS_Backend.Data.Models;

public partial class Excercise
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? Video { get; set; }

    public virtual ICollection<ExerciseInExerciseType> ExerciseInExerciseTypes { get; set; } = new List<ExerciseInExerciseType>();

    public virtual ICollection<ExserciseInSession> ExserciseInSessions { get; set; } = new List<ExserciseInSession>();
}