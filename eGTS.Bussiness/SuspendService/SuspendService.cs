using eGTS_Backend.Data.Models;
using eGTS_Backend.Data.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGTS.Bussiness.SuspendService
{
    public class SuspendService : ISuspendService
    {
        private readonly EGtsContext _context;

        public SuspendService(EGtsContext context)
        {
            _context = context;
        }

        public async Task<int> CreateSuspend(SuspendCreateViewModel request)
        {
            var pg = await _context.PackageGymers.FindAsync(request.PackageGymerId);
            if (pg == null) return 1;
            if (pg.From.Value.Date >= request.From.Date) return 2;
            if (pg.Status != "Đang hoạt động") return 5;

            TimeSpan timeDifference = request.To.Date - request.From.Date;
            int daysDifference = timeDifference.Days;
            if (daysDifference > 90) return 3;

            var suspend = new Suspend()
            {
                Id = Guid.NewGuid(),
                PackageGymerId = request.PackageGymerId,
                From = request.From.Date,
                To = request.To.Date,
                Reson = request.Reason,
                IsDelete = false
            };

            var packageType = _context.Packages.Find(pg.PackageId);
            if (packageType.HasPt == false & packageType.HasNe == false)    //goi co ban
            {
                pg.To.Value.AddDays(daysDifference);
            }

            else if (packageType.HasPt == true & packageType.HasNe == false)    //goi chi pt
            {
                var es = _context.ExcerciseSchedules.SingleOrDefault(a => a.PackageGymerId == pg.Id && a.IsDelete == false);
                if (es != null)
                {
                    es.To.AddDays(daysDifference);
                    var sessions = await _context.Sessions.Where(a => a.ScheduleId == es.Id && a.IsDelete == false && a.From.Date >= request.From.Date ).ToListAsync();
                    if (!sessions.IsNullOrEmpty())
                    {
                        foreach (var item in sessions)
                        {
                            item.From = item.From.AddDays(daysDifference);
                            item.To = item.To.AddDays(daysDifference);
                        }
                    }
                }
            }

            else if (packageType.HasPt == false & packageType.HasNe == true)    //goi chi ne
            {
                var ns = _context.NutritionSchedules.SingleOrDefault(a => a.PackageGymerId == pg.Id && a.IsDelete == false);
                var meals = _context.Meals.Where(a => a.Datetime.Date >= request.From.Date).ToList();
                if (!meals.IsNullOrEmpty())
                {
                    foreach (var item in meals)
                    {
                        item.Datetime.AddDays(daysDifference);
                    }
                }
            }

            else if (packageType.HasPt == true & packageType.HasNe == true)    //goi co pt&ne
            {
                var es = _context.ExcerciseSchedules.SingleOrDefault(a => a.PackageGymerId == pg.Id && a.IsDelete == false);
                var ns = _context.NutritionSchedules.SingleOrDefault(a => a.PackageGymerId == pg.Id && a.IsDelete == false);
                if (es != null)
                {
                    es.To.AddDays(daysDifference);
                    var sessions = await _context.Sessions.Where(a => a.ScheduleId == es.Id && a.IsDelete == false && a.From.Date >= request.From.Date).ToListAsync();
                    if (!sessions.IsNullOrEmpty())
                    {
                        foreach (var item in sessions)
                        {
                            item.From = item.From.AddDays(daysDifference);
                            item.To = item.To.AddDays(daysDifference);
                        }
                    }
                }

                var meals = _context.Meals.Where(a => a.Datetime.Date >= request.From.Date).ToList();
                if (!meals.IsNullOrEmpty())
                {
                    foreach (var item in meals)
                    {
                        item.Datetime.AddDays(daysDifference);
                    }
                }
            }

            pg.Status = "Tạm ngưng";

            try
            {
                await _context.Suspends.AddAsync(suspend);
                await _context.SaveChangesAsync();
                return 4;
            } catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<SuspendViewModel> GetSuspend(Guid Id)
        {
            var suspend = await _context.Suspends.FindAsync(Id);
            if (suspend == null) return null;
            var result = new SuspendViewModel()
            {
                Id = suspend.Id,
                From = suspend.From,
                To = suspend.To,
                Reson = suspend.Reson,
                IsDelete = suspend.IsDelete
            };
            return result;
        }

        public async Task<List<SuspendViewModel>> GetSuspends(Guid PackageGymerId)
        {
            var suspends = await _context.Suspends.Where(a => a.IsDelete == false && a.PackageGymerId == PackageGymerId).ToListAsync();
            if (suspends.IsNullOrEmpty()) return null;

            var result = new List<SuspendViewModel>();
            foreach ( var suspend in suspends)
            {
                var model = new SuspendViewModel()
                {
                    Id = suspend.Id,
                    From = suspend.From,
                    To = suspend.To,
                    Reson = suspend.Reson,
                    IsDelete = suspend.IsDelete
                };
                result.Add(model);
            }
            return result;
        }
    }
}
