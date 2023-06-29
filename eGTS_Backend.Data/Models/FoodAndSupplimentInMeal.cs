using System;
using System.Collections.Generic;

namespace eGTS_Backend.Data.Models;

public partial class FoodAndSupplimentInMeal
{
    public Guid Id { get; set; }

    public Guid FoodAndSupplimentId { get; set; }

    public Guid MealId { get; set; }

    public virtual FoodAndSuppliment FoodAndSuppliment { get; set; } = null!;

    public virtual Meal Meal { get; set; } = null!;
}
