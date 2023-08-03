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
        private readonly INutritionScheduleService _scheduleService;
        private readonly IExcerciseScheduleService _excerciseScheduleService;

        public RequestService(EGtsContext context, INutritionScheduleService scheduleService, IExcerciseScheduleService excerciseScheduleService)
        {
            _context = context;
            _scheduleService = scheduleService;
            _excerciseScheduleService = excerciseScheduleService;
        }

        public async Task<int> CreateRequest(RequestCreateViewModel request)
        {
            var id = Guid.NewGuid();
            var requestService = new Request(id, request.GymerId, request.ReceiverId, request.PackageGymerId, request.IsPt, null, false);
            var checkExist = await _context.Requests.SingleOrDefaultAsync(a => a.ReceiverId == request.ReceiverId && a.PackageGymerId == request.PackageGymerId);
            if (checkExist != null) return 2;
            try
            {
                _context.Requests.Add(requestService);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
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
                var requestNeedDelete = await _context.Requests.Where(a => a.PackageGymerId == requestDB.PackageGymerId && a.IsPt == request.IsPt).ToListAsync();
                foreach (var item in requestNeedDelete)
                {
                    item.IsDelete = true;
                }

                if (request.IsPt == true)
                {
                    packageGymer.Ptid = requestDB.ReceiverId; 
                    await _context.SaveChangesAsync();
                    var schedule = await _excerciseScheduleService.CreateExcerciseScheduleV2(packageGymer.Id);
                    if (schedule == false) return false;
                }
                else
                {
                    packageGymer.Neid = requestDB.ReceiverId;
                    await _context.SaveChangesAsync();
                    var schedule = await _scheduleService.CreateNutritionSchedule(packageGymer.Id);
                    if (schedule == false) return false;
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
                packageGymer.From = DateTime.Now;
            }
            else if (packageType.HasPt == false && packageGymer.Neid != null)
            {
                packageGymer.Status = "Đang hoạt động";
                packageGymer.From = DateTime.Now;
            }
            else if (packageType.HasNe == false && packageGymer.Ptid != null)
            {
                packageGymer.Status = "Đang hoạt động";
                packageGymer.From = DateTime.Now;
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
