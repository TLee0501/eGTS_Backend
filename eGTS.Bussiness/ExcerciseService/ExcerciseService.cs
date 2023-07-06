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

namespace eGTS.Bussiness.ExcerciseService
{
    public class ExcerciseService : IExcerciseService
    {
        private readonly EGtsContext _context;
        private readonly ILogger<IAccountService> _logger;

        public ExcerciseService(EGtsContext context, ILogger<IAccountService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> CreateExcercise(ExcerciseCreateViewModel model)
        {
            Guid id = Guid.NewGuid();
            DateTime createDate = DateTime.Now;
            Excercise excercise = new Excercise(id, model.Ptid, model.Name, model.Description, model.Video, createDate);
            try
            {
                await _context.Excercises.AddAsync(excercise);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Invalid data.");
                return false;
            }

        }

        public async Task<bool> CreateExcerciseInType(ExcerciseInTypeCreateViewModel model)
        {
            Guid id = Guid.NewGuid();
            ExerciseInExerciseType EIT = new ExerciseInExerciseType(id, model.ExerciseTypeId, model.ExerciseId);
            try
            {
                await _context.ExerciseInExerciseTypes.AddAsync(EIT);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Invalid data.");
                return false;
            }
        }

        public async Task<bool> CreateExcerciseType(ExcerciseTypeCreateViewModel model)
        {
            Guid id = Guid.NewGuid();
            DateTime createDate = DateTime.Now;
            ExcerciseType excerciseType = new ExcerciseType(id, model.Name, model.Ptid);
            try
            {
                await _context.ExcerciseTypes.AddAsync(excerciseType);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Invalid data.");
                return false;
            }
        }

        public async Task<bool> DeleteExcercise(Guid id)
        {
            if (_context.Excercises == null)
                return false;

            var excercise = await _context.Excercises.FindAsync(id);
            if (excercise != null)
            {
                _context.Excercises.Remove(excercise);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteExcerciseInType(Guid id)
        {
            if (_context.ExerciseInExerciseTypes == null)
                return false;

            var excerciseInType = await _context.ExerciseInExerciseTypes.FindAsync(id);
            if (excerciseInType != null)
            {
                _context.ExerciseInExerciseTypes.Remove(excerciseInType);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteExcerciseType(Guid id)
        {
            if (_context.ExcerciseTypes == null)
                return false;

            var excerciseType = await _context.ExcerciseTypes.FindAsync(id);
            if (excerciseType != null)
            {
                _context.ExcerciseTypes.Remove(excerciseType);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<ExcerciseViewModel>> GetAllExcercise()
        {
            List<ExcerciseViewModel> resultList = new List<ExcerciseViewModel>();

            var excercises = await _context.Excercises.ToListAsync();

            foreach (Excercise e in excercises)
            {
                ExcerciseViewModel result = new ExcerciseViewModel();
                result.id = e.Id;
                result.Ptid = e.Ptid;
                result.Name = e.Name;
                result.Description = e.Description;
                result.Video = e.Video;
                result.CreateDate = e.CreateDate;
                resultList.Add(result);
            }

            if (resultList.Count > 0)
                return resultList;
            else
                return null;

        }

        public async Task<List<ExcerciseInTypeViewModel>> GetAllExcerciseInType()
        {
            List<ExcerciseInTypeViewModel> resultList = new List<ExcerciseInTypeViewModel>();

            var excercisInType = await _context.ExerciseInExerciseTypes.ToListAsync();

            foreach (ExerciseInExerciseType e in excercisInType)
            {
                ExcerciseInTypeViewModel result = new ExcerciseInTypeViewModel();
                result.Id = e.Id;
                result.ExerciseTypeId = e.ExerciseTypeId;
                result.ExerciseId = e.ExerciseId;
                resultList.Add(result);
            }

            if (resultList.Count > 0)
                return resultList;
            else
                return null;
        }

        public async Task<List<ExcerciseTypeViewModel>> GetAllExcerciseType()
        {
            List<ExcerciseTypeViewModel> resultList = new List<ExcerciseTypeViewModel>();

            var excerciseType = await _context.ExcerciseTypes.ToListAsync();

            foreach (ExcerciseType e in excerciseType)
            {
                ExcerciseTypeViewModel result = new ExcerciseTypeViewModel();
                result.Id = e.Id;
                result.Ptid = e.Ptid;
                result.Name = e.Name;
                resultList.Add(result);
            }

            if (resultList.Count > 0)
                return resultList;
            else
                return null;
        }

        public async Task<ExcerciseViewModel> GetExcerciseByID(Guid ID)
        {

            var excercise = await _context.Excercises.FindAsync(ID);
            if (excercise == null)
                return null;
            else
            {
                ExcerciseViewModel result = new ExcerciseViewModel();
                result.id = excercise.Id;
                result.Ptid = excercise.Ptid;
                result.Name = excercise.Name;
                result.Description = excercise.Description;
                result.Video = excercise.Video;
                result.CreateDate = excercise.CreateDate;

                return result;
            }

        }

        public async Task<List<ExcerciseViewModel>> GetExcerciseByName(string Name)
        {
            List<ExcerciseViewModel> resultList = new List<ExcerciseViewModel>();
            var excercises = await _context.Excercises.Where(e => e.Name.Contains(Name)).ToListAsync();
            foreach (Excercise e in excercises)
            {
                ExcerciseViewModel result = new ExcerciseViewModel();
                result.id = e.Id;
                result.Ptid = e.Ptid;
                result.Name = e.Name;
                result.Description = e.Description;
                result.Video = e.Video;
                result.CreateDate = e.CreateDate;
                resultList.Add(result);
            }

            if (resultList.Count > 0)
                return resultList;
            else
                return null;
        }

        public async Task<List<ExcerciseViewModel>> GetExcerciseByPTID(Guid PTID)
        {
            List<ExcerciseViewModel> resultList = new List<ExcerciseViewModel>();
            var excercises = await _context.Excercises.Where(e => e.Ptid == PTID).ToListAsync();
            foreach (Excercise e in excercises)
            {
                ExcerciseViewModel result = new ExcerciseViewModel();
                result.id = e.Id;
                result.Ptid = e.Ptid;
                result.Name = e.Name;
                result.Description = e.Description;
                result.Video = e.Video;
                result.CreateDate = e.CreateDate;
                resultList.Add(result);
            }

            if (resultList.Count > 0)
                return resultList;
            else
                return null;
        }

        public async Task<List<ExcerciseViewModel>> GetExcerciseByType(Guid TypeID)
        {
            List<ExcerciseViewModel> resultList = new List<ExcerciseViewModel>();
            var EITList = await _context.ExerciseInExerciseTypes.Where(e => e.ExerciseTypeId == TypeID).ToListAsync();
            foreach (ExerciseInExerciseType EIT in EITList)
            {
                var excercise = await _context.Excercises.FindAsync(EIT.ExerciseId);
                ExcerciseViewModel result = new ExcerciseViewModel();
                result.id = excercise.Id;
                result.Ptid = excercise.Ptid;
                result.Name = excercise.Name;
                result.Description = excercise.Description;
                result.Video = excercise.Video;
                result.CreateDate = excercise.CreateDate;
                resultList.Add(result);
            }
            if (resultList.Count > 0)
                return resultList;
            else
                return null;
        }

        public async Task<ExcerciseTypeViewModel> GetExcerciseTypeByID(Guid id)
        {
            var excerciseType = await _context.ExcerciseTypes.FindAsync(id);
            if (excerciseType == null)
                return null;
            else
            {
                ExcerciseTypeViewModel result = new ExcerciseTypeViewModel();
                result.Id = excerciseType.Id;
                result.Ptid = excerciseType.Ptid;
                result.Name = excerciseType.Name;

                return result;
            }
        }

        public async Task<List<ExcerciseTypeViewModel>> GetExcerciseTypeByName(string Name)
        {
            List<ExcerciseTypeViewModel> resultList = new List<ExcerciseTypeViewModel>();
            var excerciseTypes = await _context.ExcerciseTypes.Where(e => e.Name.Contains(Name)).ToListAsync();
            foreach (ExcerciseType e in excerciseTypes)
            {
                ExcerciseTypeViewModel result = new ExcerciseTypeViewModel();
                result.Id = e.Id;
                result.Ptid = e.Ptid;
                result.Name = e.Name;
                resultList.Add(result);
            }

            if (resultList.Count > 0)
                return resultList;
            else
                return null;
        }

        public async Task<List<ExcerciseTypeViewModel>> GetExcerciseTypeByPTID(Guid PTID)
        {
            List<ExcerciseTypeViewModel> resultList = new List<ExcerciseTypeViewModel>();
            var excercises = await _context.ExcerciseTypes.Where(e => e.Ptid == PTID).ToListAsync();
            foreach (ExcerciseType e in excercises)
            {
                ExcerciseTypeViewModel result = new ExcerciseTypeViewModel();
                result.Id = e.Id;
                result.Ptid = e.Ptid;
                result.Name = e.Name;
                resultList.Add(result);
            }

            if (resultList.Count > 0)
                return resultList;
            else
                return null;
        }

        public async Task<bool> UpdateExcercise(Guid id, ExcerciseUpdateViewModel request)
        {
            var excercise = await _context.Excercises.FindAsync(id);
            if (excercise == null)
                return false;
            if (!request.Name.Equals(""))
                excercise.Name = request.Name;
            if (!request.Description.Equals(""))
                excercise.Description = request.Description;
            if (!request.Video.Equals(""))
                excercise.Video = request.Video;

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

        public async Task<bool> UpdateExcerciseInType(Guid id, ExcerciseInTypeUpdateViewModel request)
        {
            var excerciseInType = await _context.ExerciseInExerciseTypes.FindAsync(id);
            if (excerciseInType == null)
                return false;
            if (!request.ExerciseTypeId.Equals(""))
                excerciseInType.ExerciseTypeId = request.ExerciseTypeId;
            if (!request.ExerciseId.Equals(""))
                excerciseInType.ExerciseId = request.ExerciseId;

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

        public async Task<bool> UpdateExcerciseType(Guid id, ExcerciseTypeUpdateViewModel request)
        {
            var excerciseType = await _context.ExcerciseTypes.FindAsync(id);
            if (excerciseType == null)
                return false;
            if (!request.Name.Equals(""))
                excerciseType.Name = request.Name;

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
    }
}
