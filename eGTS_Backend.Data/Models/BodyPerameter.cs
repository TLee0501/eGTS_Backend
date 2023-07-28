using System;
using System.Collections.Generic;

namespace eGTS_Backend.Data.Models;

public partial class BodyPerameter
{
    public Guid Id { get; set; }

    public Guid GymerId { get; set; }

    public string? Goal { get; set; }

    public double? Weight { get; set; }

    public double? Height { get; set; }

    public double? Bmi { get; set; }

    public short? Bone { get; set; }

    public short? Fat { get; set; }

    public short? Muscle { get; set; }

    public DateTime CreateDate { get; set; }

    public bool IsDelete { get; set; }

    public BodyPerameter(Guid id, Guid gymerId, string? goal, double? weight, double? height, double? bmi, short? bone, short? fat, short? muscle, DateTime createDate, bool isDelete, Account gymer)
    {
        Id = id;
        GymerId = gymerId;
        Goal = goal;
        Weight = weight;
        Height = height;
        Bmi = bmi;
        Bone = bone;
        Fat = fat;
        Muscle = muscle;
        CreateDate = createDate;
        IsDelete = isDelete;
        Gymer = gymer;
    }

    public virtual Account Gymer { get; set; } = null!;
}
