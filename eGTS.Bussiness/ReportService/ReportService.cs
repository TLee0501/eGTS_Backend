﻿using eGTS_Backend.Data.Models;
using eGTS_Backend.Data.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGTS.Bussiness.ReportService
{
    public class ReportService : IReportService
    {
        private readonly EGtsContext _context;

        public ReportService(EGtsContext context)
        {
            _context = context;
        }

        public async Task<List<GymerPackageActiveViewModel>> GetActivePackages()
        {
            var result = new List<GymerPackageActiveViewModel>();
            var listPG = await _context.PackageGymers.Where(a => a.Status == "Đang hoạt động" && a.IsDelete ==  false).ToListAsync();
            foreach (var item in listPG)
            {
                var tmp = new GymerPackageActiveViewModel();
                tmp.GymerId = item.GymerId;
                tmp.GymerName = _context.Accounts.FindAsync(item.GymerId).Result.Fullname;
                tmp.PackageGymerId = item.Id;
                tmp.PackageName = item.Name;
                result.Add(tmp);
            }
            return result;
        }

        public Task<List<GymerPackageActiveViewModel>> GetActivePackagesByTime(int month, int year)
        {
            throw new NotImplementedException();
        }

        public async Task<List<GymerPackageActiveViewModel>> GetDonePackages()
        {
            var result = new List<GymerPackageActiveViewModel>();
            var listPG = await _context.PackageGymers.Where(a => a.Status == "Đã hoàn thành" && a.IsDelete == false).ToListAsync();
            foreach (var item in listPG)
            {
                var tmp = new GymerPackageActiveViewModel();
                tmp.GymerId = item.GymerId;
                tmp.GymerName = _context.Accounts.FindAsync(item.GymerId).Result.Fullname;
                tmp.PackageGymerId = item.Id;
                tmp.PackageName = item.Name;
                result.Add(tmp);
            }
            return result;
        }

        public async Task<List<GymerPackageActiveViewModel>> GetDonePackagesByTime(int month, int year)
        {
            var result = new List<GymerPackageActiveViewModel>();
            var listPG = await _context.PackageGymers.Where(a => a.Status == "Đã hoàn thành" && a.IsDelete == false).ToListAsync();
            foreach (var item in listPG)
            {
                var tmp = new GymerPackageActiveViewModel();
                tmp.GymerId = item.GymerId;
                tmp.GymerName = _context.Accounts.FindAsync(item.GymerId).Result.Fullname;
                tmp.PackageGymerId = item.Id;
                tmp.PackageName = item.Name;
                result.Add(tmp);
            }
            return result;
        }

        public async Task<List<GymerPackageActiveViewModel>> GetPausePackages()
        {
            var result = new List<GymerPackageActiveViewModel>();
            var listPG = await _context.PackageGymers.Where(a => a.Status == "Tạm ngưng" && a.IsDelete == false).ToListAsync();
            foreach (var item in listPG)
            {
                var tmp = new GymerPackageActiveViewModel();
                tmp.GymerId = item.GymerId;
                tmp.GymerName = _context.Accounts.FindAsync(item.GymerId).Result.Fullname;
                tmp.PackageGymerId = item.Id;
                tmp.PackageName = item.Name;
                result.Add(tmp);
            }
            return result;
        }

        public Task<List<GymerPackageActiveViewModel>> GetPausePackagesByTime(int month, int year)
        {
            throw new NotImplementedException();
        }
    }
}