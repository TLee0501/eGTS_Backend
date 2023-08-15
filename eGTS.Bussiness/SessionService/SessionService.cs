using eGTS.Bussiness.AccountService;
using eGTS.Bussiness.ExcerciseScheduleService;
using eGTS_Backend.Data.Models;
using eGTS_Backend.Data.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eGTS.Bussiness.SessionService
{
    public class SessionService : ISessionService
    {

        private readonly EGtsContext _context;
        private readonly IExcerciseScheduleService _excerciseScheduleService;
        private readonly ILogger<IAccountService> _logger;

        public SessionService(EGtsContext context, IExcerciseScheduleService excerciseScheduleService, ILogger<IAccountService> logger)
        {
            _context = context;
            _excerciseScheduleService = excerciseScheduleService;
            _logger = logger;
        }

        public async Task<bool> CreateExcerciseInSession(ExInSessionCreateViewModel model)
        {
            ExserciseInSession EIS = new ExserciseInSession
            {
                Id = Guid.NewGuid(),
                SessionId = model.SessionId,
                ExerciseId = model.ExerciseId
        };
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

        /*public async Task<bool> CreateSession(Guid scheduleID, DateTime startTime, double during)
        {
            var exSchedule = await _excerciseScheduleService.GetExcerciseScheduleByID(scheduleID);
            if (exSchedule != null)
            {
                if (exSchedule.From <= startTime && startTime <= exSchedule.To)
                {
                    var check = _context.Sessions.Where(s => s.ScheduleId.Equals(scheduleID) && s.From.Equals(startTime));
                    if (check.Any())
                        return false;
                    Guid id = Guid.NewGuid();
                    Session session = new Session(id, scheduleID, startTime, startTime.AddHours(during), false);

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
        }*/

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
                    model.From = session.From;
                    model.To = session.To; 
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
            Session session = _context.Sessions.FirstOrDefault(s => s.ScheduleId.Equals(ScheduleID) && s.From.Equals(dateTime));
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
                ExV.CalorieCumsumption = excercise.CalorieCumsumption;
                ExV.RepTime = excercise.RepTime;
                ExV.UnitOfMeasurement = excercise.UnitOfMeasurement;

                excerciseList.Add(ExV);
            }
            if (excerciseList.Count > 0)
            {
                var result = new ExInSessionWithSessionIDViewModel();
                result.SessionID = session.Id;
                result.From = session.From;
                result.To = session.To;
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
                ExV.CalorieCumsumption = excercise.CalorieCumsumption;
                ExV.RepTime = excercise.RepTime;
                ExV.UnitOfMeasurement = excercise.UnitOfMeasurement;

                excerciseList.Add(ExV);
            }
            if (excerciseList.Count > 0)
            {
                var result = new ExInSessionWithSessionIDViewModel();
                result.SessionID = session.Id;
                result.From = session.From;
                result.To = session.To;
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
                result.From = session.From;
                result.To = session.To;
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
                    model.From = session.From;
                    model.To = session.To;
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

        /*public async Task<bool> UpdateSession(Guid id, SessionUpdateViewModel request)
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
                    session.From = request.DateAndTime;
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
        }*/

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

        public List<Guid> GetListOfActivePackGymerIDByGymerID(Guid GymerID)
        {
            var result = new List<Guid>();
            var pgList = _context.PackageGymers.Where(p => p.Status.Equals("Đang hoạt động") && p.GymerId.Equals(GymerID)).ToList();
            if (pgList.Count > 0)
            {
                foreach (var pg in pgList)
                    result.Add(pg.Id);
                return result;
            }
            return null;
        }

        public async Task<List<ActiveSessionsViewModel>> GetListOfActiveSessionByGymerID(Guid GymerID)
        {
            var listPGID = GetListOfActivePackGymerIDByGymerID(GymerID);
            var result = new List<ActiveSessionsViewModel>();
            if (listPGID != null)
            {

                foreach (Guid PGID in listPGID)
                {

                    var scheduleList = _context.ExcerciseSchedules.Where(s => s.PackageGymerId.Equals(PGID)).ToList();

                    if (scheduleList.Count > 0)
                    {
                        foreach (var schedule in scheduleList)
                        {
                            List<SessionViewModel> sessionList = new List<SessionViewModel>();
                            var sessions = _context.Sessions.Where(s => s.ScheduleId == schedule.Id).ToList();
                            if (sessions.Count > 0)
                            {
                                foreach (var session in sessions)
                                {
                                    SessionViewModel modelSession = new SessionViewModel();
                                    modelSession.id = session.Id;
                                    modelSession.ScheduleId = session.ScheduleId;
                                    modelSession.From = session.From;
                                    modelSession.To = session.To;
                                    modelSession.IsDelete = session.IsDelete;

                                    sessionList.Add(modelSession);
                                }

                            }
                            ActiveSessionsViewModel modelActive = new ActiveSessionsViewModel();
                            modelActive.ScheduleID = PGID;
                            modelActive.SessionList = sessionList;
                            result.Add(modelActive);
                        }
                    }

                }
                if (result.Count > 0)
                {
                    return result;
                }
                return null;
            }
            return null;
        }

        /*public async Task<bool> CreateSessionV2(SessionCreateViewModelV2 model)
        {
            var pg = await _context.PackageGymers.FindAsync(model.PackageGymerID);
            if (pg == null) return false;

            if (pg.From > model.DateAndTime) return false;

            var exSchedule = await _context.ExcerciseSchedules.SingleOrDefaultAsync(a => a.PackageGymerId == model.PackageGymerID && a.IsDelete == false);
            if (exSchedule == null) return false;

            try
            {
                var id = Guid.NewGuid();
                var session = new Session(id, exSchedule.Id, model.DateAndTime, model.DateAndTime.AddHours(2), false);
                await _context.Sessions.AddAsync(session);

                foreach (var item in model.ListExcerciseID)
                {
                    var EInSID = Guid.NewGuid();
                    var EinS = new ExserciseInSession
                    {
                        Id = Guid.NewGuid(),
                        SessionId = id,
                        ExerciseId = item
                    };
                    await _context.ExserciseInSessions.AddAsync(EinS);
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Invalid Data");
                return false;
            }
        }*/

        public async Task<bool> UpdateSessionV3(Guid id, SessionUpdateViewModel request)
        {

            var session = await _context.Sessions.FindAsync(id);
            if (session == null)
                return false;

            var From = request.DateTime.Date.Add(TimeSpan.Parse(request.From));
            var To = request.DateTime.Date.Add(TimeSpan.Parse(request.To));

            var exSchedule = await _context.ExcerciseSchedules.FindAsync(session.ScheduleId);
            if (exSchedule.From <= request.DateTime)
            {
                session.From = From;
                session.To = To;

                if (request.ListExcercise.Count > 0 || request.ListExcercise != null)
                {
                    foreach (var item in request.ListExcercise)
                    {
                        var tmp = new ExserciseInSession
                        {
                            Id = Guid.NewGuid(),
                            SessionId = session.Id,
                            ExerciseId = item
                        };
                        await _context.ExserciseInSessions.AddAsync(tmp);
                    }
                }

                try
                {
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Unable to Update Session");
                    return false;
                }
            }
            else return false;
        }

        /*private async Task<bool> UpdateExcerciseListinSession(Guid SessionID, List<Guid> ExcerciseList)
        {
            var inDB = await _context.ExserciseInSessions.Where(a => a.SessionId == SessionID).ToListAsync();

            foreach (var item in inDB)
            {
                var existInRequest = false;
                foreach (var item1 in ExcerciseList)
                {
                    if (item.ExerciseId == item1) existInRequest = true; break;
                }
                if (existInRequest == false) {
                    var tmp = await _context.ExserciseInSessions.SingleOrDefaultAsync(a => a.SessionId == SessionID && a.ExerciseId == item.ExerciseId);
                    await _context.ExserciseInSessions.Remove(tmp);
                }  
            }
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Unable to UpdateExcerciseListinSession");
                return false;
            }
        }*/
    }
}
