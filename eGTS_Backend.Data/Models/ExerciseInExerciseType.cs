using System;
using System.Collections.Generic;

namespace eGTS_Backend.Data.Models;

public partial class ExerciseInExerciseType
{
    public ExerciseInExerciseType(Guid id, Guid exerciseTypeId, Guid exerciseId)
    {
        Id = id;
        ExerciseTypeId = exerciseTypeId;
        ExerciseId = exerciseId;
    }

    public Guid Id { get; set; }

    public Guid ExerciseTypeId { get; set; }

    public Guid ExerciseId { get; set; }

    public virtual Excercise Exercise { get; set; } = null!;

    public virtual ExcerciseType ExerciseType { get; set; } = null!;
}
