﻿using System;
using System.Collections.Generic;

namespace eGTS_Backend.Data.Models;

public partial class Excercise
{
    public Excercise(Guid id, Guid ptid, string name, string? description, string? video, DateTime createDate, bool isDelete)
    {
        Id = id;
        Ptid = ptid;
        Name = name;
        Description = description;
        Video = video;
        CreateDate = createDate;
        IsDelete = isDelete;
    }

    public Guid Id { get; set; }

    public Guid Ptid { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? Video { get; set; }

    public DateTime CreateDate { get; set; }

    public bool IsDelete { get; set; }

    public virtual ICollection<ExerciseInExerciseType> ExerciseInExerciseTypes { get; set; } = new List<ExerciseInExerciseType>();

    public virtual ICollection<ExserciseInSession> ExserciseInSessions { get; set; } = new List<ExserciseInSession>();

    public virtual Account Pt { get; set; } = null!;
}
