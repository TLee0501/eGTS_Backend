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
        Task<bool> UpdateSession(Guid id, SessionUpdateViewModel request);
        Task<bool> DeleteSession(Guid id);
        Task<SessionViewModel> GetSessionByID(Guid id);
        Task<List<SessionViewModel>> GetSessionListWithSceduleID(Guid id);
        Task<List<SessionViewModel>> DebugGetAllSessionList();

    }
}
