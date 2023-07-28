using eGTS_Backend.Data.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGTS.Bussiness.SessionService
{
    public interface ISessionService
    {
        Task<bool> CreateSession(SessionCreateViewModel model);
        Task<bool> CreateSessionResult(SessionResultCreateViewModel model);
        Task<bool> CreateExcerciseInSession(ExInSessionCreateViewModel model);
        Task<bool> UpdateSession(Guid id, SessionUpdateViewModel request);
        Task<bool> UpdateSessionResult(Guid id, SessionResultUpdateViewModel model);
        Task<bool> UpdateExcerciseInSession(Guid id, ExInSessionUpdateViewModel request);
        Task<bool> DeleteSession(Guid id);
        Task<bool> DeleteSessionResult(Guid id);
        Task<bool> DeleteSessionPERMANENT(Guid id);
        Task<bool> DeleteSessionResultPERMANENT(Guid id);
        Task<bool> DeleteExcerciseInSessionPERMANENT(Guid id);
        Task<SessionViewModel> GetSessionByID(Guid id);
        Task<SessionResultViewModel> GetSessionResultByID(Guid id);
        Task<List<SessionResultViewModel>> GetSessionResultBySessionID(Guid id);
        Task<ExInSessionViewModel> GetExcerciseInSessionByID(Guid id);
        Task<List<ExInSessionViewModel>> GetExcerciseInSessionBySessionID(Guid id);
        Task<List<SessionViewModel>> GetSessionListWithSceduleID(Guid id);
        Task<List<SessionViewModel>> DebugGetAllSessionList();
        Task<List<SessionResultViewModel>> DebugGetAllSessionResultList();
        Task<List<ExInSessionViewModel>> DebugGetAllExcerciseInSessionList();
        Task<ExInSessionWithSessionIDViewModel> GetAllExcerciseInSessionWithSessionID(Guid SessionID);
        Task<ExInSessionWithSessionIDViewModel> GetAllExcerciseInSessionWithScheduleIDAndDateTime(Guid ScheduleID, DateTime dateTime);

    }
}
