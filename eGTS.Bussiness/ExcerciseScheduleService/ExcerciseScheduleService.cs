using eGTS.Bussiness.AccountService;
using eGTS_Backend.Data.Models;
using eGTS_Backend.Data.ViewModel;
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
            ExcerciseSchedule exSchedule = new ExcerciseSchedule(id, model.GymerId, model.Ptid, model.PackageGymerId, model.From, model.To);

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

        public Task<bool> DeleteExcerciseSchedule(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
