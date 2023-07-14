using System;
using System.Collections.Generic;

namespace eGTS_Backend.Data.Models;

public partial class FeedBack
{
    public Guid Id { get; set; }

    public Guid PtidorNeid { get; set; }

    public Guid PackageGymerId { get; set; }

    public short Rate { get; set; }

    public string Feedback1 { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public bool IsDelete { get; set; }

    public virtual PackageGymer PackageGymer { get; set; } = null!;

    public virtual Account PtidorNe { get; set; } = null!;
}
