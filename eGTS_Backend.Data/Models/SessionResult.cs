using System;
using System.Collections.Generic;

namespace eGTS_Backend.Data.Models;

public partial class SessionResult
{
    public Guid Id { get; set; }

    public Guid SessionId { get; set; }

    public int CaloConsump { get; set; }

    public string Note { get; set; } = null!;

    public bool IsDelete { get; set; }

    public virtual Session Session { get; set; } = null!;
}
