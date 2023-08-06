using eGTS_Backend.Data.Models;
using eGTS_Backend.Data.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
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
            Package package = new Package(id, request.Name, request.HasPt, request.HasNe, request.NumberOfsession, request.NumberOfMonth, request.Ptcost, request.Necost, request.CenterCost, request.Price, request.Discount, false);
            _context.Packages.Add(package);
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeletePackage(Guid id)
        {
            var package = await _context.Packages.FindAsync(id);
            if (package == null) return false;
            else
            {
                package.IsDelete = true;
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
                if (package.NumberOfsession != null) result.NumberOfsession = (short)package.NumberOfsession;
                if (package.NumberOfMonth != null) result.NumberOfMonth = (short)package.NumberOfMonth;
                result.Name = package.Name;
                result.Ptcost = package.Ptcost;
                result.Necost = package.Necost;
                result.CenterCost = package.CenterCost;
                result.IsDelete = package.IsDelete;
                result.Discount = package.Discount;
                return result;
            }
        }

        public async Task<List<PackageViewModel>> GetPackages()
        {
            var packages = await _context.Packages.ToListAsync();

            if (packages.Count > 0)
            {
                List<PackageViewModel> result = new List<PackageViewModel>();
                foreach (var package in packages)
                {
                    var viewModel = new PackageViewModel();
                    viewModel.Id = package.Id;
                    viewModel.Name = package.Name;
                    viewModel.HasPt = package.HasPt;
                    viewModel.HasNe = package.HasNe;
                    viewModel.NumberOfsession = package.NumberOfsession;
                    viewModel.NumberOfMonth = package.NumberOfMonth;
                    viewModel.Ptcost = package.Ptcost;
                    viewModel.Necost = package.Necost;
                    viewModel.CenterCost = package.CenterCost;
                    viewModel.Price = package.Price;
                    viewModel.IsDelete = package.IsDelete;
                    viewModel.IsDelete = package.IsDelete;
                    result.Add(viewModel);
                }
                return result;
            }
            return null;
        }

        public async Task<bool> UpdatePackage(PackageViewModel request)
        {
            Package package = new Package(request.Id, request.Name, request.HasPt, request.HasNe, request.NumberOfsession, request.NumberOfMonth, request.Ptcost, request.Necost, request.CenterCost, request.Price, request.Discount, request.IsDelete);
            _context.Entry(package).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
