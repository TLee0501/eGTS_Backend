using System;
using System.Collections.Generic;

namespace eGTS_Backend.Data.Models;

public partial class Schedule
{
    public Guid Id { get; set; }

    public DateTime DateTime { get; set; }

    public Guid ContractId { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual Contract Contract { get; set; } = null!;
}
