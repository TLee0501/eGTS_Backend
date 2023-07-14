using System;
using System.Collections.Generic;

namespace eGTS_Backend.Data.Models;

public partial class FoodAndSupplimentType
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public Guid Neid { get; set; }

    public bool IsDelete { get; set; }

    public virtual ICollection<FoodAndSupplimentInFoodAndSupplimentType> FoodAndSupplimentInFoodAndSupplimentTypes { get; set; } = new List<FoodAndSupplimentInFoodAndSupplimentType>();
}
