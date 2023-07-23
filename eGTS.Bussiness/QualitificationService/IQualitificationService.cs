using eGTS_Backend.Data.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace eGTS.Bussiness.QualitificationService
{
    public interface IQualitificationService
    {
        Task<bool> CreateQualitification(QualitificationCreateViewModel request);
        Task<ActionResult<QualitificationViewModel>> GetQualitificationByAccountId(Guid id);
        Task<bool> DeleteQualitification(Guid id);
        Task<bool> UpdateQualitification(QualitificationViewModel request);
    }
}
