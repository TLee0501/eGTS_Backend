using System;
using System.Collections.Generic;

namespace eGTS_Backend.Data.Models;

public partial class ExcerciseType
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public Guid Ptid { get; set; }

    public ExcerciseType(Guid id, string name, Guid ptid)
    {
        Id = id;
        Name = name;
        Ptid = ptid;
    }

    public virtual ICollection<ExerciseInExerciseType> ExerciseInExerciseTypes { get; set; } = new List<ExerciseInExerciseType>();

    public virtual Account Pt { get; set; } = null!;
}
