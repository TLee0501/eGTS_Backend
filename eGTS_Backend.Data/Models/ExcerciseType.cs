using System;
using System.Collections.Generic;

namespace eGTS_Backend.Data.Models;

public partial class ExcerciseType
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public Guid Ptid { get; set; }

    public bool IsDelete { get; set; }

    public virtual ICollection<ExerciseInExerciseType> ExerciseInExerciseTypes { get; set; } = new List<ExerciseInExerciseType>();

    public virtual Account Pt { get; set; } = null!;
}
