﻿using eGTS_Backend.Data.Models;
using eGTS_Backend.Data.ViewModel;
using Microsoft.EntityFrameworkCore;

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

            if(packageRequest.NumberOfMonth != null)
            {
                var month = (int)packageRequest.NumberOfMonth;
                PackageGymer packageGymer = new PackageGymer(id, packageRequest.Name, request.GymerID, request.PackageID, null, null, null, DateTime.Now, DateTime.Now.AddMonths(month), "Đang hoạt động", false);
                _context.PackageGymers.Add(packageGymer);
            }
            else
            {
                PackageGymer packageGymer = new PackageGymer(id, packageRequest.Name, request.GymerID, request.PackageID, null, null, packageRequest.NumberOfsession, DateTime.Now, null, "Đang chờ", false);
                _context.PackageGymers.Add(packageGymer);
            }
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

        public async Task<List<PackageGymerViewModel>> GetPackageGymerByGymerID(Guid request)
        {
            var listPackageGymer = await _context.PackageGymers.Where(a => a.GymerId == request).ToListAsync();
            List<PackageGymerViewModel> result = new List<PackageGymerViewModel>();
            foreach (var item in listPackageGymer)
            {
                var temp = new PackageGymerViewModel();
                temp.Id = item.Id;
                temp.GymerId = item.GymerId;
                temp.Name = item.Name;
                temp.Ptid = item.Ptid;
                temp.Neid = item.Neid;
                temp.NumberOfSession = item.NumberOfSession;
                temp.From = item.From;
                temp.To = item.To;
                temp.Status = item.Status;
                var checkPM = await _context.BodyPerameters.Where(a => a.GymerId == item.GymerId).ToListAsync();
                if (checkPM.Any()) temp.hasBodyParameter = true;
                else temp.hasBodyParameter = false;
                result.Add(temp);
            }
            return result;
        }

        public async Task<bool> UpdatePackageGymer(PackageGymerViewModel request)
        {
            var packageGymerRequest = await _context.PackageGymers.FindAsync(request.Id);
            PackageGymer packageGymer = new PackageGymer(request.Id, request.Name, request.GymerId, (Guid)packageGymerRequest.PackageId, request.Ptid, request.Neid, request.NumberOfSession, packageGymerRequest.From, packageGymerRequest.To, request.Status, request.isDelete);
            _context.Entry(packageGymer).State = EntityState.Modified;
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
        public List<GymerPackageActiveViewModel> GetGymerPackageActiveByNE(Guid NEID)
        {
            List<GymerPackageActiveViewModel> result = new List<GymerPackageActiveViewModel>();

            var listGymerPackage = _context.PackageGymers.Where(a => a.Neid == NEID && a.IsDelete == false).ToList();
            foreach (var item in listGymerPackage)
            {
                var gymerActive = new GymerPackageActiveViewModel();
                gymerActive.GymerId = item.GymerId;
                gymerActive.PackageName = item.Name;
                gymerActive.PackageGymerId = item.Id;
                gymerActive.GymerName = _context.Accounts.Find(item.GymerId).Fullname;
                gymerActive.From = (DateTime)item.From;
                gymerActive.Status = item.Status;
                gymerActive.NumberOfSession = _context.Packages.Find(item.PackageId).NumberOfsession;
                var s = _context.ExcerciseSchedules.SingleOrDefault(a => a.PackageGymerId == item.PackageId);
                var isUpdate = true;
                if (s == null) isUpdate = false;
                gymerActive.isUpdate = isUpdate;
                result.Add(gymerActive);
            }

            return result;
        }
        public List<GymerPackageActiveViewModel> GetGymerPackageActiveByPT(Guid PTID)
        {
            List<GymerPackageActiveViewModel> result = new List<GymerPackageActiveViewModel>();

            var listGymerPackage = _context.PackageGymers.Where(a => a.Ptid == PTID && a.IsDelete == false).ToList();
            foreach (var item in listGymerPackage)
            {
                var gymerActive = new GymerPackageActiveViewModel();
                gymerActive.GymerId = item.GymerId;
                gymerActive.PackageName = item.Name;
                gymerActive.PackageGymerId = item.Id;
                gymerActive.GymerName = _context.Accounts.Find(item.GymerId).Fullname;
                gymerActive.From = (DateTime)item.From;
                gymerActive.Status = item.Status;
                gymerActive.NumberOfSession = _context.Packages.Find(item.PackageId).NumberOfsession;
                var s = _context.ExcerciseSchedules.SingleOrDefault(a => a.PackageGymerId == item.PackageId);
                var isUpdate = true;
                if (s == null) isUpdate = false;
                gymerActive.isUpdate = isUpdate;
                result.Add(gymerActive);
            }

            return result;
        }

        public async Task<bool> CheckAlreadyPackGymerHasCenter(Guid id)
        {
            var gp = await _context.PackageGymers.Where(a => a.GymerId == id && a.IsDelete == false && a.Status != "Đã hoàn thành").ToListAsync();
            foreach (var item in gp)
            {
                if (item.Ptid == null && item.Neid == null) return true;
            }
            return false;
        }

        public async Task<bool> CheckAlreadyPackGymerHasNE(Guid id)
        {
            var gp = await _context.PackageGymers.Where(a => a.GymerId == id && a.IsDelete == false && a.Status != "Đã hoàn thành").ToListAsync();
            foreach (var item in gp)
            {
                if (item.Neid != null) return true;
            }
            return false;
        }
    }
}
