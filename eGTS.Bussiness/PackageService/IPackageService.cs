using eGTS_Backend.Data.Models;
using eGTS_Backend.Data.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGTS.Bussiness.PackageService
{
    public interface IPackageService
    {
        Task<List<PackageViewModel>> GetPackages();
        Task<PackageViewModel> GetPackage(Guid id);
        Task<bool> UpdatePackage(PackageViewModel package);
        Task<bool> CreatePackage(PackageCreateViewModel package);
        Task<bool> DeletePackage(Guid id);
        Task<List<PackageMobileViewModel>> GetPackagesForMobile();

    }
}
