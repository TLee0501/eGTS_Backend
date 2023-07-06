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
        public DateTime DateTime { get; set; }
        public List<FoodAndSupplimentViewModel> FoodAndSuppliment { get; set; }
    }
}
