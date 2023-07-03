using eGTS_Backend.Data.Models;
using eGTS_Backend.Data.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace eGTS.Bussiness.FoodAndSupplimentService
{
    public class FoodAndSupplimentService : IFoodAndSupplimentService
    {
        private readonly EGtsContext _context;
        public FoodAndSupplimentService(EGtsContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateFoodAndSuppliment(FoodAndSupplimentCreateViewModel request)
        {
            Guid id = Guid.NewGuid();
            var createdate = DateTime.Now;
            FoodAndSuppliment foodAndSuppliment = new FoodAndSuppliment(id, request.Neid, request.Name, request.Ammount, request.Calories, createdate);
            _context.FoodAndSuppliments.Add(foodAndSuppliment);
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
                return false;
            }
        }

        public async Task<bool> DeleteFoodAndSuppliment(Guid id)
        {
            var foodAndSuppliment = await _context.FoodAndSuppliments.FindAsync(id);
            if (foodAndSuppliment == null) return false;
            else
            {
                _context.FoodAndSuppliments.Remove(foodAndSuppliment);
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<FoodAndSupplimentViewModel> GetFoodAndSuppliment(Guid id)
        {
            FoodAndSupplimentViewModel result = new FoodAndSupplimentViewModel();
            if (_context.Packages == null)
            {
                return null;
            }
            var foodAndSuppliment = await _context.FoodAndSuppliments.FindAsync(id);
            if (foodAndSuppliment == null) return null;
            else
            {
                result.Id = foodAndSuppliment.Id;
                result.Neid = foodAndSuppliment.Neid;
                result.Name = foodAndSuppliment.Name;
                result.Ammount = foodAndSuppliment.Ammount;
                result.Calories = foodAndSuppliment.Calories;
                result.CreateDate = foodAndSuppliment.CreateDate;
                return result;
            }
        }

        public async Task<List<FoodAndSupplimentViewModel>> GetFoodAndSuppliments()
        {
            var foodAndSuppliments = await _context.FoodAndSuppliments.ToListAsync();

            if (foodAndSuppliments.Count > 0)
            {
                List<FoodAndSupplimentViewModel> result = new List<FoodAndSupplimentViewModel>();
                foreach (var foodAndSuppliment in foodAndSuppliments)
                {
                    var viewModel = new FoodAndSupplimentViewModel();
                    viewModel.Id = foodAndSuppliment.Id;
                    viewModel.Neid = foodAndSuppliment.Neid;
                    viewModel.Name = foodAndSuppliment.Name;
                    viewModel.Ammount = foodAndSuppliment.Ammount;
                    viewModel.Calories = foodAndSuppliment.Calories;
                    viewModel.CreateDate = foodAndSuppliment.CreateDate;
                    result.Add(viewModel);
                }
                return result;
            }
            return null;
        }

        public async Task<List<FoodAndSupplimentViewModel>> GetFoodAndSupplimentsBYNE(Guid id)
        {
            var foodAndSuppliments = await _context.FoodAndSuppliments.Where(a => a.Neid.Equals(id)).ToListAsync();

            if (foodAndSuppliments.Count > 0)
            {
                List<FoodAndSupplimentViewModel> result = new List<FoodAndSupplimentViewModel>();
                foreach (var foodAndSuppliment in foodAndSuppliments)
                {
                    var viewModel = new FoodAndSupplimentViewModel();
                    viewModel.Id = foodAndSuppliment.Id;
                    viewModel.Neid = foodAndSuppliment.Neid;
                    viewModel.Name = foodAndSuppliment.Name;
                    viewModel.Ammount = foodAndSuppliment.Ammount;
                    viewModel.Calories = foodAndSuppliment.Calories;
                    viewModel.CreateDate = foodAndSuppliment.CreateDate;
                    result.Add(viewModel);
                }
                return result;
            }
            return null;
        }

        public async Task<bool> UpdateFoodAndSuppliment(FoodAndSupplimentUpdateViewModel request)
        {
            var inDatabase = await _context.FoodAndSuppliments.FindAsync(request.Id);
            _context.ChangeTracker.Clear();
            FoodAndSuppliment foodAndSuppliment = new FoodAndSuppliment(request.Id, inDatabase.Neid, request.Name, request.Ammount, request.Calories, inDatabase.CreateDate);
            _context.Entry(foodAndSuppliment).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
                return false;
            }
        }
    }
}
