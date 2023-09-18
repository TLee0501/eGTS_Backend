using System;
using System.Collections.Generic;

namespace eGTS_Backend.Data.Models;

public partial class FoodAndSupplementInMeal
{
    public Guid Id { get; set; }

    public Guid FoodAndSupplementId { get; set; }

    public Guid MealId { get; set; }

    public virtual FoodAndSupplement FoodAndSupplement { get; set; } = null!;

    public virtual Meal Meal { get; set; } = null!;
}
