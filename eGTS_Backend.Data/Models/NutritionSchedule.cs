using System;
using System.Collections.Generic;

namespace eGTS_Backend.Data.Models;

public partial class NutritionSchedule
{
    public Guid Id { get; set; }

    public Guid GymerId { get; set; }

    public Guid Neid { get; set; }

    public Guid? PackageGymerId { get; set; }

    public virtual ICollection<Meal> Meals { get; set; } = new List<Meal>();

    public virtual PackageGymer? PackageGymer { get; set; }
}
