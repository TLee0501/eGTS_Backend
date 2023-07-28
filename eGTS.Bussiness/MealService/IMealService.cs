using eGTS_Backend.Data.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGTS.Bussiness.MealService
{
    public interface IMealService
    {
        Task<bool> CreateMeal(MealCreateViewModel request);
        Task<bool> CheckvalidDate(MealCreateViewModel request);
    }
}
