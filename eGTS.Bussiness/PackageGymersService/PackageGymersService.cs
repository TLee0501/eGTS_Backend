﻿using eGTS_Backend.Data.Models;
using eGTS_Backend.Data.ViewModel;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGTS.Bussiness.PackageGymersService
{
    public class PackageGymersService : IPackageGymersService
    {
        private readonly EGtsContext _context;

        public PackageGymersService(EGtsContext context)
        {
            _context = context;
        }

        public async Task<bool> CreatePackageGymer(PackageGymerCreateViewModel request)
        {
            Guid id = Guid.NewGuid();
            var packageRequest = await _context.Packages.FindAsync(request.PackageID);
            PackageGymer packageGymer = new PackageGymer(id, packageRequest.Name, request.GymerID, request.PackageID, null, null, packageRequest.NumberOfsession, "Pause");
            _context.PackageGymers.Add(packageGymer);
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
            return false;
        }

        public async Task<List<PackageGymerViewModel>> GetPackageGymerByGymerID(Guid request)
        {
            var listPackageGymer = await _context.PackageGymers.Where(a => a.GymerId == request).ToListAsync();
            List<PackageGymerViewModel> result = new List<PackageGymerViewModel>();
            foreach (var item in listPackageGymer)
            {
                var temp = new PackageGymerViewModel();
                temp.Id = item.Id;
                temp.Name = item.Name;
                temp.Ptid = item.Ptid;
                temp.Neid = item.Neid;
                temp.NumberOfSession = item.NumberOfSession;
                temp.Status = item.Status;
                result.Add(temp);
            }
            return result;
        }

        public async Task<bool> UpdatePackageGymer(PackageGymerViewModel request)
        {
            var packageGymerRequest = await _context.PackageGymers.FindAsync(request.Id);
            PackageGymer packageGymer = new PackageGymer(request.Id, request.Name, request.GymerId, packageGymerRequest.PackageId, request.Ptid, request.Neid, request.NumberOfSession, request.Status);
            _context.Entry(packageGymer).State = EntityState.Modified;
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
        public async Task<List<GymerPackageActiveViewModel>> GetGymerPackageActiveByNE(Guid NEID)
        {
            List<GymerPackageActiveViewModel> result = new List<GymerPackageActiveViewModel>();

            var listGymerPackage = await _context.PackageGymers.Where(a => a.Neid == NEID).ToListAsync();
            foreach (var item in listGymerPackage)
            {
                var gymerActive = new GymerPackageActiveViewModel();
                gymerActive.GymerId = item.GymerId;
                //gymerActive.PackageName = item.Name;
                gymerActive.GymerName = _context.Accounts.FindAsync(item.GymerId).Result.Fullname;
                result.Add(gymerActive);
            }

            return null;
        }
    }
}