using eGTS_Backend.Data.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGTS.Bussiness.ReportService
{
    public interface IReportService 
    {
        Task<List<GymerPackageActiveViewModel>> GetActivePackages();
        Task<List<GymerPackageActiveViewModel>> GetDonePackages();
        Task<List<GymerPackageActiveViewModel>> GetPausePackages();

        Task<List<GymerPackageActiveViewModel>> GetActivePackagesByTime(int month, int year);
        Task<List<GymerPackageActiveViewModel>> GetDonePackagesByTime(int month, int year);
        Task<List<GymerPackageActiveViewModel>> GetPausePackagesByTime(int month, int year);
    }
}
