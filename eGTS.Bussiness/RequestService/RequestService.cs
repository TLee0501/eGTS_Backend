using eGTS.Bussiness.ExcerciseScheduleService;
using eGTS.Bussiness.NutritionScheduleService;
using eGTS_Backend.Data.Models;
using eGTS_Backend.Data.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace eGTS.Bussiness.RequestService
{
    public class RequestService : IRequestService
    {
        private readonly EGtsContext _context;

        public RequestService(EGtsContext context)
        {
            _context = context;
        }

        public async Task<int> CreateRequest(RequestCreateViewModel request)
        {
            var account = await _context.Accounts.FindAsync(request.ReceiverId);
            var boolPT = false;
            if(account.Role.Equals("PT")) boolPT = true;

            //check sent Request before
            var checkExist = await _context.Requests.SingleOrDefaultAsync(a => a.ReceiverId == request.ReceiverId && a.PackageGymerId == request.PackageGymerId);
            if (checkExist != null) return 2;

            //check Type PackageGymer
            var checkpackageGymer = await _context.PackageGymers.FindAsync(request.PackageGymerId);
            if (checkpackageGymer.Status != "Đang chờ") return 4;
            var checkPackage = await _context.Packages.FindAsync(checkpackageGymer.PackageId);
            var checkExpert = await _context.Accounts.FindAsync(request.ReceiverId);
            if (checkPackage.HasPt == false && checkExpert.Role == "PT") return 3;
            if (checkPackage.HasNe == false && checkExpert.Role == "NE") return 3;

            var id = Guid.NewGuid();
            var requestService = new Request(id, request.GymerId, request.ReceiverId, request.PackageGymerId, boolPT, null, false);
            
            try
            {
                _context.Requests.Add(requestService);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message, ex);
                return 0;
            }
            return 1;
        }

        public async Task<List<RequestViewModel>> GetAllRequestForPTNE(Guid id)
        {
            var requests = await _context.Requests.Where(a => a.ReceiverId == id && a.IsDelete == false).ToListAsync();
            List<RequestViewModel> result = new List<RequestViewModel>();
            foreach (var item in requests)
            {
                var account = await _context.Accounts.FindAsync(item.GymerId);
                var pg = await _context.PackageGymers.FindAsync(item.PackageGymerId);

                var temp = new RequestViewModel();
                temp.Id = item.Id;
                temp.ReceiverId = item.ReceiverId;
                temp.GymerName = account.Fullname;
                temp.GymerId = item.GymerId;
                temp.PackageGymerId = item.PackageGymerId;
                temp.PackageGymerName = pg.Name;
                temp.NumberOfSession = pg.NumberOfSession;
                temp.IsPt = item.IsPt;
                if (item.IsAccepted != null)
                {
                    temp.IsAccepted = item.IsAccepted;
                }
                result.Add(temp);
            }
            if(result.Count == 0) return null;
            return result;
        }

        public async Task<Request> GetRequest(Guid id)
        {
            var result = await _context.Requests.FindAsync(id);
            return result;
        }

        public async Task<bool> UpdateRequest(RequestUpdateViewModel request)
        {
            var requestDB = await _context.Requests.FindAsync(request.Id);

            var packageGymer = await _context.PackageGymers.FindAsync(requestDB.PackageGymerId);

            if (request.IsAccepted == true)
            {
                requestDB.IsAccepted = true;
                //Xóa request còn lại
                var requestNeedDelete = await _context.Requests.Where(a => a.PackageGymerId == requestDB.PackageGymerId && a.IsPt == requestDB.IsPt).ToListAsync();
                foreach (var item in requestNeedDelete)
                {
                    item.IsDelete = true;
                }

                if (requestDB.IsPt == true)
                {
                    packageGymer.Ptid = requestDB.ReceiverId; 
                }
                else
                {
                    packageGymer.Neid = requestDB.ReceiverId;

                    var id = Guid.NewGuid();
                    var schedule = new NutritionSchedule(id, packageGymer.GymerId, (Guid)packageGymer.Neid, packageGymer.Id, false);
                    await _context.NutritionSchedules.AddAsync(schedule);
                }
            }
            else
            {
                requestDB.IsAccepted = false;
                requestDB.IsDelete = true;
                await _context.SaveChangesAsync();
                return true;
            }
            
            var packageType = await _context.Packages.FindAsync(packageGymer.PackageId);

            //Update status PackageGymer
            if (packageGymer.Ptid != null && packageGymer.Neid != null)
            {
                packageGymer.Status = "Đang hoạt động";
                //packageGymer.From = DateTime.Now;
            }
            else if (packageType.HasPt == false && packageGymer.Neid != null)
            {
                packageGymer.Status = "Đang hoạt động";
                //packageGymer.From = DateTime.Now;
            }
            else if (packageType.HasNe == false && packageGymer.Ptid != null)
            {
                packageGymer.Status = "Đang hoạt động";
                //packageGymer.From = DateTime.Now;
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
    }
}
