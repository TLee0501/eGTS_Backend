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
            /*//Check exist name NE create
            Guid accountRequest = GetNEID();
            if (accountRequest == Guid.Empty)
            {
                return false;
            }
            var foodAndSupplimentsOfNE = await _context.FoodAndSuppliments.SingleOrDefaultAsync(a => a.Name.Equals(request.Name) && a.Neid.Equals(accountRequest));
            if (foodAndSupplimentsOfNE == null)
            {
                return false;
            }
            //Check exist name Staff create
            Guid staffID = GetStaffID();
            var foodAndSupplimentsOfStaff = await _context.FoodAndSuppliments.SingleOrDefaultAsync(a => a.Name.Equals(request.Name) && a.Neid.Equals(staffID));
            if (foodAndSupplimentsOfStaff == null)
            {
                return false;
            }*/

            Guid id = Guid.NewGuid();
            var createdate = DateTime.Now;
            FoodAndSuppliment foodAndSuppliment = new FoodAndSuppliment(id, request.Neid, request.Name, request.Ammount, request.UnitOfMesuament, request.Calories, createdate, false);
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
                result.IsDelete = foodAndSuppliment.IsDelete;
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
                    viewModel.IsDelete = foodAndSuppliment.IsDelete;
                    result.Add(viewModel);
                }
                return result;
            }
            return null;
        }

        public async Task<List<FoodAndSupplimentViewModel>> GetFoodAndSupplimentsBYNE(Guid id)
        {
            var foodAndSuppliments = await _context.FoodAndSuppliments.Where(a => a.Neid.Equals(id) && a.IsDelete == false).ToListAsync();

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
            FoodAndSuppliment foodAndSuppliment = new FoodAndSuppliment(request.Id, inDatabase.Neid, request.Name, request.Ammount, request.UnitOfMesuament, request.Calories, inDatabase.CreateDate, inDatabase.IsDelete);
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

        private Guid GetNEID()
        {
            Guid result = Guid.Empty;
            if (_httpContextAccessor.HttpContext is not null)
            {
                /*var roleClaim = User?.FindAll(ClaimTypes.Name);
                var phoneNo = roleClaim?.Select(c => c.Value).SingleOrDefault().ToString();
                var id = _context.Accounts.SingleOrDefault(x => x.PhoneNo.Equals(phoneNo))?.Id.ToString();*/

                
                //var nePhoneClaimRequest = _httpContextAccessor.HttpContext.User.Identity.Name;
                var nePhoneClaimRequest = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
                if (nePhoneClaimRequest != null)
                {
                    var accountRequest = _context.Accounts.SingleOrDefault(x => x.PhoneNo.Equals(nePhoneClaimRequest)).Id.ToString();
                    result = Guid.Parse(accountRequest);
                }
            }
            return result;
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
