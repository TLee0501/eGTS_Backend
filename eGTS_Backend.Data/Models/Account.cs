﻿using System;
using System.Collections.Generic;

namespace eGTS_Backend.Data.Models;

public partial class Account
{
    public Guid Id { get; set; }

    public string PhoneNo { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Image { get; set; }

    public string Fullname { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public string Role { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public bool IsDelete { get; set; }

    public Account(Guid id, string phoneNo, string password, string image, string fullname, string gender, string role, DateTime createDate, bool isDelete)
    {
        Id = id;
        PhoneNo = phoneNo;
        Password = password;
        Image = image;
        Fullname = fullname;
        Gender = gender;
        Role = role;
        CreateDate = createDate;
        IsDelete = isDelete;
    }

    public virtual ICollection<BodyPerameter> BodyPerameters { get; set; } = new List<BodyPerameter>();

    public virtual ICollection<ExerciseSchedule> ExerciseScheduleGymers { get; set; } = new List<ExerciseSchedule>();

    public virtual ICollection<ExerciseSchedule> ExerciseSchedulePts { get; set; } = new List<ExerciseSchedule>();

    public virtual ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();

    public virtual ICollection<FeedBack> FeedBacks { get; set; } = new List<FeedBack>();

    public virtual ICollection<FoodAndSupplement> FoodAndSupplements { get; set; } = new List<FoodAndSupplement>();

    public virtual ICollection<PackageGymer> PackageGymerGymers { get; set; } = new List<PackageGymer>();

    public virtual ICollection<PackageGymer> PackageGymerNes { get; set; } = new List<PackageGymer>();

    public virtual ICollection<PackageGymer> PackageGymerPts { get; set; } = new List<PackageGymer>();

    public virtual Qualification? Qualification { get; set; }

    public virtual ICollection<Request> RequestGymers { get; set; } = new List<Request>();

    public virtual ICollection<Request> RequestReceivers { get; set; } = new List<Request>();
}
