using System;
using System.Collections.Generic;

namespace eGTS_Backend.Data.Models;

public partial class Payment
{
    public Guid Id { get; set; }

    public Guid ContractId { get; set; }

    public DateTime PaymentDate { get; set; }

    public double Amount { get; set; }

    public virtual Contract Contract { get; set; } = null!;
}
