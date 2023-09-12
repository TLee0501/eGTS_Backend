using eGTS_Backend.Data.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGTS.Bussiness.BodyParameters
{
    public interface IBodyParametersService
    {
        Task<bool> CreateBodyParameters(BodyPerameterCreateViewModel model);
        Task<bool> UpdateBodyParameters(Guid id, BodyPerameterUpdateViewModel model);
        Task<bool> DeleteBodyParameters(Guid id);
        Task<bool> DeleteBodyParametersPERMANENT(Guid id);
        Task<BodyPerameterViewModel> GetBodyParametersByID(Guid id);
        Task<List<BodyPerameterViewModel>> GetBodyParametersByGymerID(Guid id);
        Task<List<BodyPerameterViewModel>> DEBUGGetAllBodyParameters();
        Task<BodyPerameterViewModel> GetBodyParameterByGymerID(Guid GymerId);
    }
}
