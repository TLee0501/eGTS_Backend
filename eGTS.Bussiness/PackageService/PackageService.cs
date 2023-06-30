using eGTS_Backend.Data.Models;
using eGTS_Backend.Data.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGTS.Bussiness.PackageService
{
    public class PackageService : IPackageService
    {
        private readonly EGtsContext _context;
        public PackageService(EGtsContext context)
        {
            _context = context;
        }
        private bool PackageExists(Guid id)
        {
            return (_context.Packages?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        public async Task<bool> CreatePackage(PackageCreateViewModel request)
        {
            Guid id = Guid.NewGuid();
            Package package = new Package(id, request.HasPt, request.HasNe, request.NumberOfsession, request.Price);
            _context.Packages.Add(package);
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
                return false;
            }
        }

        public async Task<bool> DeletePackage(Guid id)
        {
            var package = await _context.Packages.FindAsync(id);
            if (package == null) return false;
            else
            {
                _context.Packages.Remove(package);
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<PackageViewModel> GetPackage(Guid id)
        {
            PackageViewModel result = new PackageViewModel();
            if (_context.Packages == null)
            {
                return null;
            }
            var package = await _context.Packages.FindAsync(id);
            if (package == null) return null;
            else
            {
                result.Id = package.Id;
                result.Price = package.Price;
                result.HasPt = package.HasPt;
                result.HasNe = package.HasNe;
                result.NumberOfsession = package.NumberOfsession;
                return result;
            }
        }

        public async Task<List<PackageViewModel>> GetPackages()
        {
            var packages = await _context.Packages.ToListAsync();

            if (packages != null)
            {
                List<PackageViewModel> result = new List<PackageViewModel>();
                foreach (var package in packages)
                {
                    var viewModel = new PackageViewModel();
                    viewModel.Id = package.Id;
                    viewModel.Price = package.Price;
                    viewModel.HasPt = package.HasPt;
                    viewModel.HasNe = package.HasNe;
                    viewModel.NumberOfsession = package.NumberOfsession;
                    result.Add(viewModel);
                }
                return result;
            }
            return null;
        }

        public async Task<bool> UpdatePackage(PackageViewModel request)
        {
            Package package = new Package(request.Id, request.HasPt, request.HasNe, request.NumberOfsession, request.Price);
            _context.Entry(package).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
                return false;
            }
        }
    }
}
