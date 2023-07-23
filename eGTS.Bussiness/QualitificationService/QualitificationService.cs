using Azure.Core;
using eGTS.Bussiness.AccountService;
using eGTS_Backend.Data.Models;
using eGTS_Backend.Data.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGTS.Bussiness.QualitificationService
{
    public class QualitificationService : IQualitificationService
    {
        private readonly EGtsContext _context;
        private readonly ILogger<IQualitificationService> _logger;

        public QualitificationService(EGtsContext context, ILogger<IQualitificationService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> CreateQualitification(QualitificationCreateViewModel request)
        {
            try
            {
                Qualification qualitification = new Qualification(request.ExpertId, request.Certificate, request.Experience, true, false);
                await _context.Qualifications.AddAsync(qualitification);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Invalid data." + ex);
                return false;
            }
        }

        public async Task<ActionResult<QualitificationViewModel>> GetQualitificationByAccountId(Guid id)
        {
            try
            {
                var qualitification = await _context.Qualifications.FirstOrDefaultAsync(a => a.ExpertId == id && a.IsDelete == false);
                if (qualitification == null) return null;
                var viewModel = new QualitificationViewModel()
                {
                    ExpertId = qualitification.ExpertId,
                    Certificate = qualitification.Certificate,
                    Experience = qualitification.Experience,
                    IsCetifide = qualitification.IsCetifide
                };
                return viewModel;
            }
            catch (Exception ex)
            {
                _logger.LogError("Invalid data." + ex);
                return null;
            }
        }
    }
}
