using eGTS_Backend.Data.ViewModel;

namespace eGTS.Bussiness.SuspendService
{
    public interface ISuspendService
    {
        Task<int> CreateSuspend(SuspendCreateViewModel request);
        Task<SuspendViewModel> GetSuspend(Guid Id);
        Task<List<SuspendViewModel>> GetSuspends(Guid PackageGymerId);
    }
}
