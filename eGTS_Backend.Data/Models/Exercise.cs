using System;
using System.Collections.Generic;

namespace eGTS_Backend.Data.Models;

public partial class Exercise
{
    public Guid Id { get; set; }

    public Guid Ptid { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? Video { get; set; }

    public DateTime CreateDate { get; set; }

    public int CalorieCumsumption { get; set; }

    public int RepTime { get; set; }

    public string UnitOfMeasurement { get; set; } = null!;

    public bool IsDelete { get; set; }

    public virtual ICollection<ExerciseInSession> ExserciseInSessions { get; set; } = new List<ExerciseInSession>();

    public virtual Account Pt { get; set; } = null!;
}
