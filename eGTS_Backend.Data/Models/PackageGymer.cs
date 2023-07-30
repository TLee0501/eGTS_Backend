using System;
using System.Collections.Generic;

namespace eGTS_Backend.Data.Models;

public partial class PackageGymer
{
    public PackageGymer(Guid id, string? name, Guid gymerId, Guid? packageId, Guid? ptid, Guid? neid, short? numberOfSession, string status, bool isDelete)
    {
        Id = id;
        Name = name;
        GymerId = gymerId;
        PackageId = packageId;
        Ptid = ptid;
        Neid = neid;
        NumberOfSession = numberOfSession;
        Status = status;
        IsDelete = isDelete;
    }

    public Guid Id { get; set; }

    public string? Name { get; set; }

    public Guid GymerId { get; set; }

    public Guid? PackageId { get; set; }

    public Guid? Ptid { get; set; }

    public Guid? Neid { get; set; }

    public short? NumberOfSession { get; set; }

    public DateTime? From { get; set; }

    public DateTime? To { get; set; }

    public string Status { get; set; } = null!;

    public bool IsDelete { get; set; }

    public virtual ICollection<ExcerciseSchedule> ExcerciseSchedules { get; set; } = new List<ExcerciseSchedule>();

    public virtual ICollection<FeedBack> FeedBacks { get; set; } = new List<FeedBack>();

    public virtual Account Gymer { get; set; } = null!;

    public virtual Account? Ne { get; set; }

    public virtual ICollection<NutritionSchedule> NutritionSchedules { get; set; } = new List<NutritionSchedule>();

    public virtual Package? Package { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Account? Pt { get; set; }

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();

    public virtual ICollection<Suspend> Suspends { get; set; } = new List<Suspend>();
}
