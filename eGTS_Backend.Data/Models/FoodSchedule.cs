using System;
using System.Collections.Generic;

namespace eGTS_Backend.Data.Models;

public partial class FoodSchedule
{
    public Guid Id { get; set; }

    public Guid Neid { get; set; }

    public DateTime CreateDate { get; set; }

    public Guid ContractId { get; set; }

    public DateTime Date { get; set; }

    public TimeSpan Time { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public virtual Contract Contract { get; set; } = null!;

    public virtual ICollection<Food> Foods { get; set; } = new List<Food>();

    public virtual Account Ne { get; set; } = null!;
}
