using eGTS_Backend.Data.ViewModel;

namespace eGTS.Bussiness.MealService
{
    public interface IMealService
    {
        Task<bool> CreateMeal(MealCreateViewModel request);
        Task<bool> CheckvalidDate(MealCreateViewModel request);
        Task<bool> UpdateMeal(MealCreateViewModel request);
        Task<int> UpdateMealFood(MealCreateViewModel request);
    }
}
