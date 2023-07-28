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
        public async Task<ActionResult> GetNutritionScheduleByGymerID(Guid GymerId, DateTime date)
        {
            //Tim GymerPackageID
            var package = await _context.PackageGymers.SingleOrDefaultAsync(a => a.GymerId == GymerId && a.Status == "Active");
            if (package == null) return null;
            var GymerPackageId = package.Id;

            //Tim ScheduleID
            var schedule = await _context.NutritionSchedules.SingleOrDefaultAsync(a => a.Id == GymerPackageId);
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
                    /*if (item.me)
                    {
                        
                    }*/
                }
            }
            return null;
        }
    }
}
