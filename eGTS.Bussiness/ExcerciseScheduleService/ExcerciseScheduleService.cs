using eGTS.Bussiness.AccountService;
using eGTS_Backend.Data.Models;
using eGTS_Backend.Data.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace eGTS.Bussiness.ExcerciseScheduleService
{
    public class ExcerciseScheduleService : IExcerciseScheduleService
    {
        private readonly EGtsContext _context;
        private readonly ILogger<IAccountService> _logger;

        public ExcerciseScheduleService(EGtsContext context, ILogger<IAccountService> logger)
        {
            _context = context;
            _logger = logger;
        }

        /*public async Task<bool> CreateExcerciseSchedule(ExScheduleCreateViewModel model)
        {
            Guid id = Guid.NewGuid();
            ExcerciseSchedule exSchedule = new ExcerciseSchedule(id, model.GymerId, model.Ptid, model.PackageGymerId, model.From, model.To, false);

            try
            {
                await _context.ExerciseSchedules.AddAsync(exSchedule);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Invalid data.");
                return false;
            }

        }*/

        public async Task<List<ExScheduleViewModel>> DEBUGGetAllExcerciseSchedule(bool? isExpired)
        {
            var resultList = new List<ExScheduleViewModel>();

            if (isExpired == false)
            {
                var ExScheduleList = await _context.ExerciseSchedules.Where(e => e.To > DateTime.Now).ToListAsync();
                foreach (ExerciseSchedule exSchedule in ExScheduleList)
                {
                    var result = new ExScheduleViewModel();
                    result.id = exSchedule.Id;
                    result.GymerId = exSchedule.GymerId;
                    result.Ptid = exSchedule.Ptid;
                    result.PackageGymerId = exSchedule.PackageGymerId;
                    result.From = exSchedule.From;
                    result.To = exSchedule.To;
                    result.IsDeleted = exSchedule.IsDelete;
                    resultList.Add(result);
                }
            }
            else if (isExpired == true)
            {
                var ExScheduleList = await _context.ExerciseSchedules.Where(e => e.To <= DateTime.Now).ToListAsync();
                foreach (ExerciseSchedule exSchedule in ExScheduleList)
                {
                    var result = new ExScheduleViewModel();
                    result.id = exSchedule.Id;
                    result.GymerId = exSchedule.GymerId;
                    result.Ptid = exSchedule.Ptid;
                    result.PackageGymerId = exSchedule.PackageGymerId;
                    result.From = exSchedule.From;
                    result.To = exSchedule.To;
                    result.IsDeleted = exSchedule.IsDelete;
                    resultList.Add(result);
                }
            }
            else
            {
                var ExScheduleList = await _context.ExerciseSchedules.ToListAsync();
                foreach (ExerciseSchedule exSchedule in ExScheduleList)
                {
                    var result = new ExScheduleViewModel();
                    result.id = exSchedule.Id;
                    result.GymerId = exSchedule.GymerId;
                    result.Ptid = exSchedule.Ptid;
                    result.PackageGymerId = exSchedule.PackageGymerId;
                    result.From = exSchedule.From;
                    result.To = exSchedule.To;
                    result.IsDeleted = exSchedule.IsDelete;
                    resultList.Add(result);
                }
            }

            if (resultList.Count > 0)
                return resultList;
            else
                return null;
        }

        public async Task<bool> DeleteExcerciseSchedulePERMANENT(Guid id)
        {
            var exSchedule = await _context.ExerciseSchedules.FindAsync(id);
            if (exSchedule != null)
            {
                _context.ExerciseSchedules.Remove(exSchedule);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<ExScheduleViewModel> GetExcerciseScheduleByID(Guid id)
        {
            var ExS = await _context.ExerciseSchedules.FindAsync(id);

            if (ExS == null) return null;
            if (ExS.IsDelete == true) return null;

            if (ExS != null)
            {
                var result = new ExScheduleViewModel();
                result.id = id;
                result.GymerId = ExS.GymerId;
                result.Ptid = ExS.Ptid;
                result.PackageGymerId = ExS.PackageGymerId;
                result.From = ExS.From;
                result.To = ExS.To;
                result.IsDeleted = ExS.IsDelete;
                return result;
            }
            return null;
        }

        public async Task<List<ExScheduleViewModel>> GetExcerciseSchedulesWithPTID(Guid PTID, bool? isExpired)
        {
            var resultList = new List<ExScheduleViewModel>();

            if (isExpired == false)
            {
                var ExScheduleList = await _context.ExerciseSchedules.Where(e => e.To > DateTime.Now && e.Ptid.Equals(PTID)).ToListAsync();
                foreach (ExerciseSchedule exSchedule in ExScheduleList)
                {
                    var result = new ExScheduleViewModel();
                    result.id = exSchedule.Id;
                    result.GymerId = exSchedule.GymerId;
                    result.Ptid = exSchedule.Ptid;
                    result.PackageGymerId = exSchedule.PackageGymerId;
                    result.From = exSchedule.From;
                    result.To = exSchedule.To;
                    result.IsDeleted = exSchedule.IsDelete;
                    resultList.Add(result);
                }
            }
            else if (isExpired == true)
            {
                var ExScheduleList = await _context.ExerciseSchedules.Where(e => e.To <= DateTime.Now && e.Ptid.Equals(PTID)).ToListAsync();
                foreach (ExerciseSchedule exSchedule in ExScheduleList)
                {
                    var result = new ExScheduleViewModel();
                    result.id = exSchedule.Id;
                    result.GymerId = exSchedule.GymerId;
                    result.Ptid = exSchedule.Ptid;
                    result.PackageGymerId = exSchedule.PackageGymerId;
                    result.From = exSchedule.From;
                    result.To = exSchedule.To;
                    result.IsDeleted = exSchedule.IsDelete;
                    resultList.Add(result);
                }
            }
            else
            {
                var ExScheduleList = await _context.ExerciseSchedules.Where(e => e.Ptid.Equals(PTID)).ToListAsync();
                foreach (ExerciseSchedule exSchedule in ExScheduleList)
                {
                    var result = new ExScheduleViewModel();
                    result.id = exSchedule.Id;
                    result.GymerId = exSchedule.GymerId;
                    result.Ptid = exSchedule.Ptid;
                    result.PackageGymerId = exSchedule.PackageGymerId;
                    result.From = exSchedule.From;
                    result.To = exSchedule.To;
                    result.IsDeleted = exSchedule.IsDelete;
                    resultList.Add(result);
                }
            }

            if (resultList.Count > 0)
                return resultList;
            else
                return null;
        }

        public async Task<List<ExScheduleViewModel>> GetExcerciseSchedulesWithGymerID(Guid GymerID, bool? isExpired)
        {
            var resultList = new List<ExScheduleViewModel>();

            if (isExpired == false)
            {
                var ExScheduleList = await _context.ExerciseSchedules.Where(e => e.To > DateTime.Now && e.GymerId.Equals(GymerID)).ToListAsync();
                foreach (ExerciseSchedule exSchedule in ExScheduleList)
                {
                    var result = new ExScheduleViewModel();
                    result.id = exSchedule.Id;
                    result.GymerId = exSchedule.GymerId;
                    result.Ptid = exSchedule.Ptid;
                    result.PackageGymerId = exSchedule.PackageGymerId;
                    result.From = exSchedule.From;
                    result.To = exSchedule.To;
                    result.IsDeleted = exSchedule.IsDelete;
                    resultList.Add(result);
                }
            }
            else if (isExpired == true)
            {
                var ExScheduleList = await _context.ExerciseSchedules.Where(e => e.To <= DateTime.Now && e.GymerId.Equals(GymerID)).ToListAsync();
                foreach (ExerciseSchedule exSchedule in ExScheduleList)
                {
                    var result = new ExScheduleViewModel();
                    result.id = exSchedule.Id;
                    result.GymerId = exSchedule.GymerId;
                    result.Ptid = exSchedule.Ptid;
                    result.PackageGymerId = exSchedule.PackageGymerId;
                    result.From = exSchedule.From;
                    result.To = exSchedule.To;
                    result.IsDeleted = exSchedule.IsDelete;
                    resultList.Add(result);
                }
            }
            else
            {
                var ExScheduleList = await _context.ExerciseSchedules.Where(e => e.GymerId.Equals(GymerID)).ToListAsync();
                foreach (ExerciseSchedule exSchedule in ExScheduleList)
                {
                    var result = new ExScheduleViewModel();
                    result.id = exSchedule.Id;
                    result.GymerId = exSchedule.GymerId;
                    result.Ptid = exSchedule.Ptid;
                    result.PackageGymerId = exSchedule.PackageGymerId;
                    result.From = exSchedule.From;
                    result.To = exSchedule.To;
                    result.IsDeleted = exSchedule.IsDelete;
                    resultList.Add(result);
                }
            }

            if (resultList.Count > 0)
                return resultList;
            else
                return null;
        }

        public async Task<bool> UpdateExcerciseSchedule(Guid id, ExScheduleUpdateViewModel request)
        {
            var exSchedule = await _context.ExerciseSchedules.FindAsync(id);
            if (exSchedule == null)
            {
                return false;
            }
            if (!request.Ptid.Equals("") || request.Ptid == null)
            {

                exSchedule.Ptid = request.Ptid;

            }
            if (!request.From.Equals(""))
            {
                DateTime fromDate = Convert.ToDateTime(request.From);
                exSchedule.From = fromDate;
            }

            if (!request.To.Equals(""))
            {
                DateTime toDate = Convert.ToDateTime(request.To);
                exSchedule.To = toDate;
            }

            exSchedule.IsDelete = request.IsDeleted;

            try
            {
                if (exSchedule.From > exSchedule.To)
                {
                    throw new Exception();
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Unable to save changes");
            }
            return false;

        }

        public async Task<bool> DeleteExcerciseSchedule(Guid id)
        {
            var exSchedule = await _context.ExerciseSchedules.FindAsync(id);
            if (exSchedule == null)
            {
                return false;
            }


            exSchedule.IsDelete = true;

            try
            {
                if (exSchedule.From > exSchedule.To)
                {
                    throw new Exception();
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Unable to save changes");
            }
            return false;
        }

        /*public async Task<bool> CreateExcerciseScheduleV2(Guid packageGymerID)
        {
            var pg = await _context.PackageGymers.FindAsync(packageGymerID);
            if (pg == null) return false;

            var id = Guid.NewGuid();
            var schedule = new ExcerciseSchedule(id, pg.GymerId, (Guid)pg.Ptid, packageGymerID, DateTime.Now, DateTime.Now.AddMonths(1), false);
            var schedule = new ExcerciseSchedule
            {
                Id = Guid.NewGuid(),
                GymerId = pg.GymerId,
                PackageGymerId = packageGymerID,

            }
            await _context.ExerciseSchedules.AddAsync(schedule);
            await _context.SaveChangesAsync();
            return true;
        }*/

        public async Task<List<SessionDetailViewModel>> GetExcerciseScheduleByGymerIDAndDate(Guid GymerId, DateTime date)
        {
            //Tim GymerPackageID
            var packageGymers = await _context.PackageGymers.Where(a => a.GymerId == GymerId && a.Status != "Đã hoàn thành").ToListAsync();
            if (packageGymers.Count == 0) return null;

            //Tim ScheduleID
            var scheduleIDs = new List<Guid>();
            foreach (var item in packageGymers)
            {
                var schedules = await _context.ExerciseSchedules.Where(a => a.PackageGymerId == item.Id && a.IsDelete == false).ToListAsync();
                if (schedules.Count > 0)
                {
                    foreach (var item1 in schedules)
                    {
                        scheduleIDs.Add(item1.Id);
                    }
                }
            }

            //Tim sessions
            var sessions = new List<Session>();
            foreach (var item in scheduleIDs)
            {
                var tmp = await _context.Sessions.Where(a => a.ScheduleId == item && a.From.Date == date.Date).ToListAsync();
                if (tmp.Count > 0 || tmp.IsNullOrEmpty() == false)
                {
                    foreach (var item1 in tmp)
                    {
                        sessions.Add(item1);
                    }
                }
            }

            //Thêm bài tập vào buổi tập
            var result = new List<SessionDetailViewModel>();
            foreach (var item in sessions)
            {
                var tmp = new SessionDetailViewModel();
                tmp.id = item.Id;
                tmp.ScheduleId = item.ScheduleId;
                tmp.From = item.From;
                tmp.To = item.To;
                tmp.Excercises = GetExcercises(item.Id);
                result.Add(tmp);
            }
            return result;
        }

        private List<ExcerciseViewModel> GetExcercises(Guid id)
        {
            var result = new List<ExcerciseViewModel>();

            var Exs = _context.ExerciseInSessions.Where(a => a.SessionId == id).ToList();
            if (Exs.Count == 0) return null;

            foreach (var item in Exs)
            {
                var excercise = _context.Exercises.SingleOrDefault(a => a.Id == item.ExerciseId && a.IsDelete == false);
                if (excercise != null)
                {
                    var viewModel = new ExcerciseViewModel();
                    viewModel.id = excercise.Id;
                    viewModel.Ptid = excercise.Ptid;
                    viewModel.Name = excercise.Name;
                    if (excercise.Description != null) viewModel.Description = excercise.Description;
                    if (excercise.Video != null) viewModel.Video = excercise.Video;
                    viewModel.CreateDate = excercise.CreateDate;
                    viewModel.CalorieCumsumption = excercise.CalorieCumsumption;
                    viewModel.RepTime = excercise.RepTime;
                    viewModel.UnitOfMeasurement = excercise.UnitOfMeasurement;
                    result.Add(viewModel);
                }
            }
            return result;
        }

        public async Task<List<SessionDetailViewModel>> GetExcerciseScheduleByGymerIDV2(Guid GymerId)
        {
            //Tim GymerPackageID
            var packageGymers = await _context.PackageGymers.Where(a => a.GymerId == GymerId && a.Status != "Đã hoàn thành").ToListAsync();
            if (packageGymers.Count == 0) return null;

            //Tim ScheduleID
            var scheduleIDs = new List<Guid>();
            foreach (var item in packageGymers)
            {
                var schedules = await _context.ExerciseSchedules.Where(a => a.PackageGymerId == item.Id && a.IsDelete == false).ToListAsync();
                if (schedules.Count > 0)
                {
                    foreach (var item1 in schedules)
                    {
                        scheduleIDs.Add(item1.Id);
                    }
                }
            }

            //Tim sessions
            var sessions = new List<Session>();
            foreach (var item in scheduleIDs)
            {
                var tmp = await _context.Sessions.Where(a => a.ScheduleId == item).ToListAsync();
                if (tmp != null)
                {
                    foreach (var item1 in tmp)
                    {
                        sessions.Add(item1);
                    }
                }
            }

            //Thêm bài tập vào buổi tập
            var result = new List<SessionDetailViewModel>();
            foreach (var item in sessions)
            {
                var tmp = new SessionDetailViewModel();
                tmp.id = item.Id;
                tmp.ScheduleId = item.ScheduleId;
                tmp.From = item.From;
                tmp.To = item.To;
                tmp.Excercises = GetExcercises(item.Id);
                result.Add(tmp);
            }
            return result;
        }

        public async Task<List<SessionOfPTViewModel>> GetWorkingScheduleByPTIDAndDate(Guid PTId, DateTime date)
        {
            //Tim GymerPackageID
            var packageGymers = await _context.PackageGymers.Where(a => a.Ptid == PTId && a.Status != "Đã hoàn thành").ToListAsync();
            if (packageGymers.Count == 0) return null;

            //Tim ScheduleID
            var scheduleIDs = new List<Guid>();
            foreach (var item in packageGymers)
            {
                var schedules = await _context.ExerciseSchedules.Where(a => a.PackageGymerId == item.Id && a.IsDelete == false).ToListAsync();
                if (schedules.Count > 0)
                {
                    foreach (var item1 in schedules)
                    {
                        scheduleIDs.Add(item1.Id);
                    }
                }
            }

            //Tim sessions
            var sessions = new List<Session>();
            foreach (var item in scheduleIDs)
            {
                var tmp = await _context.Sessions.Where(a => a.ScheduleId == item && a.From.Date == date.Date).ToListAsync();
                if (tmp != null)
                {
                    foreach (var item1 in tmp)
                    {
                        sessions.Add(item1);
                    }
                }
            }

            //Thêm bài tập vào buổi tập
            var result = new List<SessionOfPTViewModel>();
            foreach (var item in sessions)
            {
                var GymerID = _context.ExerciseSchedules.FindAsync(item.ScheduleId).Result.GymerId;
                var tmp = new SessionOfPTViewModel();
                tmp.id = item.Id;
                tmp.GymerID = GymerID;
                tmp.GymerName = _context.Accounts.FindAsync(GymerID).Result.Fullname;
                tmp.From = item.From;
                tmp.To = item.To;
                tmp.Excercises = GetExcercises(item.Id);
                result.Add(tmp);
            }
            return result;
        }

        public async Task<List<SessionOfPTViewModel>> GetWorkingScheduleByPTID(Guid PTId)
        {
            //Tim GymerPackageID
            var packageGymers = await _context.PackageGymers.Where(a => a.Ptid == PTId && a.Status != "Đã hoàn thành").ToListAsync();
            if (packageGymers == null) return null;

            //Tim ScheduleID
            var scheduleIDs = new List<Guid>();
            foreach (var item in packageGymers)
            {
                var schedules = await _context.ExerciseSchedules.Where(a => a.PackageGymerId == item.Id && a.IsDelete == false).ToListAsync();
                if (schedules != null)
                {
                    foreach (var item1 in schedules)
                    {
                        scheduleIDs.Add(item1.Id);
                    }
                }
            }

            //Tim sessions
            var sessions = new List<Session>();
            foreach (var item in scheduleIDs)
            {
                var tmp = await _context.Sessions.Where(a => a.ScheduleId == item).ToListAsync();
                if (tmp != null)
                {
                    foreach (var item1 in tmp)
                    {
                        sessions.Add(item1);
                    }
                }
            }

            //Thêm bài tập vào buổi tập
            var result = new List<SessionOfPTViewModel>();
            foreach (var item in sessions)
            {
                var GymerID = _context.ExerciseSchedules.FindAsync(item.ScheduleId).Result.GymerId;
                var tmp = new SessionOfPTViewModel();
                tmp.id = item.Id;
                tmp.GymerID = GymerID;
                tmp.GymerName = _context.Accounts.FindAsync(GymerID).Result.Fullname;
                tmp.From = item.From;
                tmp.To = item.To;
                tmp.Excercises = GetExcercises(item.Id);
                result.Add(tmp);
            }
            return result;
        }

        public async Task<bool> CreateExcerciseScheduleV3(ExcerciseScheduleCreateViewModelV3 request)
        {
            var pg = await _context.PackageGymers.FindAsync(request.PackageGymerID);
            if (pg == null) return false;

            var checkS = await _context.ExerciseSchedules.SingleOrDefaultAsync(a => a.PackageGymerId == request.PackageGymerID);
            if (checkS != null) return false;

            var id = Guid.NewGuid();
            var schedule = new ExerciseSchedule
            {
                Id = id,
                GymerId = pg.GymerId,
                Ptid = (Guid)pg.Ptid,
                PackageGymerId = request.PackageGymerID,
                From = request.From,
                To = request.To,
                IsDelete = false
            };
            await _context.ExerciseSchedules.AddAsync(schedule);

            //Tao session
            foreach (var item in request.listSession)
            {
                var check = await _context.Sessions.Where(s => s.ScheduleId.Equals(id) && s.From.Equals(item)).ToListAsync();
                if (check.Count > 0) return false;
                Guid Sid = Guid.NewGuid();
                Session session = new Session()
                {
                    Id = Guid.NewGuid(),
                    ScheduleId = id,
                    From = item,
                    To = item.AddHours(2),
                    IsDelete = false
                };
                //(Sid, id, item, item.AddHours(1), false);

                try
                {
                    await _context.Sessions.AddAsync(session);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Invalid Data");
                    return false;
                }
            }
            await _context.SaveChangesAsync();
            return true;
        }

        /*private async Task<bool> createSession(Guid scheduleID, DateTime from)
        {
            var check = await _context.Sessions.Where(s => s.ScheduleId.Equals(scheduleID) && s.From.Equals(from)).ToListAsync();
            if (check != null) return false;
            Guid id = Guid.NewGuid();
            //Session session = new Session(id, scheduleID, from.AddHours(9), from.AddHours(10), false);
            Session session = new Session()
            {
                Id = Guid.NewGuid(),
                ScheduleId = id,
                From = from.AddHours(9),
                To = from.AddHours(11),
                IsDelete = false
            };

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
        }*/

        public async Task<List<SessionDetailViewModel>> GetExcerciseScheduleByPackageGymerIDAndDate(Guid packageGymerID, DateTime date)
        {
            var schedule = await _context.ExerciseSchedules.SingleOrDefaultAsync(a => a.PackageGymerId == packageGymerID && a.IsDelete == false);

            //Tim sessions
            var sessions = await _context.Sessions.Where(a => a.ScheduleId == schedule.Id && a.From.Date == date.Date).ToListAsync();
            if (sessions == null) return null;

            //Thêm bài tập vào buổi tập
            var result = new List<SessionDetailViewModel>();
            foreach (var item in sessions)
            {
                var viewModel = new SessionDetailViewModel();
                viewModel.id = item.Id;
                viewModel.ScheduleId = item.ScheduleId;
                viewModel.From = item.From;
                viewModel.To = item.To;
                viewModel.Excercises = GetExcercises(item.Id);
                result.Add(viewModel);
            }
            return result;
        }

        public async Task<List<SessionDateViewModel>> GetExcerciseScheduleByPackageGymerID(Guid packageGymerID)
        {
            var schedule = await _context.ExerciseSchedules.SingleOrDefaultAsync(a => a.PackageGymerId == packageGymerID && a.IsDelete == false);

            //Tim sessions
            var sessions = await _context.Sessions.Where(a => a.ScheduleId == schedule.Id && a.IsDelete == false).ToListAsync();
            if (sessions.IsNullOrEmpty()) return null;

            //Thêm bài tập vào buổi tập
            var result = new List<SessionDateViewModel>();
            foreach (var item in sessions)
            {
                var viewModel = new SessionDateViewModel();
                viewModel.id = item.Id;
                viewModel.ScheduleId = item.ScheduleId;
                viewModel.Date = item.From.Date;
                result.Add(viewModel);
            }
            return result;
        }
    }
}
