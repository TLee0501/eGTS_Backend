using System;
using System.Collections.Generic;

namespace eGTS_Backend.Data.Models;

public partial class Qualification
{
    public Qualification(Guid expertId, string? certificate, short? experience, bool isCetifide, bool isDelete)
    {
        ExpertId = expertId;
        Certificate = certificate;
        Experience = experience;
        IsCetifide = isCetifide;
        IsDelete = isDelete;
    }

    public Guid ExpertId { get; set; }

    public string? Certificate { get; set; }

    public short? Experience { get; set; }

    public bool IsCetifide { get; set; }

    public bool IsDelete { get; set; }

    public virtual Account Expert { get; set; } = null!;
}
