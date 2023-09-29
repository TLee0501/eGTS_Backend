using eGTS_Backend.Data.Models;
using eGTS_Backend.Data.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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
                var nuSchedule = await _context.NutritionSchedules.SingleOrDefaultAsync(a => a.PackageGymerId == request.PackageGymerID);
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
                        meal.NutritionScheduleId = nuSchedule.Id;
                        meal.MealTime = 1;
                        meal.Datetime = item.Date;
                        meal.IsDelete = false;
                        await _context.Meals.AddAsync(meal);

                        //Them tung mon vao bua an
                        foreach (var sang in request.MonAnSang)
                        {
                            var fim = new FoodAndSupplementInMeal();
                            var fimId = Guid.NewGuid();
                            fim.Id = fimId;
                            fim.FoodAndSupplementId = sang;
                            fim.MealId = mealId;
                            await _context.FoodAndSupplementInMeals.AddAsync(fim);
                        }
                    }
                    if (request.MonAnTrua != null)  //Neu co mon thi tao bua an
                    {
                        var meal = new Meal();
                        var mealId = Guid.NewGuid();
                        meal.Id = mealId;
                        meal.NutritionScheduleId = nuSchedule.Id;
                        meal.MealTime = 2;
                        meal.Datetime = item.Date;
                        meal.IsDelete = false;
                        await _context.Meals.AddAsync(meal);

                        //Them tung mon vao bua an
                        foreach (var trua in request.MonAnTrua)
                        {
                            var fim = new FoodAndSupplementInMeal();
                            var fimId = Guid.NewGuid();
                            fim.Id = fimId;
                            fim.FoodAndSupplementId = trua;
                            fim.MealId = mealId;
                            await _context.FoodAndSupplementInMeals.AddAsync(fim);
                        }
                    }
                    if (request.MonAnToi != null)  //Neu co mon thi tao bua an
                    {
                        var meal = new Meal();
                        var mealId = Guid.NewGuid();
                        meal.Id = mealId;
                        meal.NutritionScheduleId = nuSchedule.Id;
                        meal.MealTime = 3;
                        meal.Datetime = item.Date;
                        meal.IsDelete = false;
                        await _context.Meals.AddAsync(meal);

                        //Them tung mon vao bua an
                        foreach (var toi in request.MonAnToi)
                        {
                            var fim = new FoodAndSupplementInMeal();
                            var fimId = Guid.NewGuid();
                            fim.Id = fimId;
                            fim.FoodAndSupplementId = toi;
                            fim.MealId = mealId;
                            await _context.FoodAndSupplementInMeals.AddAsync(fim);
                        }
                    }
                    if (request.MonAnTruocTap != null)  ///Neu co mon thi tao bua an
                    {
                        var meal = new Meal();
                        var mealId = Guid.NewGuid();
                        meal.Id = mealId;
                        meal.NutritionScheduleId = nuSchedule.Id;
                        meal.MealTime = 4;
                        meal.Datetime = item.Date;
                        meal.IsDelete = false;
                        await _context.Meals.AddAsync(meal);

                        //Them tung mon vao bua an
                        foreach (var truoctap in request.MonAnTruocTap)
                        {
                            var fim = new FoodAndSupplementInMeal();
                            var fimId = Guid.NewGuid();
                            fim.Id = fimId;
                            fim.FoodAndSupplementId = truoctap;
                            fim.MealId = mealId;
                            await _context.FoodAndSupplementInMeals.AddAsync(fim);
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

            var nuSchedule = await _context.NutritionSchedules.SingleOrDefaultAsync(a => a.PackageGymerId == request.PackageGymerID);
            var meals = await _context.Meals.Where(a => a.NutritionScheduleId == nuSchedule.Id).ToListAsync();
            if (meals.Count == 0) return true;
            var inDB = new List<DateTime>();
            foreach (var meal in meals)
            {
                inDB.Add(meal.Datetime);
            }

            foreach (var date in dates) //From FE
            {
                foreach (var db in inDB)
                {
                    if (date.Date == db.Date) return false;
                }
            }

            return true;
        }

        public async Task<bool> UpdateMeal(MealCreateViewModel request)
        {
            var nuSchedule = await _context.NutritionSchedules.SingleOrDefaultAsync(a => a.PackageGymerId == request.PackageGymerID);


            var inDB = await _context.Meals.Where(a => a.NutritionScheduleId == nuSchedule.Id && a.IsDelete == false).ToListAsync();
            foreach (var item in inDB)
            {
                if (item.Datetime.Date >= request.FromDatetime.Date && item.Datetime.Date <= request.ToDatetime.Date)
                {
                    item.IsDelete = true;

                    var fim = await _context.FoodAndSupplementInMeals.Where(a => a.MealId == item.Id).ToListAsync();
                    foreach (var item1 in fim)
                    {
                        _context.FoodAndSupplementInMeals.Remove(item1);
                    }
                }
            }

            try
            {
                await _context.SaveChangesAsync();
                await CreateMeal(request);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdateMealFood(MealCreateViewModel request)
        {
            var pg = await _context.PackageGymers.FindAsync(request.PackageGymerID);
            var ns = await _context.NutritionSchedules.SingleOrDefaultAsync(a => a.PackageGymerId == pg.Id);

            if (!request.MonAnSang.IsNullOrEmpty())
            {
                var oldMs = await _context.Meals.Where(a => a.NutritionScheduleId == ns.Id && a.MealTime == 1 && a.Datetime.Date >= request.FromDatetime.Date && a.Datetime <= request.ToDatetime.Date).ToListAsync();
                if (oldMs != null)
                {
                    foreach (var item in oldMs)
                    {
                        item.IsDelete = true;

                        var oldFiMs = await _context.FoodAndSupplementInMeals.Where(a => a.MealId == item.Id).ToListAsync();
                        if (oldFiMs.Any())
                        {
                            foreach (var oldFiM in oldFiMs)
                            {
                                _context.FoodAndSupplementInMeals.Remove(oldFiM);
                            }
                        }
                    }
                    try
                    {
                        await _context.SaveChangesAsync();
                        var result = await CreateMeal(request);
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
            }

            if (!request.MonAnTrua.IsNullOrEmpty())
            {
                var oldMs = await _context.Meals.Where(a => a.NutritionScheduleId == ns.Id && a.MealTime == 2 && a.Datetime.Date >= request.FromDatetime.Date && a.Datetime <= request.ToDatetime.Date).ToListAsync();
                if (oldMs != null)
                {
                    foreach (var item in oldMs)
                    {
                        item.IsDelete = true;

                        var oldFiMs = await _context.FoodAndSupplementInMeals.Where(a => a.MealId == item.Id).ToListAsync();
                        if (oldFiMs.Any())
                        {
                            foreach (var oldFiM in oldFiMs)
                            {
                                _context.FoodAndSupplementInMeals.Remove(oldFiM);
                            }
                        }
                    }
                    try
                    {
                        await _context.SaveChangesAsync();
                        var result = await CreateMeal(request);
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
            }

            if (!request.MonAnToi.IsNullOrEmpty())
            {
                var oldMs = await _context.Meals.Where(a => a.NutritionScheduleId == ns.Id && a.MealTime == 3 && a.Datetime.Date >= request.FromDatetime.Date && a.Datetime <= request.ToDatetime.Date).ToListAsync();
                if (oldMs != null)
                {
                    foreach (var item in oldMs)
                    {
                        item.IsDelete = true;

                        var oldFiMs = await _context.FoodAndSupplementInMeals.Where(a => a.MealId == item.Id).ToListAsync();
                        if (oldFiMs.Any())
                        {
                            foreach (var oldFiM in oldFiMs)
                            {
                                _context.FoodAndSupplementInMeals.Remove(oldFiM);
                            }
                        }
                    }
                    try
                    {
                        await _context.SaveChangesAsync();
                        var result = await CreateMeal(request);
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
            }

            if (!request.MonAnTruocTap.IsNullOrEmpty())
            {
                var oldMs = await _context.Meals.Where(a => a.NutritionScheduleId == ns.Id && a.MealTime == 4 && a.Datetime.Date >= request.FromDatetime.Date && a.Datetime <= request.ToDatetime.Date).ToListAsync();
                if (oldMs != null)
                {
                    foreach (var item in oldMs)
                    {
                        item.IsDelete = true;

                        var oldFiMs = await _context.FoodAndSupplementInMeals.Where(a => a.MealId == item.Id).ToListAsync();
                        if (oldFiMs.Any())
                        {
                            foreach (var oldFiM in oldFiMs)
                            {
                                _context.FoodAndSupplementInMeals.Remove(oldFiM);
                            }
                        }
                    }
                    try
                    {
                        await _context.SaveChangesAsync();
                        var result = await CreateMeal(request);
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
