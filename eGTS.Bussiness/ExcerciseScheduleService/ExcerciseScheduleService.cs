using Azure.Core;
using eGTS.Bussiness.AccountService;
using eGTS_Backend.Data.Models;
using eGTS_Backend.Data.ViewModel;
using Google.Api.Gax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<bool> CreateExcerciseSchedule(ExScheduleCreateViewModel model)
        {
            Guid id = Guid.NewGuid();
            ExcerciseSchedule exSchedule = new ExcerciseSchedule(id, model.GymerId, model.Ptid, model.PackageGymerId, model.From, model.To, false);

            try
            {
                await _context.ExcerciseSchedules.AddAsync(exSchedule);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Invalid data.");
                return false;
            }

        }

        public async Task<List<ExScheduleViewModel>> DEBUGGetAllExcerciseSchedule(bool? isExpired)
        {
            var resultList = new List<ExScheduleViewModel>();

            if (isExpired == false)
            {
                var ExScheduleList = await _context.ExcerciseSchedules.Where(e => e.To > DateTime.Now).ToListAsync();
                foreach (ExcerciseSchedule exSchedule in ExScheduleList)
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
                var ExScheduleList = await _context.ExcerciseSchedules.Where(e => e.To <= DateTime.Now).ToListAsync();
                foreach (ExcerciseSchedule exSchedule in ExScheduleList)
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
                var ExScheduleList = await _context.ExcerciseSchedules.ToListAsync();
                foreach (ExcerciseSchedule exSchedule in ExScheduleList)
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
            var exSchedule = await _context.ExcerciseSchedules.FindAsync(id);
            if (exSchedule != null)
            {
                _context.ExcerciseSchedules.Remove(exSchedule);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<ExScheduleViewModel> GetExcerciseScheduleByID(Guid id)
        {
            var ExS = await _context.ExcerciseSchedules.FindAsync(id);
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
                var ExScheduleList = await _context.ExcerciseSchedules.Where(e => e.To > DateTime.Now && e.Ptid.Equals(PTID)).ToListAsync();
                foreach (ExcerciseSchedule exSchedule in ExScheduleList)
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
                var ExScheduleList = await _context.ExcerciseSchedules.Where(e => e.To <= DateTime.Now && e.Ptid.Equals(PTID)).ToListAsync();
                foreach (ExcerciseSchedule exSchedule in ExScheduleList)
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
                var ExScheduleList = await _context.ExcerciseSchedules.Where(e => e.Ptid.Equals(PTID)).ToListAsync();
                foreach (ExcerciseSchedule exSchedule in ExScheduleList)
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
                var ExScheduleList = await _context.ExcerciseSchedules.Where(e => e.To > DateTime.Now && e.GymerId.Equals(GymerID)).ToListAsync();
                foreach (ExcerciseSchedule exSchedule in ExScheduleList)
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
                var ExScheduleList = await _context.ExcerciseSchedules.Where(e => e.To <= DateTime.Now && e.GymerId.Equals(GymerID)).ToListAsync();
                foreach (ExcerciseSchedule exSchedule in ExScheduleList)
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
                var ExScheduleList = await _context.ExcerciseSchedules.Where(e => e.GymerId.Equals(GymerID)).ToListAsync();
                foreach (ExcerciseSchedule exSchedule in ExScheduleList)
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
            var exSchedule = await _context.ExcerciseSchedules.FindAsync(id);
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
            var exSchedule = await _context.ExcerciseSchedules.FindAsync(id);
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

        public async Task<bool> CreateExcerciseScheduleV2(Guid packageGymerID)
        {
            var pg = await _context.PackageGymers.FindAsync(packageGymerID);
            if (pg == null) return false;

            var id = Guid.NewGuid();
            var schedule = new ExcerciseSchedule(id, pg.GymerId, (Guid)pg.Ptid, packageGymerID, DateTime.Now, DateTime.Now.AddMonths(1), false);
            await _context.ExcerciseSchedules.AddAsync(schedule);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<SessionDetailViewModel>> GetExcerciseScheduleByGymerIDAndDate(Guid GymerId, DateTime date)
        {
            //Tim GymerPackageID
            var packageGymers = await _context.PackageGymers.Where(a => a.GymerId == GymerId && a.Status != "Done").ToListAsync();
            if (packageGymers.Count == 0) return null;

            //Tim ScheduleID
            var scheduleIDs = new List<Guid>();
            foreach (var item in packageGymers)
            {
                var schedules = await _context.ExcerciseSchedules.Where(a => a.PackageGymerId == item.Id && a.IsDelete == false).ToListAsync();
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
                var tmp = await _context.Sessions.Where(a => a.ScheduleId == item && a.DateAndTime.Date == date.Date).ToListAsync();
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
                tmp.DateAndTime = item.DateAndTime;
                tmp.Excercises = GetExcercises(item.Id);
                result.Add(tmp);
            }
            return result;
        }

        private List<ExcerciseViewModel> GetExcercises(Guid id)
        {
            var result = new List<ExcerciseViewModel>();

            var Exs = _context.ExserciseInSessions.Where(a => a.SessionId == id).ToList();
            if (Exs.Count == 0) return null;

            foreach (var item in Exs)
            {
                var excercise = _context.Excercises.SingleOrDefault(a => a.Id == item.ExerciseId && a.IsDelete == false);
                if(excercise != null)
                {
                    var viewModel = new ExcerciseViewModel();
                    viewModel.id = excercise.Id;
                    viewModel.Ptid = excercise.Ptid;
                    viewModel.Name = excercise.Name;
                    if (excercise.Description != null) viewModel.Description = excercise.Description;
                    if (excercise.Video != null) viewModel.Video = excercise.Video;
                    viewModel.CreateDate = excercise.CreateDate;
                    result.Add(viewModel);
                }
            }
            return result;
        }
    }
}
