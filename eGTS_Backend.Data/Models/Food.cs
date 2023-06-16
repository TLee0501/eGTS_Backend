using System;
using System.Collections.Generic;

namespace eGTS_Backend.Data.Models;

public partial class Food
{
    public Guid Id { get; set; }

    public Guid FoodScheduleId { get; set; }

    public string Name { get; set; } = null!;

    public double Weight { get; set; }

    public double Calories { get; set; }

    public virtual FoodSchedule FoodSchedule { get; set; } = null!;
}
