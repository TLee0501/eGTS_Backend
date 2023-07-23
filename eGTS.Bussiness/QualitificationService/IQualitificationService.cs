using eGTS_Backend.Data.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGTS.Bussiness.QualitificationService
{
    public interface IQualitificationService
    {
        Task<bool> CreateQualitification(QualitificationCreateViewModel request);
        Task<ActionResult<QualitificationViewModel>> GetQualitificationByAccountId(Guid id);
    }
}
