using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGTS_Backend.Data.ViewModel
{
    public class MealViewModel
    {
        public Guid Id { get; set; }

        public Guid NutritionScheduleId { get; set; }

        public int MealTime { get; set; }

        public DateTime Datetime { get; set; }

        public bool IsDelete { get; set; }
        public List<FoodAndSupplimentViewModel> FoodAndSuppliment { get; set; }
    }
}
