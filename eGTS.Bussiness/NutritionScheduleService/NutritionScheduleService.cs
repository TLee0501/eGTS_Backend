using eGTS_Backend.Data.Models;
using eGTS_Backend.Data.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGTS.Bussiness.NutritionScheduleService
{
    public class NutritionScheduleService : INutritionScheduleService
    {
        private readonly EGtsContext _context;
        public NutritionScheduleService(EGtsContext context)
        {
            _context = context;
        }
        public async Task<List<MealViewModel>> GetNutritionScheduleByGymerIDAndDate(Guid GymerId, DateTime date)
        {
            //Tim GymerPackageID
            var package = await _context.PackageGymers.SingleOrDefaultAsync(a => a.GymerId == GymerId && a.Status != "Done");
            if (package == null) return null;
            var GymerPackageId = package.Id;

            //Tim ScheduleID
            var schedule = await _context.NutritionSchedules.SingleOrDefaultAsync(a => a.PackageGymerId == GymerPackageId);
            if (schedule == null) return null;
            var ScheduleID = schedule.Id;

            //Tim Meal
            var meals = await _context.Meals.Where(a => a.NutritionScheduleId == ScheduleID).ToListAsync();
            if (meals == null) return null;

            //Search Meal
            var result = new List<MealViewModel>();
            foreach (var item in meals)
            {
                if (item.Datetime.Date == date.Date)
                {
                    var tmp = new MealViewModel();
                    tmp.Id = item.Id;
                    tmp.NutritionScheduleId = ScheduleID;
                    tmp.Datetime = item.Datetime.Date;
                    tmp.MealTime = item.MealTime;
                    tmp.FoodAndSuppliment = GetFoodInMeals(item.Id);
                    result.Add(tmp);
                }
            }
            return result;
        }

        public async Task<List<MealViewModel>> GetNutritionScheduleByGymerID(Guid GymerId)
        {
            //Tim GymerPackageID
            var package = await _context.PackageGymers.SingleOrDefaultAsync(a => a.GymerId == GymerId && a.Status == "Active");
            if (package == null) return null;
            var GymerPackageId = package.Id;

            //Tim ScheduleID
            var schedule = await _context.NutritionSchedules.SingleOrDefaultAsync(a => a.PackageGymerId == GymerPackageId);
            if (schedule == null) return null;
            var ScheduleID = schedule.Id;

            //Tim Meal
            var meals = await _context.Meals.Where(a => a.NutritionScheduleId == ScheduleID).ToListAsync();
            if (meals == null) return null;

            //Search Meal
            var result = new List<MealViewModel>();
            foreach (var item in meals)
            {
                var tmp = new MealViewModel();
                tmp.Id = item.Id;
                tmp.NutritionScheduleId = ScheduleID;
                tmp.Datetime = item.Datetime.Date;
                tmp.MealTime = item.MealTime;
                tmp.FoodAndSuppliment = GetFoodInMeals(item.Id);
                result.Add(tmp);
            }
            return result;
        }

        private List<FoodAndSupplimentViewModel> GetFoodInMeals(Guid id)
        {
            var result = new List<FoodAndSupplimentViewModel>();

            var foodIDs = _context.FoodAndSupplimentInMeals.Where(a => a.MealId == id).ToList();
            if (foodIDs == null) return null;

            foreach (var item in foodIDs)
            {
                var food = _context.FoodAndSuppliments.SingleOrDefault(a => a.Id == item.FoodAndSupplimentId && a.IsDelete == false);
                if (food != null)
                {
                    var viewModel = new FoodAndSupplimentViewModel();
                    viewModel.Id = food.Id;
                    viewModel.Neid = food.Neid;
                    viewModel.Name = food.Name;
                    viewModel.Ammount = food.Ammount;
                    viewModel.UnitOfMesuament = food.UnitOfMesuament;
                    viewModel.Calories = food.Calories;
                    viewModel.CreateDate = food.CreateDate;
                    result.Add(viewModel);
                }
            }
            return result;
        }
    }
}
