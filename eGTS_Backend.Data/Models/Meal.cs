using System;
using System.Collections.Generic;

namespace eGTS_Backend.Data.Models;

public partial class Meal
{
    public Guid Id { get; set; }

    public Guid NutritionScheduleId { get; set; }

    public DateTime Datetime { get; set; }

    public virtual ICollection<FoodAndSupplimentInMeal> FoodAndSupplimentInMeals { get; set; } = new List<FoodAndSupplimentInMeal>();

    public virtual NutritionSchedule NutritionSchedule { get; set; } = null!;
}
