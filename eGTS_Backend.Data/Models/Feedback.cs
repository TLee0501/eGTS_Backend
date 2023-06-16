using System;
using System.Collections.Generic;

namespace eGTS_Backend.Data.Models;

public partial class Feedback
{
    public Guid Id { get; set; }

    public Guid ContractId { get; set; }

    public short Rate { get; set; }

    public string? Feedback1 { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual Contract Contract { get; set; } = null!;
}
