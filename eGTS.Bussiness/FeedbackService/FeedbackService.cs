using eGTS.Bussiness.AccountService;
using eGTS_Backend.Data.Models;
using eGTS_Backend.Data.ViewModel;
using Microsoft.Extensions.Logging;

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
    }
}
