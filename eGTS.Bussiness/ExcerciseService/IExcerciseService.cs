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
        Task<bool> UpdateExcercise(Guid id, ExcerciseUpdateViewModel request);
        Task<bool> DeleteExcercise(Guid id);
        Task<bool> CreateExcerciseType(ExcerciseTypeCreateViewModel model);
        Task<bool> UpdateExcerciseType(Guid id, ExcerciseTypeUpdateViewModel request);
        Task<bool> DeleteExcerciseType(Guid id);
        Task<bool> CreateExcerciseInType(ExcerciseInTypeCreateViewModel model);
        Task<bool> UpdateExcerciseInType(Guid id, ExcerciseInTypeUpdateViewModel request);
        Task<bool> DeleteExcerciseInType(Guid id);
        Task<List<ExcerciseViewModel>> GetExcerciseByPTID(Guid PTID);
        Task<ExcerciseViewModel> GetExcerciseByID(Guid PTID);
        Task<List<ExcerciseViewModel>> GetExcerciseByType(Guid TypeID);
        Task<List<ExcerciseViewModel>> GetAllExcercise();
        Task<List<ExcerciseViewModel>> GetExcerciseByName(string Name);
        Task<List<ExcerciseTypeViewModel>> GetExcerciseTypeByPTID(Guid PTID);
        Task<ExcerciseTypeViewModel> GetExcerciseTypeByID(Guid PTID);
        Task<List<ExcerciseTypeViewModel>> GetExcerciseTypeByName(string Name);
        Task<List<ExcerciseInTypeViewModel>> GetAllExcerciseInType();
    }
}
