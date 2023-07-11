using eGTS_Backend.Data.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGTS.Bussiness.ExcerciseScheduleService
{
    public class IExcerciseScheduleService
    {

        Task<List<ExScheduleViewModel>> GetExcerciseScheduleWithConditions();

    }
}
