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
        //Task<bool> DeletePackage(Guid id);
        Task<List<GymerPackageActiveViewModel>> GetGymerPackageActiveByPT(Guid PTID);
        Task<List<GymerPackageActiveViewModel>> GetGymerPackageActiveByNE(Guid NEID);
    }
}
