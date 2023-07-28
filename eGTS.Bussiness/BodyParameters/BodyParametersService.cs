using eGTS.Bussiness.AccountService;
using eGTS.Bussiness.ExcerciseScheduleService;
using eGTS.Bussiness.ExcerciseService;
using eGTS_Backend.Data.Models;
using eGTS_Backend.Data.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGTS.Bussiness.BodyParameters
{
    public class BodyParametersService : IBodyParametersService
    {

        private readonly EGtsContext _context;
        private readonly IBodyParametersService _bodyParametersService;
        private readonly ILogger<IAccountService> _logger;

        public async Task<bool> CreateBodyParameters(BodyPerameterCreateViewModel model)
        {
            Guid id = Guid.NewGuid();
            BodyPerameter BPS = new BodyPerameter(id, model.GymerId, model.Goal,
                model.Weight, model.Height, model.Bmi, model.Bone, model.Fat, model.Muscle, DateTime.Now, false);
            try
            {
                await _context.BodyPerameters.AddAsync(BPS);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Invalid data.");
                return false;
            }

        }

        public async Task<List<BodyPerameterViewModel>> DEBUGGetAllBodyParameters()
        {
            var resultList = new List<BodyPerameterViewModel>();
            var BPs = await _context.BodyPerameters.ToListAsync();
            if (BPs.Count() > 0)
            {
                foreach (var BP in BPs)
                {
                    BodyPerameterViewModel model = new BodyPerameterViewModel();
                    model.Id = BP.Id;
                    model.GymerId = BP.Id;
                    model.Goal = BP.Goal;
                    model.Weight = BP.Weight;
                    model.Height = BP.Height;
                    model.Bmi = BP.Bmi;
                    model.Bone = BP.Bone;
                    model.Fat = BP.Fat;
                    model.Muscle = BP.Muscle;
                    model.CreateDate = BP.CreateDate;
                    model.IsDelete = BP.IsDelete;
                    resultList.Add(model);
                }
                return resultList;
            }
            return null;
        }

        public Task<bool> DeleteBodyParameters(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteBodyParametersPERMANENT(Guid id)
        {
            var BP = await _context.BodyPerameters.FindAsync(id);
            if (BP != null)
            {
                _context.BodyPerameters.Remove(BP);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public Task<List<BodyPerameterViewModel>> GetBodyParametersByGymerID(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<BodyPerameterViewModel> GetBodyParametersByID(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateBodyParameters(Guid id, BodyPerameterUpdateViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
