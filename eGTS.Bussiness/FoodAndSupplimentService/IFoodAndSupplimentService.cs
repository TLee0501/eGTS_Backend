using eGTS_Backend.Data.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGTS.Bussiness.FoodAndSupplimentService
{
    public interface IFoodAndSupplimentService
    {
        Task<List<FoodAndSupplimentViewModel>> GetFoodAndSuppliments();
        Task<List<FoodAndSupplimentViewModel>> GetFoodAndSupplimentsBYNE(Guid id);
        Task<FoodAndSupplimentViewModel> GetFoodAndSuppliment(Guid id);
        Task<bool> UpdateFoodAndSuppliment(FoodAndSupplimentUpdateViewModel request);
        Task<bool> CreateFoodAndSuppliment(FoodAndSupplimentCreateViewModel request);
        Task<bool> DeleteFoodAndSuppliment(Guid id);
        Task<List<FoodAndSupplimentViewModel>> SearchFoodAndSupplimentsByNameAndNE(Guid NEID, string FoodName);
    }
}
