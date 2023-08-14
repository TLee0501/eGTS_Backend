using eGTS_Backend.Data.ViewModel;

namespace eGTS.Bussiness.FeedbackService
{
    public interface IFeedbackService
    {
        Task<int> CreateFeedback(FeedbackCreateViewModel request);
        //Task<int> UpdateFeedback();
    }
}
