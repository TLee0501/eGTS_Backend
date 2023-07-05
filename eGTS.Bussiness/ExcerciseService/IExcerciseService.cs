using eGTS_Backend.Data.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGTS.Bussiness.ExcerciseService
{
    public interface IExcerciseService
    {
        Task<bool> CreateExcercise(ExcerciseCreateViewModel model);
        Task<bool> UpdateExcercise(Guid id, ExcerciseUpdateViewModel model);
        Task<bool> DeleteExcercise(Guid id);
        Task<List<ExcerciseViewModel>> GetExcerciseByPTID(Guid PTID);
        Task<ExcerciseViewModel> GetExcerciseByID(Guid PTID);
        Task<List<ExcerciseViewModel>> GetAllExcercise();
        Task<List<ExcerciseViewModel>> GetExcerciseByName();
    }
}
