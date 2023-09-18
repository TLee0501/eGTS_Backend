using System;
using System.Collections.Generic;

namespace eGTS_Backend.Data.Models;

public partial class FoodAndSupplement
{
    public Guid Id { get; set; }

    public Guid Neid { get; set; }

    public string Name { get; set; } = null!;

    public short Ammount { get; set; }

    public string UnitOfMesuament { get; set; } = null!;

    public double Calories { get; set; }

    public DateTime CreateDate { get; set; }

    public bool IsDelete { get; set; }

    public virtual ICollection<FoodAndSupplementInMeal> FoodAndSupplementInMeals { get; set; } = new List<FoodAndSupplementInMeal>();

    public virtual Account Ne { get; set; } = null!;
}
