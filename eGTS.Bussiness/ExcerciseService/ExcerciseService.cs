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
            Excercise excercise = new Excercise(id, model.Ptid, model.Name, model.Description, model.Video, DateTime.Now);
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

        public async Task<List<ExcerciseViewModel>> GetExcerciseByPTID(Guid PTID)
        {
            List<ExcerciseViewModel> resultList = new List<ExcerciseViewModel>();
            ExcerciseViewModel result = new ExcerciseViewModel();
            var excercises = await _context.Excercises.Where(e => e.Ptid == PTID).ToListAsync();
            foreach (Excercise e in excercises)
            {
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

        public Task<bool> UpdateExcercise(Guid id, ExcerciseUpdateViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
