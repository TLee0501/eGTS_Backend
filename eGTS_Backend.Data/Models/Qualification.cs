using System;
using System.Collections.Generic;

namespace eGTS_Backend.Data.Models;

public partial class Qualification
{
    public Guid ExpertId { get; set; }

    public string? Certificate { get; set; }

    public short? Experience { get; set; }

    public string? Description { get; set; }

    public bool IsCetifide { get; set; }

    public bool IsDelete { get; set; }

    public Qualification(Guid expertId, string? certificate, short? experience, string? description, bool isCetifide, bool isDelete)
    {
        ExpertId = expertId;
        Certificate = certificate;
        Experience = experience;
        Description = description;
        IsCetifide = isCetifide;
        IsDelete = isDelete;
    }

    public virtual Account Expert { get; set; } = null!;
}
