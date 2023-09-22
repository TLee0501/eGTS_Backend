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
            var createDate = DateTime.Now;
            Package package = new Package(id, request.Name, request.HasPt, request.HasNe, request.NumberOfMonth, request.NumberOfsession, request.Ptcost, request.Necost, request.CenterCost, request.Price, request.Discount, createDate, false);
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
                result.CreateDate = package.CreateDate;
                return result;
            }
        }

        public async Task<List<PackageMobileViewModel>> GetPackagesForMobile()
        {
            var packages = await _context.Packages.ToListAsync();

            if (packages.Count > 0)
            {
                List<PackageMobileViewModel> result = new List<PackageMobileViewModel>();
                foreach (var package in packages)
                {
                    double oriPrice = 0;
                    if (package.Ptcost != null)
                    {
                        var totalPT = package.Ptcost * package.NumberOfsession;
                        oriPrice = (double)(oriPrice + totalPT);
                    }
                    if (package.Necost != null)
                    {
                        if (package.NumberOfsession != null)
                        {
                            oriPrice = (double)(oriPrice + package.Necost);
                        }
                        else
                        {
                            var totalNE = package.Necost * package.NumberOfMonth;
                            oriPrice = ((double)(oriPrice + totalNE));
                        }
                    }
                    if (package.CenterCost != null)
                    {
                        var totalCC = package.CenterCost * package.NumberOfMonth;
                        oriPrice = (double)(oriPrice + totalCC);
                    }

                    var viewModel = new PackageMobileViewModel();
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
                    viewModel.OriginPrice = oriPrice;
                    viewModel.Discount = package.Discount;
                    viewModel.CreateDate = package.CreateDate;
                    viewModel.IsDelete = package.IsDelete;
                    result.Add(viewModel);
                }
                return result;
            }
            return null;
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
                    viewModel.Discount = package.Discount;
                    viewModel.CreateDate = package.CreateDate;
                    viewModel.IsDelete = package.IsDelete;
                    result.Add(viewModel);
                }
                return result;
            }
            return null;
        }

        public async Task<bool> UpdatePackage(PackageViewModel request)
        {
            Package package = new Package(request.Id, request.Name, request.HasPt, request.HasNe, request.NumberOfsession, request.NumberOfMonth, request.Ptcost, request.Necost, request.CenterCost, request.Price, request.Discount, request.CreateDate, request.IsDelete);
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
