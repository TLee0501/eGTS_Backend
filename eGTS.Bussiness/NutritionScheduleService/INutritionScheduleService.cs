using eGTS_Backend.Data.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGTS.Bussiness.NutritionScheduleService
{
    public interface INutritionScheduleService
    {
        //Task<List<FoodAndSupplimentViewModel>> GetFoodAndSupplimentsBYNE(Guid id);
        //Task<FoodAndSupplimentViewModel> GetFoodAndSuppliment(Guid id);
        //Task<bool> UpdateFoodAndSuppliment(FoodAndSupplimentUpdateViewModel request);
        Task<List<MealViewModel>> GetMealByGymerIDAndDateAndMealTime(Guid GymerId, DateTime date, int MealTime);
        //Task<bool> DeleteFoodAndSuppliment(Guid id);
        Task<List<MealViewModel>> GetNutritionScheduleByGymerIDAndDate(Guid GymerId, DateTime date);
        Task<List<MealViewModel>> GetNutritionScheduleByGymerID(Guid GymerId);
        Task<bool> CreateNutritionSchedule(Guid PackageGymerID);
    }
}
