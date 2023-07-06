using eGTS_Backend.Data.Models;
using eGTS_Backend.Data.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGTS.Bussiness.RequestService
{
    public interface IRequestService
    {
        Task<bool> CreateRequest(RequestCreateViewModel request);
        Task<Request> GetRequest(Guid id);
        Task<bool> UpdateRequest(RequestViewModel request);
        Task<List<RequestViewModel>> GetAllRequestForPTNE(Guid id, bool isPT);
    }
}
