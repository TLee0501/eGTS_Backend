using eGTS_Backend.Data.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGTS.Bussiness.BodyParameters
{
    public class BodyParametersService : IBodyParametersService
    {
        public Task<bool> CreateBodyParameters(BodyPerameterCreateViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task<List<BodyPerameterViewModel>> DEBUGGetAllBodyParameters()
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteBodyParameters(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteBodyParametersPERMANENT(Guid id)
        {
            throw new NotImplementedException();
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
