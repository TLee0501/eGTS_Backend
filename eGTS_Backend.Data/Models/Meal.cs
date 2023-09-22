using System;
using System.Collections.Generic;

namespace eGTS_Backend.Data.Models;

public partial class Meal
{
    public Guid Id { get; set; }

    public Guid NutritionScheduleId { get; set; }

    public int MealTime { get; set; }

    public DateTime Datetime { get; set; }

    public bool IsDelete { get; set; }

    public virtual ICollection<FoodAndSupplementInMeal> FoodAndSupplementInMeals { get; set; } = new List<FoodAndSupplementInMeal>();

    public virtual NutritionSchedule NutritionSchedule { get; set; } = null!;
}
