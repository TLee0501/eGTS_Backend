using eGTS_Backend.Data.Models;
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

            if (packageRequest.NumberOfMonth != null)
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

            //create payment record
            var paymentrecord = new Payment
            {
                Id = Guid.NewGuid(),
                PackageGymerId = id,
                PaymentDate = DateTime.Now,
                Amount = packageRequest.Price
            };
            _context.Payments.AddAsync(paymentrecord);

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
                temp.NumberOfMonth = _context.Packages.FindAsync(item.PackageId).Result.NumberOfMonth;
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
        public async Task<List<GymerPackageFilterByGymerViewModel>> GetGymerPackageActiveByNE(Guid NEID)
        {
            List<GymerPackageActiveViewModel> result = new List<GymerPackageActiveViewModel>();

            var listGymerPackage = await _context.PackageGymers.Where(a => a.Neid == NEID && a.IsDelete == false).ToListAsync();
            foreach (var item in listGymerPackage)
            {
                var gymerActive = new GymerPackageActiveViewModel();
                gymerActive.GymerId = item.GymerId;
                gymerActive.PackageName = item.Name;
                gymerActive.PackageGymerId = item.Id;
                gymerActive.GymerName = _context.Accounts.FindAsync(item.GymerId).Result.Fullname;
                gymerActive.From = (DateTime)item.From;
                gymerActive.Status = item.Status;
                gymerActive.NumberOfSession = _context.Packages.FindAsync(item.PackageId).Result.NumberOfsession;
                var s = _context.ExcerciseSchedules.SingleOrDefault(a => a.PackageGymerId == item.Id);
                var isUpdate = true;
                if (s == null) isUpdate = false;
                gymerActive.isUpdate = isUpdate;
                result.Add(gymerActive);
            }
            var filterResult = FilterByGymer(result);
            return filterResult;
        }
        public async Task<List<GymerPackageFilterByGymerViewModel>> GetGymerPackageActiveByPT(Guid PTID)
        {
            List<GymerPackageActiveViewModel> result = new List<GymerPackageActiveViewModel>();

            var listGymerPackage = await _context.PackageGymers.Where(a => a.Ptid == PTID && a.IsDelete == false).ToListAsync();
            foreach (var item in listGymerPackage)
            {
                var gymerActive = new GymerPackageActiveViewModel();
                gymerActive.GymerId = item.GymerId;
                gymerActive.PackageName = item.Name;
                gymerActive.PackageGymerId = item.Id;
                gymerActive.GymerName = _context.Accounts.FindAsync(item.GymerId).Result.Fullname;
                gymerActive.From = (DateTime)item.From;
                gymerActive.Status = item.Status;
                gymerActive.NumberOfSession = _context.Packages.FindAsync(item.PackageId).Result.NumberOfsession;
                var s = _context.ExcerciseSchedules.SingleOrDefault(a => a.PackageGymerId == item.Id);
                var isUpdate = true;
                if (s == null) isUpdate = false;
                gymerActive.isUpdate = isUpdate;
                result.Add(gymerActive);
            }
            var filterResult = FilterByGymer(result);
            return filterResult;
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

        public async Task<List<AccountIdAndNameViewModel>> GetGymersByNE(Guid NEID)
        {
            var pgs = await _context.PackageGymers.Where(a => a.Neid == NEID && a.IsDelete == false).ToListAsync();
            var gymersDup = new List<Guid>();
            foreach (var item in pgs)
            {
                gymersDup.Add(item.GymerId);
            }

            var gymers = gymersDup.Distinct().ToList();
            var result = new List<AccountIdAndNameViewModel>();
            foreach (var item in gymers)
            {
                var account = await _context.Accounts.FindAsync(item);
                var tmp = new AccountIdAndNameViewModel()
                {
                    Id = item,
                    Fullname = account.Fullname
                };
                result.Add(tmp);
            }
            return result;
        }

        public List<GymerPackageActiveViewModel> GetGymerPackagesByNEAndGymer(Guid NEID, Guid GymerId)
        {
            List<GymerPackageActiveViewModel> result = new List<GymerPackageActiveViewModel>();

            var listGymerPackage = _context.PackageGymers.Where(a => a.Neid == NEID && a.GymerId == GymerId && a.IsDelete == false).ToList();
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
                var s = _context.ExcerciseSchedules.SingleOrDefault(a => a.PackageGymerId == item.Id);
                var isUpdate = true;
                if (s == null) isUpdate = false;
                gymerActive.isUpdate = isUpdate;
                result.Add(gymerActive);
            }

            return result;
        }

        private List<GymerPackageFilterByGymerViewModel> FilterByGymer(List<GymerPackageActiveViewModel> request)
        {
            var groupedByGymerId = request
                .GroupBy(item => new { item.GymerId, item.GymerName })
                .Select(group => new GymerPackageFilterByGymerViewModel
                    {
                        GymerId = group.Key.GymerId,
                        GymerName = group.Key.GymerName,
                        GymerPackages = group.Select(item => new GymerPackage
                        {
                            PackageGymerId = item.PackageGymerId,
                            PackageName = item.PackageName,
                            From = item.From,
                            Status = item.Status,
                            NumberOfSession = item.NumberOfSession,
                            isUpdate = item.isUpdate
                        }).ToList()
                    }).ToList();
            return groupedByGymerId;
        }
    }
}
