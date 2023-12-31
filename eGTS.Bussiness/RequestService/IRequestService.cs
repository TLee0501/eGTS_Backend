﻿using eGTS_Backend.Data.Models;
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
        Task<int> CreateRequest(RequestCreateViewModel request);
        Task<Request> GetRequest(Guid id);
        Task<bool> UpdateRequest(RequestUpdateViewModel request);
        Task<List<RequestViewModel>> GetAllRequestForPTNE(Guid ExpertId);
    }
}
