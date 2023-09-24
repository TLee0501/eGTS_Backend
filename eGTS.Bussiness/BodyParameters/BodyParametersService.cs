using eGTS_Backend.Data.Models;
using eGTS_Backend.Data.ViewModel;
using Google.Apis.Auth.OAuth2;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eGTS.Bussiness.BodyParameters
{
    public class BodyParametersService : IBodyParametersService
    {

        private readonly EGtsContext _context;
        private readonly ILogger<IBodyParametersService> _logger;

        public BodyParametersService(EGtsContext context, ILogger<IBodyParametersService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> CreateBodyParameters(BodyPerameterCreateViewModel model)
        {
            Guid id = Guid.NewGuid();

            var account = _context.Accounts.Find(model.GymerId);

            if (account != null && account.Role.Equals("Gymer") && account.IsDelete == false)
            {
                var bmi = BMIcalculator(model.Weight, model.Height);
                var BPS = new BodyPerameter
                {
                    Id = Guid.NewGuid(),
                    GymerId = model.GymerId,
                    Goal = model.Goal,
                    Weight = model.Weight,
                    Height = model.Height,
                    Bmi = bmi,
                    Bone = model.Bone,
                    Fat = model.Fat,
                    Muscle = model.Muscle,
                    CreateDate = DateTime.Now,
                    IsDelete = false
                };
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
            return false;
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
                    model.GymerId = BP.GymerId;
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

        public async Task<bool> DeleteBodyParameters(Guid id)
        {
            var BP = _context.BodyPerameters.Find(id);
            if (BP == null)
                return false;

            if (BP.IsDelete != true)
                BP.IsDelete = true;

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

        public async Task<List<BodyPerameterViewModel>> GetBodyParametersByGymerID(Guid GymerID)
        {
            List<BodyPerameterViewModel> resultList = new List<BodyPerameterViewModel>();
            var BPs = _context.BodyPerameters.Where(b => b.GymerId.Equals(GymerID));

            if (BPs != null)
            {
                foreach (var BP in BPs)
                {
                    if (BP.IsDelete == false)
                    {
                        BodyPerameterViewModel model = new BodyPerameterViewModel();
                        model.Id = BP.Id;
                        model.GymerId = BP.GymerId;
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
                }
                return resultList;
            }
            return null;
        }

        public async Task<BodyPerameterViewModel> GetBodyParametersByID(Guid id)
        {
            var BP = await _context.BodyPerameters.FindAsync(id);

            if (BP != null)
            {
                BodyPerameterViewModel model = new BodyPerameterViewModel();
                model.Id = BP.Id;
                model.GymerId = BP.GymerId;
                model.Goal = BP.Goal;
                model.Weight = BP.Weight;
                model.Height = BP.Height;
                model.Bmi = BP.Bmi;
                model.Bone = BP.Bone;
                model.Fat = BP.Fat;
                model.Muscle = BP.Muscle;
                model.CreateDate = BP.CreateDate;
                model.IsDelete = BP.IsDelete;

                return model;
            }
            return null;

        }

        public async Task<bool> UpdateBodyParameters(Guid id, BodyPerameterUpdateViewModel model)
        {
            var BP = await _context.BodyPerameters.FindAsync(id);
            if (BP == null)
                return false;
            if (!(model.Goal.Equals("")))
                BP.Goal = model.Goal;
            if (model.Weight > 0)
                BP.Weight = model.Weight;
            if (model.Height > 0)
                BP.Height = model.Height;
            BP.Bmi = BMIcalculator(BP.Weight, BP.Height);
            if (model.Bone > 0)
                BP.Bone = model.Bone;
            if (model.Fat > 0)
                BP.Fat = model.Fat;
            if (model.Muscle > 0)
                BP.Muscle = model.Muscle;
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

        public double? BMIcalculator(double? weight, double? height)
        {
            if (weight == null || height == null || weight <= 0 || height <= 0)
            {
                return 0;
            }
            return weight / ((height / 100) * (height / 100));// Height is in CM
        }

        public async Task<BodyPerameterViewModel> GetBodyParameterByGymerID(Guid GymerId)
        {
            List<BodyPerameterViewModel> resultList = new List<BodyPerameterViewModel>();
            var BP = await _context.BodyPerameters
                .Where(a => a.GymerId == GymerId)
                .OrderByDescending(b => b.CreateDate)
                .FirstOrDefaultAsync();

            if (BP != null)
            {
                BodyPerameterViewModel model = new BodyPerameterViewModel()
                {
                    Id = BP.Id,
                    GymerId = GymerId,
                    Goal = BP.Goal,
                    Weight = BP.Weight,
                    Height = BP.Height,
                    Bmi = BP.Bmi,
                    Bone = BP.Bone,
                    Fat = BP.Fat,
                    Muscle = BP.Muscle,
                    CreateDate = BP.CreateDate,
                    IsDelete = BP.IsDelete,
                };
                resultList.Add(model);
                return model;
            }
            return null;
        }
    }
}
