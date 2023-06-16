using System;
using System.Collections.Generic;

namespace eGTS_Backend.Data.Models;

public partial class Payment
{
    public Guid Id { get; set; }

    public Guid GymerId { get; set; }

    public Guid CotractId { get; set; }

    public DateTime PaymentDate { get; set; }

    public double Amount { get; set; }

    public virtual Contract Cotract { get; set; } = null!;

    public virtual Account Gymer { get; set; } = null!;
}
