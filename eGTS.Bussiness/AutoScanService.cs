using eGTS_Backend.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace eGTS.Bussiness
{
    public class AutoScanService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public AutoScanService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var _context = scope.ServiceProvider.GetRequiredService<EGtsContext>();
                    ScanOfGymerPackagesToFinish(_context);
                }
                await Task.Delay(10000);
            }
        }

        private async void ScanOfGymerPackagesToFinish(EGtsContext _context)
        {
            var PackageGymers = _context.PackageGymers.Where(a => a.IsDelete == false && a.Status == "Đang hoạt động").ToList();

            foreach (var item in PackageGymers)
            {
                var packageType = _context.Packages.Find(item.PackageId);
                if (packageType.HasPt == false & packageType.HasNe == false)    //goi co ban
                {
                    if (item.To.Value.Date < DateTime.Now.Date)
                    {
                        item.Status = "Đã hoàn thành";
                        item.To = DateTime.Now;
                    }
                }

                else if (packageType.HasPt == true & packageType.HasNe == false)    //goi chi pt
                {
                    var es = _context.ExcerciseSchedules.SingleOrDefault(a => a.PackageGymerId == item.Id && a.IsDelete == false);
                    if (es != null && es.To.Date < DateTime.Now.Date)
                    {
                        item.Status = "Đã hoàn thành";
                        item.To = DateTime.Now;
                    }
                }

                else if (packageType.HasPt == false & packageType.HasNe == true)    //goi chi ne
                {
                    var ns = _context.NutritionSchedules.SingleOrDefault(a => a.PackageGymerId == item.Id && a.IsDelete == false);
                    var meals = _context.Meals.Where(m => m.Datetime.Date > DateTime.Now.Date).ToList();
                    if (!meals.IsNullOrEmpty())
                    {
                        item.Status = "Đã hoàn thành";
                        item.To = DateTime.Now;
                    }
                }

                else if (packageType.HasPt == true & packageType.HasNe == true)    //goi co pt&ne
                {
                    var es = _context.ExcerciseSchedules.SingleOrDefault(a => a.PackageGymerId == item.Id && a.IsDelete == false);
                    var ns = _context.NutritionSchedules.SingleOrDefault(a => a.PackageGymerId == item.Id && a.IsDelete == false);
                    var meals = _context.Meals.Where(m => m.Datetime.Date > DateTime.Now.Date).ToList();

                    if (es != null && es.To.Date < DateTime.Now.Date && !meals.IsNullOrEmpty() && ns != null)
                    {
                        item.Status = "Đã hoàn thành";
                        item.To = DateTime.Now;
                    }
                }
            }
            try
            {
                _context.SaveChanges();
            } catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        private async void ScanOfGymerPackagesAtPause(EGtsContext _context)
        {

        }
    }
}