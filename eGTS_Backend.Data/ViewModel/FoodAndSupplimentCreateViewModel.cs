﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGTS_Backend.Data.ViewModel
{
    public class FoodAndSupplimentCreateViewModel
    {
        public Guid Neid { get; set; }

        public string Name { get; set; } = null!;

        public short Ammount { get; set; }
        public string UnitOfMesuament { get; set; } = null!;

        public double Calories { get; set; }
    }
}
