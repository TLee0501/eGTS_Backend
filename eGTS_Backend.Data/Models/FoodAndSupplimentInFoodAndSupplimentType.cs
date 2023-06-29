using System;
using System.Collections.Generic;

namespace eGTS_Backend.Data.Models;

public partial class FoodAndSupplimentInFoodAndSupplimentType
{
    public Guid Id { get; set; }

    public Guid FoodAndSupplimentTypeId { get; set; }

    public Guid FoodAndSupplimentId { get; set; }

    public virtual FoodAndSuppliment FoodAndSuppliment { get; set; } = null!;

    public virtual FoodAndSupplimentType FoodAndSupplimentType { get; set; } = null!;
}
