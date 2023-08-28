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
        Task<List<PackageGymerViewModel>> GetPackageGymerByGymerID(Guid request);
        Task<bool> UpdatePackageGymer(PackageGymerViewModel request);
        Task<bool> CreatePackageGymer(PackageGymerCreateViewModel request);
        List<GymerPackageActiveViewModel> GetGymerPackageActiveByPT(Guid PTID);
        List<GymerPackageActiveViewModel> GetGymerPackageActiveByNE(Guid NEID);
        Task<bool> CheckAlreadyPackGymerHasCenter(Guid id);
        Task<bool> CheckAlreadyPackGymerHasNE(Guid id);
        Task<List<AccountIdAndNameViewModel>> GetGymersByNE(Guid NEID);
        List<GymerPackageActiveViewModel> GetGymerPackagesByNEAndGymer(Guid NEID, Guid GymerId);
    }
}
