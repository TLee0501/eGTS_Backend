using eGTS_Backend.Data.Models;
using eGTS_Backend.Data.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace eGTS.Bussiness.FoodAndSupplimentService
{
    public class FoodAndSupplimentService : IFoodAndSupplimentService
    {
        private readonly EGtsContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public FoodAndSupplimentService(EGtsContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<bool> CreateFoodAndSuppliment(FoodAndSupplimentCreateViewModel request)
        {
            var checkValid = await _context.FoodAndSupplements.SingleOrDefaultAsync(a => a.Neid == request.Neid && a.Ammount == request.Ammount && a.IsDelete == false);
            if (checkValid != null) return false;

            FoodAndSupplement foodAndSuppliment = new FoodAndSupplement()
            {
                Id = Guid.NewGuid(),
                Neid = request.Neid,
                Name = request.Name,
                Ammount = request.Ammount,
                UnitOfMesuament = request.UnitOfMesuament,
                Calories = request.Calories,
                CreateDate = DateTime.Now,
                IsDelete = false
            };
                //(id, request.Neid, request.Name, request.Ammount, request.UnitOfMesuament, request.Calories, createdate, false);
            await _context.FoodAndSupplements.AddAsync(foodAndSuppliment);
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteFoodAndSuppliment(Guid id)
        {
            var foodAndSuppliment = await _context.FoodAndSupplements.FindAsync(id);
            if (foodAndSuppliment == null) return false;
            else
            {
                foodAndSuppliment.IsDelete = true;
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
            var foodAndSuppliment = await _context.FoodAndSupplements.FindAsync(id);
            if (foodAndSuppliment == null) return null;
            else
            {
                result.Id = foodAndSuppliment.Id;
                result.Neid = foodAndSuppliment.Neid;
                result.Name = foodAndSuppliment.Name;
                result.Ammount = foodAndSuppliment.Ammount;
                result.Calories = foodAndSuppliment.Calories;
                result.UnitOfMesuament = foodAndSuppliment.UnitOfMesuament;
                result.CreateDate = foodAndSuppliment.CreateDate;
                result.IsDelete = foodAndSuppliment.IsDelete;
                return result;
            }
        }

        public async Task<List<FoodAndSupplimentViewModel>> GetFoodAndSuppliments()
        {
            var foodAndSuppliments = await _context.FoodAndSupplements.ToListAsync();

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
                    viewModel.UnitOfMesuament = foodAndSuppliment.UnitOfMesuament;
                    viewModel.Calories = foodAndSuppliment.Calories;
                    viewModel.CreateDate = foodAndSuppliment.CreateDate;
                    viewModel.IsDelete = foodAndSuppliment.IsDelete;
                    result.Add(viewModel);
                }
                return result;
            }
            return null;
        }

        public async Task<List<FoodAndSupplimentViewModel>> GetFoodAndSupplimentsBYNE(Guid id)
        {
            var foodAndSuppliments = await _context.FoodAndSupplements.Where(a => a.Neid.Equals(id) && a.IsDelete == false).ToListAsync();

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
                    viewModel.UnitOfMesuament = foodAndSuppliment.UnitOfMesuament;
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
            var inDatabase = await _context.FoodAndSupplements.FindAsync(request.Id);
            _context.ChangeTracker.Clear();
            FoodAndSupplement foodAndSuppliment = new FoodAndSupplement()
            {
                Id = request.Id,
                Neid = inDatabase.Neid,
                Name = request.Name,
                Ammount = request.Ammount,
                UnitOfMesuament = request.UnitOfMesuament,
                Calories = request.Calories,
                CreateDate = inDatabase.CreateDate,
                IsDelete = inDatabase.IsDelete
            };
                //(request.Id, inDatabase.Neid, request.Name, request.Ammount, request.UnitOfMesuament, request.Calories,
                //inDatabase.CreateDate, inDatabase.IsDelete);
            _context.Entry(foodAndSuppliment).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private Guid GetNEID()
        {
            Guid result = Guid.Empty;
            if (_httpContextAccessor.HttpContext is not null)
            {
                var nePhoneClaimRequest = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
                if (nePhoneClaimRequest != null)
                {
                    var accountRequest = _context.Accounts.SingleOrDefault(x => x.PhoneNo.Equals(nePhoneClaimRequest)).Id.ToString();
                    result = Guid.Parse(accountRequest);
                }
            }
            return result;
        }

        public async Task<List<FoodAndSupplimentViewModel>> SearchFoodAndSupplimentsByNameAndNE(Guid NEID, string FoodName)
        {
            var foodAndSuppliments = await _context.FoodAndSupplements.Where(a => a.Neid.Equals(NEID) && a.Name.Contains(FoodName) && a.IsDelete == false).ToListAsync();

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
                    viewModel.UnitOfMesuament = foodAndSuppliment.UnitOfMesuament;
                    viewModel.Calories = foodAndSuppliment.Calories;
                    viewModel.CreateDate = foodAndSuppliment.CreateDate;
                    result.Add(viewModel);
                }
                return result;
            }
            return null;
        }

        private Guid GetStaffID()
        {
            Guid result = Guid.Empty;
            var account = _context.Accounts.AsQueryable().Where(a => a.Role.Equals("Staff"));
            //var account = _context.Accounts.Where(a => a.Role.Equals("Staff"));

            result = account.First().Id;
            return result;
        }
    }
}
