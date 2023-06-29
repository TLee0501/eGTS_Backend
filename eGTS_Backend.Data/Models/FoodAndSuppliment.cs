using System;
using System.Collections.Generic;

namespace eGTS_Backend.Data.Models;

public partial class FoodAndSuppliment
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public short Ammount { get; set; }

    public double Calories { get; set; }

    public virtual ICollection<FoodAndSupplimentInFoodAndSupplimentType> FoodAndSupplimentInFoodAndSupplimentTypes { get; set; } = new List<FoodAndSupplimentInFoodAndSupplimentType>();

    public virtual ICollection<FoodAndSupplimentInMeal> FoodAndSupplimentInMeals { get; set; } = new List<FoodAndSupplimentInMeal>();
}
