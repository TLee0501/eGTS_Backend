using eGTS_Backend.Data.ViewModel;

namespace eGTS.Bussiness.FeedbackService
{
    public interface IFeedbackService
    {
        Task<int> CreateFeedback(FeedbackCreateViewModel request);
        Task<int> UpdateFeedback(Guid FeedbackID, FeedbackUpdateViewModel request);
        Task<bool> ChangeisDeleteFeedback(Guid FeedbackID, bool status);
        Task<bool> REMOVEFeedback(Guid FeedbackID);
        Task<List<FeedbackViewModel>> DEBUGGetALLFeedback();
        Task<List<FeedbackViewModel>> GetFeedbackListByExpertID(Guid expertID, bool? isDelete);
        Task<FeedbackAverageViewModel> GetAverageRatingByExpertID(Guid expertID);
        Task<FeedbackViewModel> GetFeedbackByID(Guid feedbackID);
    }
}
