using eGTS_Backend.Data.Models;
using eGTS_Backend.Data.ViewModel;

namespace eGTS.Bussiness.FeedbackService
{
    public class FeedbackService : IFeedbackService
    {
        private readonly EGtsContext _context;

        public FeedbackService(EGtsContext context)
        {
            _context = context;
        }

        public async Task<int> CreateFeedback(FeedbackCreateViewModel request)
        {
            var PTorNE = await _context.Accounts.FindAsync(request.PtidorNeid);
            if (PTorNE == null) return 1;

            var pg = await _context.PackageGymers.FindAsync(request.PackageGymerId);
            if (pg == null) return 2;

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
    }
}
