using eGTS_Backend.Data.Models;
using eGTS_Backend.Data.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGTS.Bussiness.MealService
{
    public class MealService : IMealService
    {
        private readonly EGtsContext _context;

        public MealService(EGtsContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateMeal(MealCreateViewModel request)
        {
            var dates = new List<DateTime>();

            try
            {
                for (var dt = request.FromDatetime; dt <= request.ToDatetime; dt = dt.AddDays(1))
                {
                    dates.Add(dt);
                }
                foreach (var item in dates) //Lay tung ngay
                {
                    if (request.MonAnSang != null)  //Neu co mon thi tao bua an
                    {
                        var meal = new Meal();
                        var mealId = Guid.NewGuid();
                        meal.Id = mealId;
                        meal.NutritionScheduleId = request.NutritionScheduleId;
                        meal.MealTime = 1;
                        meal.Datetime = item.Date;
                        meal.IsDelete = false;
                        await _context.Meals.AddAsync(meal);

                        //Them tung mon vao bua an
                        foreach (var sang in request.MonAnSang)
                        {
                            var fim = new FoodAndSupplimentInMeal();
                            var fimId = Guid.NewGuid();
                            fim.Id = fimId;
                            fim.FoodAndSupplimentId = sang;
                            fim.MealId = mealId;
                            await _context.FoodAndSupplimentInMeals.AddAsync(fim);
                        }
                    }
                    if (request.MonAnTrua != null)  //Neu co mon thi tao bua an
                    {
                        var meal = new Meal();
                        var mealId = Guid.NewGuid();
                        meal.Id = mealId;
                        meal.NutritionScheduleId = request.NutritionScheduleId;
                        meal.MealTime = 2;
                        meal.Datetime = item.Date;
                        meal.IsDelete = false;
                        await _context.Meals.AddAsync(meal);

                        //Them tung mon vao bua an
                        foreach (var trua in request.MonAnTrua)
                        {
                            var fim = new FoodAndSupplimentInMeal();
                            var fimId = Guid.NewGuid();
                            fim.Id = fimId;
                            fim.FoodAndSupplimentId = trua;
                            fim.MealId = mealId;
                            await _context.FoodAndSupplimentInMeals.AddAsync(fim);
                        }
                    }
                    if (request.MonAnToi != null)  //Neu co mon thi tao bua an
                    {
                        var meal = new Meal();
                        var mealId = Guid.NewGuid();
                        meal.Id = mealId;
                        meal.NutritionScheduleId = request.NutritionScheduleId;
                        meal.MealTime = 3;
                        meal.Datetime = item.Date;
                        meal.IsDelete = false;
                        await _context.Meals.AddAsync(meal);

                        //Them tung mon vao bua an
                        foreach (var toi in request.MonAnToi)
                        {
                            var fim = new FoodAndSupplimentInMeal();
                            var fimId = Guid.NewGuid();
                            fim.Id = fimId;
                            fim.FoodAndSupplimentId = toi;
                            fim.MealId = mealId;
                            await _context.FoodAndSupplimentInMeals.AddAsync(fim);
                        }
                    }
                    if (request.MonAnTruocTap != null)  ///Neu co mon thi tao bua an
                    {
                        var meal = new Meal();
                        var mealId = Guid.NewGuid();
                        meal.Id = mealId;
                        meal.NutritionScheduleId = request.NutritionScheduleId;
                        meal.MealTime = 4;
                        meal.Datetime = item.Date;
                        meal.IsDelete = false;
                        await _context.Meals.AddAsync(meal);

                        //Them tung mon vao bua an
                        foreach (var truoctap in request.MonAnTruocTap)
                        {
                            var fim = new FoodAndSupplimentInMeal();
                            var fimId = Guid.NewGuid();
                            fim.Id = fimId;
                            fim.FoodAndSupplimentId = truoctap;
                            fim.MealId = mealId;
                            await _context.FoodAndSupplimentInMeals.AddAsync(fim);
                        }
                    }
                    await _context.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> CheckvalidDate(MealCreateViewModel request)
        {
            var dates = new List<DateTime>();
            for (var dt = request.FromDatetime; dt <= request.ToDatetime; dt = dt.AddDays(1))
            {
                dates.Add(dt);
            }

            var meals = await _context.Meals.ToListAsync();
            if(meals.Count == 0) return true;
            var inDB = new List<DateTime>();
            foreach (var meal in meals)
            {
                inDB.Add(meal.Datetime);
            }

            foreach (var date in dates) //From FE
            {
                foreach (var db in inDB)
                {
                    if (date.Date == db) return false;
                }
            }

            return true;
        }
    }
}
