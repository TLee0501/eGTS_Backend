using eGTS_Backend.Data.Models;
using eGTS_Backend.Data.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace eGTS.Bussiness.ReportService
{
    public class ReportService : IReportService
    {
        private readonly EGtsContext _context;

        public ReportService(EGtsContext context)
        {
            _context = context;
        }

        public async Task<List<GymerPackageActiveViewModel>> GetActivePackages()
        {
            var result = new List<GymerPackageActiveViewModel>();
            var listPG = await _context.PackageGymers.Where(a => a.Status == "Đang hoạt động" && a.IsDelete == false/* && a.From.Value.Month == DateTime.Now.Month && a.From.Value.Year == DateTime.Now.Year*/).ToListAsync();
            foreach (var item in listPG)
            {
                var tmp = new GymerPackageActiveViewModel();
                tmp.GymerId = item.GymerId;
                tmp.GymerName = _context.Accounts.FindAsync(item.GymerId).Result.Fullname;
                tmp.PackageGymerId = item.Id;
                tmp.PackageName = item.Name;
                tmp.From = (DateTime)item.From;
                tmp.Status = item.Status;
                tmp.NumberOfSession = _context.Packages.FindAsync(item.PackageId).Result.NumberOfsession;
                result.Add(tmp);
            }
            return result;
        }

        public async Task<List<GymerPackageActiveViewModel>> GetDonePackages()
        {
            var result = new List<GymerPackageActiveViewModel>();
            var listPG = await _context.PackageGymers.Where(a => a.Status == "Đã hoàn thành" && a.IsDelete == false/* && a.To.Value.Month == DateTime.Now.Month && a.To.Value.Year == DateTime.Now.Year*/).ToListAsync();
            foreach (var item in listPG)
            {
                var tmp = new GymerPackageActiveViewModel();
                tmp.GymerId = item.GymerId;
                tmp.GymerName = _context.Accounts.FindAsync(item.GymerId).Result.Fullname;
                tmp.PackageGymerId = item.Id;
                tmp.PackageName = item.Name;
                tmp.From = (DateTime)item.From;
                tmp.Status = item.Status;
                tmp.NumberOfSession = _context.Packages.FindAsync(item.PackageId).Result.NumberOfsession;
                result.Add(tmp);
            }
            return result;
        }

        public async Task<List<GymerPackageActiveViewModel>> GetPausePackages()
        {
            var result = new List<GymerPackageActiveViewModel>();
            var listPG = await _context.PackageGymers.Where(a => a.Status == "Tạm ngưng" && a.IsDelete == false).ToListAsync();
            foreach (var item in listPG)
            {
                var tmp = new GymerPackageActiveViewModel();
                tmp.GymerId = item.GymerId;
                tmp.GymerName = _context.Accounts.FindAsync(item.GymerId).Result.Fullname;
                tmp.PackageGymerId = item.Id;
                tmp.PackageName = item.Name;
                tmp.From = (DateTime)item.From;
                tmp.Status = item.Status;
                tmp.NumberOfSession = _context.Packages.FindAsync(item.PackageId).Result.NumberOfsession;
                result.Add(tmp);
            }
            return result;
        }

        public async Task<ReportInComeViewModel> getReportInCome()
        {
            var payments = await _context.Payments.ToListAsync();
            double total = 0;
            double monthly = 0;
            foreach (var item in payments)
            {
                total += item.Amount;
                if (item.PaymentDate.Month == DateTime.Now.Month && item.PaymentDate.Year == DateTime.Now.Year)
                {
                    monthly += item.Amount;
                }
            }

            var result = new ReportInComeViewModel()
            {
                TotalIncome = total,
                MonthlyIncome = monthly
            };
            return result;
        }
    }
}
