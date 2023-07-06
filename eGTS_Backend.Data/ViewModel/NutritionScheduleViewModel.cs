using eGTS_Backend.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGTS_Backend.Data.ViewModel
{
    public class NutritionScheduleViewModel
    {
        public Guid Id { get; set; }
        public MealViewModel? Meal { get; set; }
    }
}
