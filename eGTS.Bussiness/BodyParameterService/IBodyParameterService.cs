using eGTS_Backend.Data.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGTS.Bussiness.BodyParameterService
{
    public interface IBodyParameterService
    {
        Task<bool> CreateBodyParamenter(BodyParameterCreateViewModel model);
        Task<bool> UpdateBodyParamenter(Guid id, BodyParameterUpdateViewModel model);
        Task<bool> DeleteBodyParamenter(Guid id);
        Task<bool> DeleteBodyParamenterPERMANENT(Guid id);
        Task<List<BodyParameterViewModel>> DEBUGGetAllBodyPerameter();
        Task<List<BodyParameterViewModel>> GetBodyParameterByGymerID(Guid id);
        Task<BodyParameterViewModel> GetBodyParameterByID(Guid id);
    }
}
