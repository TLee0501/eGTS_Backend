using eGTS.Bussiness.AccountService;
using eGTS_Backend.Data.Models;
using eGTS_Backend.Data.ViewModel;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace eGTS.Bussiness.FeedbackService
{
    public class FeedbackService : IFeedbackService
    {
        private readonly EGtsContext _context;
        private readonly ILogger<IAccountService> _logger;

        public FeedbackService(EGtsContext context, ILogger<IAccountService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<int> CreateFeedback(FeedbackCreateViewModel request)
        {
            var PTorNE = await _context.Accounts.FindAsync(request.PtidorNeid);
            if (PTorNE == null) return 1;

            var pg = await _context.PackageGymers.FindAsync(request.PackageGymerId);
            if (pg == null) return 2;

            if (!pg.Ptid.Equals(PTorNE.Id) && !pg.Neid.Equals(PTorNE.Id)) return 4;

            if (pg.Status.Equals("Đang chờ") || pg.Status.Equals("Đang hoạt động") || pg.Status.Equals("Tạm ngưng")) return 5;

            if (checkIfFeedbackAlreadyMade(request)) return 6;

            try
            {
                var feedback = new FeedBack
                {
                    Id = Guid.NewGuid(),
                    PtidorNeid = PTorNE.Id,
                    PackageGymerId = request.PackageGymerId,
                    Rate = request.Rate,
                    Feedback1 = request.Feedback1,
                    CreateDate = DateTime.Now,
                    IsDelete = false
                };
                await _context.FeedBacks.AddAsync(feedback);
                await _context.SaveChangesAsync();
                return 3;
            }
            catch (Exception ex) { return 0; }
        }

        public async Task<List<FeedbackViewModel>> DEBUGGetALLFeedback()
        {
            var resultList = new List<FeedbackViewModel>();
            var feedbackList = _context.FeedBacks.ToList();
            foreach (var feedback in feedbackList)
            {
                var result = new FeedbackViewModel();
                result.Id = feedback.Id;
                result.PtidorNeid = feedback.PtidorNeid;
                result.PTOrNeName = _context.Accounts.Find(feedback.PtidorNeid).Fullname;
                result.PackageGymerId = feedback.PackageGymerId;
                var pg = _context.PackageGymers.Find(feedback.PackageGymerId);
                result.PackageName = pg.Name;
                result.GymerName = _context.Accounts.Find(pg.GymerId).Fullname;
                result.Rate = feedback.Rate;
                result.Feedback1 = feedback.Feedback1;
                result.IsDelete = feedback.IsDelete;

                resultList.Add(result);
            }
            if (resultList.Count > 0)
                return resultList;
            else
                return null;
        }

        public async Task<bool> ChangeisDeleteFeedback(Guid FeedbackID, bool status)
        {
            var feedback = await _context.FeedBacks.FindAsync(FeedbackID);
            if (feedback != null)
            {

                feedback.IsDelete = status;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> REMOVEFeedback(Guid FeedbackID)
        {
            var feedback = _context.FeedBacks.Find(FeedbackID);
            if (feedback != null)
            {
                _context.FeedBacks.Remove(feedback);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<int> UpdateFeedback(Guid FeedbackID, FeedbackUpdateViewModel request)
        {
            var feedback = _context.FeedBacks.Find(FeedbackID);
            if (feedback != null)
            {
                if (request.Rate <= 5 || request.Rate >= 0)
                {
                    feedback.Rate = request.Rate;
                }
                else
                    return 1;
                feedback.Feedback1 = request.Feedback1;

                await _context.SaveChangesAsync();
                return 2;
            }
            return 0;
        }

        public async Task<List<FeedbackViewModel>> GetFeedbackListByExpertID(Guid expertID, bool? isDelete)
        {
            var resultList = new List<FeedbackViewModel>();
            List<FeedBack> feedbackList = new List<FeedBack>();

            switch (isDelete)
            {
                case true:
                    feedbackList = _context.FeedBacks.Where(f => f.PtidorNeid.Equals(expertID) && f.IsDelete == true).ToList();
                    break;
                case false:
                    feedbackList = _context.FeedBacks.Where(f => f.PtidorNeid.Equals(expertID) && f.IsDelete == false).ToList();
                    break;
                default:
                    feedbackList = _context.FeedBacks.Where(f => f.PtidorNeid.Equals(expertID)).ToList();
                    break;
            }

            foreach (var feedback in feedbackList)
            {
                var result = new FeedbackViewModel();
                result.Id = feedback.Id;
                result.PtidorNeid = feedback.PtidorNeid;
                result.PTOrNeName = _context.Accounts.Find(feedback.PtidorNeid).Fullname;
                result.PackageGymerId = feedback.PackageGymerId;
                var pg = _context.PackageGymers.Find(feedback.PackageGymerId);
                result.PackageName = pg.Name;
                result.GymerName = _context.Accounts.Find(pg.GymerId).Fullname;
                result.Rate = feedback.Rate;
                result.Feedback1 = feedback.Feedback1;
                result.IsDelete = feedback.IsDelete;

                resultList.Add(result);
            }
            if (resultList.Count > 0)
                return resultList;
            else
                return null;
        }

        public async Task<FeedbackAverageViewModel> GetAverageRatingByExpertID(Guid expertID)
        {
            var feedbackList = _context.FeedBacks.Where(f => f.PtidorNeid.Equals(expertID) && f.IsDelete == false).ToList();
            if (feedbackList.Count == 0) return null;

            var listRating = new List<int>();

            foreach (var feedback in feedbackList)
                listRating.Add(feedback.Rate);

            var result = new FeedbackAverageViewModel();
            result.PtidorNeid = expertID;
            result.PTOrNeName = _context.Accounts.Find(expertID).Fullname;
            result.AverageRate = listRating.Average();

            return result;
        }

        public async Task<FeedbackViewModel> GetFeedbackByID(Guid feedbackID)
        {
            var feedback = _context.FeedBacks.Find(feedbackID);

            if (feedback == null) return null;

            var result = new FeedbackViewModel();
            result.Id = feedback.Id;
            result.PtidorNeid = feedback.PtidorNeid;
            result.PTOrNeName = _context.Accounts.Find(feedback.PtidorNeid).Fullname;
            result.PackageGymerId = feedback.PackageGymerId;
            var pg = _context.PackageGymers.Find(feedback.PackageGymerId);
            result.PackageName = pg.Name;
            result.GymerName = _context.Accounts.Find(pg.GymerId).Fullname;
            result.Rate = feedback.Rate;
            result.Feedback1 = feedback.Feedback1;
            result.IsDelete = feedback.IsDelete;

            return result;

        }

        public async Task<List<FeedbackViewModel>> GetFeedbackListByPackageGymerID(Guid packageGymerID, bool? isDelete)
        {
            var resultList = new List<FeedbackViewModel>();
            List<FeedBack> feedbackList = new List<FeedBack>();

            switch (isDelete)
            {
                case true:
                    feedbackList = _context.FeedBacks.Where(f => f.PackageGymerId.Equals(packageGymerID) && f.IsDelete == true).ToList();
                    break;
                case false:
                    feedbackList = _context.FeedBacks.Where(f => f.PackageGymerId.Equals(packageGymerID) && f.IsDelete == false).ToList();
                    break;
                default:
                    feedbackList = _context.FeedBacks.Where(f => f.PackageGymerId.Equals(packageGymerID)).ToList();
                    break;
            }

            foreach (var feedback in feedbackList)
            {
                var result = new FeedbackViewModel();
                result.Id = feedback.Id;
                result.PtidorNeid = feedback.PtidorNeid;
                result.PTOrNeName = _context.Accounts.Find(feedback.PtidorNeid).Fullname;
                result.PackageGymerId = feedback.PackageGymerId;
                var pg = _context.PackageGymers.Find(feedback.PackageGymerId);
                result.PackageName = pg.Name;
                result.GymerName = _context.Accounts.Find(pg.GymerId).Fullname;
                result.Rate = feedback.Rate;
                result.Feedback1 = feedback.Feedback1;
                result.IsDelete = feedback.IsDelete;

                resultList.Add(result);
            }
            if (resultList.Count > 0)
                return resultList;
            else
                return null;
        }

        public bool checkIfFeedbackAlreadyMade(FeedbackCreateViewModel model)
        {
            var feedback = _context.FeedBacks.Where(f => f.PackageGymerId.Equals(model.PackageGymerId) && f.PtidorNeid.Equals(model.PtidorNeid));
            if (feedback.ToList().Count == 0) return true;
            return false;
        }

    }
}
