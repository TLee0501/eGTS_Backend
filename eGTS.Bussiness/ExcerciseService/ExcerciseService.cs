using eGTS.Bussiness.AccountService;
using eGTS_Backend.Data.Models;
using eGTS_Backend.Data.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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
            DateTime createDate = DateTime.Now;
            Exercise excercise = new Exercise
            {
                Id = Guid.NewGuid(),
                Ptid = model.Ptid,
                Name = model.Name,
                Description = model.Description,
                Video = model.Video,
                CreateDate = createDate,
                IsDelete = false,
                RepTime = model.RepTime,
                UnitOfMeasurement = model.UnitOfMeasurement,
                CalorieCumsumption = model.CalorieCumsumption
            };
            try
            {
                await _context.Exercises.AddAsync(excercise);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Invalid data.");
                return false;
            }

        }

        public async Task<bool> DeleteExcercisePEMANENT(Guid id)
        {
            if (_context.Exercises == null)
                return false;

            var excercise = await _context.Exercises.FindAsync(id);
            if (excercise != null)
            {
                _context.Exercises.Remove(excercise);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteExcercise(Guid id)
        {
            var excercise = await _context.Exercises.FindAsync(id);

            excercise.IsDelete = true;

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

        public async Task<List<ExcerciseViewModel>> GetAllExcercise()
        {
            List<ExcerciseViewModel> resultList = new List<ExcerciseViewModel>();

            var excercises = await _context.Exercises.ToListAsync();

            foreach (Exercise e in excercises)
            {
                ExcerciseViewModel result = new ExcerciseViewModel();
                result.id = e.Id;
                result.Ptid = e.Ptid;
                result.Name = e.Name;
                result.Description = e.Description;
                result.Video = e.Video;
                result.CreateDate = e.CreateDate;
                result.IsDelete = e.IsDelete;
                result.CalorieCumsumption = e.CalorieCumsumption;
                result.RepTime = e.RepTime;
                result.UnitOfMeasurement = e.UnitOfMeasurement;
                resultList.Add(result);
            }

            if (resultList.Count > 0)
                return resultList;
            else
                return null;

        }

        public async Task<ExcerciseViewModel> GetExcerciseByID(Guid ID)
        {

            var excercise = await _context.Exercises.FindAsync(ID);
            if (excercise == null || excercise.IsDelete == true)
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
                result.IsDelete = excercise.IsDelete;
                result.CalorieCumsumption = excercise.CalorieCumsumption;
                result.RepTime = excercise.RepTime;
                result.UnitOfMeasurement = excercise.UnitOfMeasurement;

                return result;
            }

        }

        public async Task<List<ExcerciseViewModel>> GetExcerciseByName(string Name)
        {
            List<ExcerciseViewModel> resultList = new List<ExcerciseViewModel>();
            var excercises = await _context.Exercises.Where(e => e.Name.Contains(Name) && e.IsDelete == false).ToListAsync();
            foreach (Exercise e in excercises)
            {
                ExcerciseViewModel result = new ExcerciseViewModel();
                result.id = e.Id;
                result.Ptid = e.Ptid;
                result.Name = e.Name;
                result.Description = e.Description;
                result.Video = e.Video;
                result.CreateDate = e.CreateDate;
                result.IsDelete = e.IsDelete;
                result.CalorieCumsumption = e.CalorieCumsumption;
                result.RepTime = e.RepTime;
                result.UnitOfMeasurement = e.UnitOfMeasurement;
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
            var excercises = await _context.Exercises.Where(e => e.Ptid == PTID && e.IsDelete == false).ToListAsync();
            foreach (Exercise e in excercises)
            {
                ExcerciseViewModel result = new ExcerciseViewModel();
                result.id = e.Id;
                result.Ptid = e.Ptid;
                result.Name = e.Name;
                result.Description = e.Description;
                result.Video = e.Video;
                result.CreateDate = e.CreateDate;
                result.IsDelete = e.IsDelete;
                result.CalorieCumsumption = e.CalorieCumsumption;
                result.RepTime = e.RepTime;
                result.UnitOfMeasurement = e.UnitOfMeasurement;
                resultList.Add(result);
            }

            if (resultList.Count > 0)
                return resultList;
            else
                return null;
        }
        public async Task<bool> UpdateExcercise(Guid id, ExcerciseUpdateViewModel request)
        {
            var excercise = await _context.Exercises.FindAsync(id);
            if (excercise == null || excercise.IsDelete == true)
                return false;
            if (!request.Description.Equals(""))
                excercise.Description = request.Description;
            if (!request.Video.Equals(""))
                excercise.Video = request.Video;
            excercise.IsDelete = request.IsDelete;
            excercise.CalorieCumsumption = request.CalorieCumsumption;
            excercise.RepTime = request.RepTime;
            excercise.UnitOfMeasurement = request.UnitOfMeasurement;

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
