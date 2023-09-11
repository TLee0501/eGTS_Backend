using eGTS_Backend.Data.ViewModel;

namespace eGTS.Bussiness.ReportService
{
    public interface IReportService
    {
        Task<List<GymerPackageActiveViewModel>> GetActivePackages();
        Task<List<GymerPackageActiveViewModel>> GetDonePackages();
        Task<List<GymerPackageActiveViewModel>> GetPausePackages();
        Task<ReportInComeViewModel> getReportInCome();
    }
}
