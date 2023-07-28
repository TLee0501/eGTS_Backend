using eGTS.Bussiness.AccountService;
using eGTS.Bussiness.ExcerciseScheduleService;
using eGTS.Bussiness.ExcerciseService;
using eGTS_Backend.Data.Models;
using eGTS_Backend.Data.ViewModel;
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

        public Task<bool> CreateBodyParameters(BodyPerameterCreateViewModel model)
        {
            /*Guid id = Guid.NewGuid();
            BodyPerameter BPS = new BodyPerameter(id, );*/
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
