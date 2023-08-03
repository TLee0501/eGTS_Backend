using eGTS_Backend.Data.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGTS.Bussiness.ExcerciseScheduleService
{
    public interface IExcerciseScheduleService
    {
        Task<bool> CreateExcerciseSchedule(ExScheduleCreateViewModel model);
        Task<bool> UpdateExcerciseSchedule(Guid id, ExScheduleUpdateViewModel request);
        Task<bool> DeleteExcerciseSchedulePERMANENT(Guid id);
        Task<bool> DeleteExcerciseSchedule(Guid id);
        Task<ExScheduleViewModel> GetExcerciseScheduleByID(Guid id);
        Task<List<ExScheduleViewModel>> DEBUGGetAllExcerciseSchedule(bool? isExpired);
        Task<List<ExScheduleViewModel>> GetExcerciseSchedulesWithPTID(Guid PTID, bool? isExpired);
        Task<List<ExScheduleViewModel>> GetExcerciseSchedulesWithGymerID(Guid GymerID, bool? isExpired);
        Task<bool> CreateExcerciseScheduleV2(Guid packageGymerID);


    }
}
