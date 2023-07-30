using Azure.Core;
using eGTS.Bussiness.AccountService;
using eGTS.Bussiness.ExcerciseScheduleService;
using eGTS.Bussiness.ExcerciseService;
using eGTS_Backend.Data.Models;
using eGTS_Backend.Data.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGTS.Bussiness.SessionService
{
    public class SessionService : ISessionService
    {

        private readonly EGtsContext _context;
        private readonly IExcerciseScheduleService _excerciseScheduleService;
        private readonly IExcerciseService _excerciseService;
        private readonly ILogger<IAccountService> _logger;

        public SessionService(EGtsContext context, IExcerciseScheduleService excerciseScheduleService,
            IExcerciseService excerciseService, ILogger<IAccountService> logger)
        {
            _context = context;
            _excerciseScheduleService = excerciseScheduleService;
            _excerciseService = excerciseService;
            _logger = logger;
        }

        public async Task<bool> CreateExcerciseInSession(ExInSessionCreateViewModel model)
        {
            Guid id = Guid.NewGuid();
            ExserciseInSession EIS = new ExserciseInSession(id, model.SessionId, model.ExerciseId);
            try
            {
                await _context.ExserciseInSessions.AddAsync(EIS);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Invalid data.");
                return false;
            }
        }

        public async Task<bool> CreateSession(SessionCreateViewModel model)
        {
            var exSchedule = await _excerciseScheduleService.GetExcerciseScheduleByID(model.ScheduleId);
            if (exSchedule != null)
            {
                if (exSchedule.From <= model.DateAndTime && model.DateAndTime <= exSchedule.To)
                {
                    var check = _context.Sessions.Where(s => s.ScheduleId.Equals(model.ScheduleId) && s.DateAndTime.Equals(model.DateAndTime));
                    if (check.Any())
                        return false;
                    Guid id = Guid.NewGuid();
                    Session session = new Session(id, model.ScheduleId, model.DateAndTime, false);

                    try
                    {
                        await _context.Sessions.AddAsync(session);
                        await _context.SaveChangesAsync();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError("Invalid Data");
                        return false;
                    }
                }
                else
                    return false;

            }
            else
                return false;

        }

        public async Task<bool> CreateSessionResult(SessionResultCreateViewModel model)
        {
            var session = await _context.Sessions.FindAsync(model.SessionId);
            if (session != null)
            {
                Guid id = new Guid();
                SessionResult sessionResult = new SessionResult(id, model.SessionId, model.Result, false);
                try
                {
                    await _context.SessionResults.AddAsync(sessionResult);
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Invalid Data");
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public async Task<List<ExInSessionViewModel>> DebugGetAllExcerciseInSessionList()
        {
            List<ExInSessionViewModel> resultList = new List<ExInSessionViewModel>();
            var ExInsessions = await _context.ExserciseInSessions.ToListAsync();
            if (ExInsessions.Count > 0)
            {
                foreach (var ex in ExInsessions)
                {
                    ExInSessionViewModel model = new ExInSessionViewModel();
                    model.Id = ex.Id;
                    model.SessionId = ex.SessionId;
                    model.ExerciseId = ex.ExerciseId;
                    resultList.Add(model);
                }
                return resultList;
            }
            return null;
        }

        public async Task<List<SessionViewModel>> DebugGetAllSessionList()
        {
            List<SessionViewModel> resultList = new List<SessionViewModel>();
            var sessions = await _context.Sessions.ToListAsync();
            if (sessions.Count > 0)
            {
                foreach (var session in sessions)
                {
                    SessionViewModel model = new SessionViewModel();
                    model.id = session.Id;
                    model.ScheduleId = session.ScheduleId;
                    model.DateAndTime = session.DateAndTime;
                    model.IsDelete = session.IsDelete;

                    resultList.Add(model);
                }
                return resultList;
            }
            return null;
        }

        public async Task<List<SessionResultViewModel>> DebugGetAllSessionResultList()
        {
            List<SessionResultViewModel> resultList = new List<SessionResultViewModel>();
            var sessionResults = await _context.SessionResults.ToListAsync();
            if (sessionResults.Count > 0)
            {
                foreach (var result in sessionResults)
                {
                    SessionResultViewModel model = new SessionResultViewModel();
                    model.id = result.Id;
                    model.SessionId = result.SessionId;
                    model.Result = result.Result;
                    model.IsDelete = result.IsDelete;

                    resultList.Add(model);
                }
                return resultList;
            }
            return null;
        }

        public async Task<bool> DeleteExcerciseInSessionPERMANENT(Guid id)
        {
            var EIS = await _context.ExserciseInSessions.FindAsync(id);
            if (EIS != null)
            {
                _context.ExserciseInSessions.Remove(EIS);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteSession(Guid id)
        {
            var session = await _context.Sessions.FindAsync(id);
            if (session == null)
                return false;

            session.IsDelete = true;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Unable to Delete");
            }
            return false;
        }

        public async Task<bool> DeleteSessionPERMANENT(Guid id)
        {
            var session = await _context.Sessions.FindAsync(id);
            if (session != null)
            {
                _context.Sessions.Remove(session);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteSessionResult(Guid id)
        {
            var sessionResult = await _context.SessionResults.FindAsync(id);
            if (sessionResult == null)
                return false;

            sessionResult.IsDelete = true;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Unable to Delete");
            }
            return false;
        }

        public async Task<bool> DeleteSessionResultPERMANENT(Guid id)
        {
            var sessionResult = await _context.SessionResults.FindAsync(id);
            if (sessionResult != null)
            {
                _context.SessionResults.Remove(sessionResult);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<ExInSessionWithSessionIDViewModel> GetAllExcerciseInSessionWithScheduleIDAndDateTime(Guid ScheduleID, DateTime dateTime)
        {
            Session session = _context.Sessions.FirstOrDefault(s => s.ScheduleId.Equals(ScheduleID) && s.DateAndTime.Equals(dateTime));
            var exInSessionList = _context.ExserciseInSessions.Where(s => s.SessionId.Equals(session.Id)).ToList();
            var excerciseList = new List<ExcerciseViewModel>();
            foreach (var exInSession in exInSessionList)
            {

                var excercise = _context.Excercises.Find(exInSession.ExerciseId);
                var ExV = new ExcerciseViewModel();
                ExV.id = excercise.Id;
                ExV.Ptid = excercise.Ptid;
                ExV.Name = excercise.Name;
                ExV.Description = excercise.Description;
                ExV.Video = excercise.Video;
                ExV.CreateDate = excercise.CreateDate;
                ExV.IsDelete = excercise.IsDelete;

                excerciseList.Add(ExV);
            }
            if (excerciseList.Count > 0)
            {
                var result = new ExInSessionWithSessionIDViewModel();
                result.SessionID = session.Id;
                result.SessionDateAndTime = session.DateAndTime;
                result.ExcercisesInSession = excerciseList;

                return result;
            }
            else
                return null;

        }

        public async Task<ExInSessionWithSessionIDViewModel> GetAllExcerciseInSessionWithSessionID(Guid SessionID)
        {
            var exInSessionList = _context.ExserciseInSessions.Where(s => s.SessionId == SessionID).ToList();
            var session = _context.Sessions.Find(SessionID);
            var excerciseList = new List<ExcerciseViewModel>();
            foreach (var exInSession in exInSessionList)
            {
                var excercise = _context.Excercises.Find(exInSession.ExerciseId);
                var ExV = new ExcerciseViewModel();
                ExV.id = excercise.Id;
                ExV.Ptid = excercise.Ptid;
                ExV.Name = excercise.Name;
                ExV.Description = excercise.Description;
                ExV.Video = excercise.Video;
                ExV.CreateDate = excercise.CreateDate;
                ExV.IsDelete = excercise.IsDelete;

                excerciseList.Add(ExV);
            }
            if (excerciseList.Count > 0)
            {
                var result = new ExInSessionWithSessionIDViewModel();
                result.SessionID = session.Id;
                result.SessionDateAndTime = session.DateAndTime;
                result.ExcercisesInSession = excerciseList;

                return result;
            }
            else
                return null;
        }

        public async Task<ExInSessionViewModel> GetExcerciseInSessionByID(Guid id)
        {
            var EIS = await _context.ExserciseInSessions.FindAsync(id);
            if (EIS != null)
            {
                ExInSessionViewModel result = new ExInSessionViewModel();
                result.Id = id;
                result.SessionId = EIS.Id;
                result.ExerciseId = EIS.ExerciseId;
                return result;
            }
            else
                return null;
        }

        public async Task<List<ExInSessionViewModel>> GetExcerciseInSessionBySessionID(Guid id)
        {
            List<ExInSessionViewModel> resultList = new List<ExInSessionViewModel>();
            var EISs = await _context.ExserciseInSessions.Where(s => s.SessionId == id).ToListAsync();
            if (EISs.Count > 0)
            {
                foreach (var EIS in EISs)
                {
                    ExInSessionViewModel model = new ExInSessionViewModel();
                    model.Id = EIS.Id;
                    model.SessionId = EIS.SessionId;
                    model.ExerciseId = EIS.ExerciseId;

                    resultList.Add(model);
                }
                return resultList;
            }
            return null;
        }

        public async Task<SessionViewModel> GetSessionByID(Guid id)
        {
            var session = await _context.Sessions.FindAsync(id);
            if (session != null)
            {
                SessionViewModel result = new SessionViewModel();
                result.id = session.Id;
                result.ScheduleId = session.ScheduleId;
                result.DateAndTime = session.DateAndTime;
                result.IsDelete = session.IsDelete;
                return result;
            }
            else
                return null;
        }

        public async Task<List<SessionViewModel>> GetSessionListWithSceduleID(Guid id)
        {
            List<SessionViewModel> resultList = new List<SessionViewModel>();
            var sessions = await _context.Sessions.Where(s => s.ScheduleId == id).ToListAsync();
            if (sessions.Count > 0)
            {
                foreach (var session in sessions)
                {
                    SessionViewModel model = new SessionViewModel();
                    model.id = session.Id;
                    model.ScheduleId = session.ScheduleId;
                    model.DateAndTime = session.DateAndTime;
                    model.IsDelete = session.IsDelete;

                    resultList.Add(model);
                }
                return resultList;
            }
            return null;
        }

        public async Task<SessionResultViewModel> GetSessionResultByID(Guid id)
        {
            var sessionResult = await _context.SessionResults.FindAsync(id);
            if (sessionResult != null)
            {
                SessionResultViewModel result = new SessionResultViewModel();
                result.id = sessionResult.Id;
                result.SessionId = sessionResult.SessionId;
                result.Result = sessionResult.Result;
                result.IsDelete = sessionResult.IsDelete;
                return result;
            }
            else
                return null;
        }

        public async Task<List<SessionResultViewModel>> GetSessionResultBySessionID(Guid id)
        {
            List<SessionResultViewModel> resultList = new List<SessionResultViewModel>();
            var sessionResults = await _context.SessionResults.Where(s => s.SessionId == id).ToListAsync();
            if (sessionResults.Count > 0)
            {
                foreach (var result in sessionResults)
                {
                    SessionResultViewModel model = new SessionResultViewModel();
                    model.id = result.Id;
                    model.SessionId = result.SessionId;
                    model.Result = result.Result;
                    model.IsDelete = result.IsDelete;

                    resultList.Add(model);
                }
                return resultList;
            }
            return null;
        }

        public async Task<bool> UpdateExcerciseInSession(Guid id, ExInSessionUpdateViewModel request)
        {
            var EIS = await _context.ExserciseInSessions.FindAsync(id);
            if (EIS == null)
                return false;
            if (!request.ExerciseId.Equals(""))
                EIS.ExerciseId = request.ExerciseId;
            if (!request.SessionId.Equals(""))
                EIS.SessionId = EIS.SessionId;


            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Unable to save changes");
            }
            return false;
        }

        public async Task<bool> UpdateSession(Guid id, SessionUpdateViewModel request)
        {

            var session = await _context.Sessions.FindAsync(id);
            if (session == null)
                return false;

            session.IsDelete = request.IsDelete;

            if (request.DateAndTime != null || !request.DateAndTime.Equals(""))
            {
                var exSchedule = await _excerciseScheduleService.GetExcerciseScheduleByID(session.ScheduleId);
                if (exSchedule.From <= request.DateAndTime && request.DateAndTime <= exSchedule.To)
                {
                    session.DateAndTime = request.DateAndTime;
                    try
                    {
                        await _context.SaveChangesAsync();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError("Unable to Update Session");
                    }
                }
            }
            return false;
        }

        public async Task<bool> UpdateSessionResult(Guid id, SessionResultUpdateViewModel request)
        {
            var sessionResult = await _context.SessionResults.FindAsync(id);
            if (sessionResult == null)
                return false;
            if (!request.Result.Equals("") || request.Result != null)
            {
                sessionResult.Result = request.Result;
                try
                {
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Unable to Update Session");
                }
            }
            return false;
        }

    }
}
