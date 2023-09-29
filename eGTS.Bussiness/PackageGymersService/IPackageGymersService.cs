using eGTS_Backend.Data.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGTS.Bussiness.PackageGymersService
{
    public interface IPackageGymersService
    {
        //Task<PackageGymerViewModel> GetPackage(Guid id);
        Task<List<PackageGymerNameViewModel>> GetPackageGymerByGymerID(Guid request);
        Task<bool> UpdatePackageGymer(PackageGymerViewModel request);
        Task<bool> CreatePackageGymer(PackageGymerCreateViewModel request);
        Task<List<GymerPackageFilterByGymerViewModel>> GetGymerPackageActiveByPT(Guid PTID);
        Task<List<GymerPackageFilterByGymerViewModel>> GetGymerPackageActiveByNE(Guid NEID);
        Task<bool> CheckAlreadyPackGymerHasCenter(Guid id);
        Task<bool> CheckAlreadyPackGymerHasNE(Guid id);
        Task<List<AccountIdAndNameViewModel>> GetGymersByNE(Guid NEID);
        List<GymerPackageActiveViewModel> GetGymerPackagesByNEAndGymer(Guid NEID, Guid GymerId);
    }
}
