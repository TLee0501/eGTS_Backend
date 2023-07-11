﻿using eGTS.Bussiness.AccountService;
using eGTS_Backend.Data.Models;
using eGTS_Backend.Data.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGTS.Bussiness.SessionService
{
    public class SessionService : ISessionService
    {

        private readonly EGtsContext _context;
        private readonly ILogger<IAccountService> _logger;

        public SessionService(EGtsContext context, ILogger<IAccountService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> CreateSession(SessionCreateViewModel model)
        {
            Guid id = Guid.NewGuid();
            Session session = new Session(id, model.ScheduleId, model.DateAndTime);

            try
            {
                await _context.Sessions.AddAsync(session);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Invalid Data");
                return false;
            }

        }

        public async Task<List<SessionViewModel>> DebugGetAllSessionList()
        {
            List<SessionViewModel> resultList = new List<SessionViewModel>();
            var sessions = await _context.Sessions.ToListAsync();
            if (sessions.Count > 0)
            {
                foreach (var session in sessions)
                {
                    SessionViewModel model = new SessionViewModel();
                    model.ScheduleId = session.ScheduleId;
                    model.DateAndTime = session.DateAndTime;

                    resultList.Add(model);
                }
                return resultList;
            }
            return null;
        }

        public async Task<bool> DeleteSession(Guid id)
        {
            var session = await _context.Sessions.FindAsync(id);
            if (session != null)
            {
                _context.Sessions.Remove(session);
                await _context.SaveChangesAsync();
            }
            return false;
        }

        public async Task<SessionViewModel> GetSessionByID(Guid id)
        {
            var session = await _context.Sessions.FindAsync(id);
            if (session != null)
            {
                SessionViewModel result = new SessionViewModel();
                result.ScheduleId = session.ScheduleId;
                result.DateAndTime = session.DateAndTime;
                return result;
            }
            else
                return null;
        }

        public async Task<List<SessionViewModel>> GetSessionListWithSceduleID(Guid id)
        {
            List<SessionViewModel> resultList = new List<SessionViewModel>();
            var sessions = await _context.Sessions.Where(s => s.ScheduleId == id).ToListAsync();
            if (sessions.Count > 0)
            {
                foreach (var session in sessions)
                {
                    SessionViewModel model = new SessionViewModel();
                    model.ScheduleId = session.ScheduleId;
                    model.DateAndTime = session.DateAndTime;

                    resultList.Add(model);
                }
                return resultList;
            }
            return null;
        }

        public async Task<bool> UpdateSession(Guid id, SessionUpdateViewModel request)
        {
            var session = await _context.Sessions.FindAsync(id);
            if (session == null)
                return false;
            if (request.ScheduleId != null || !request.ScheduleId.Equals(""))
            {
                session.ScheduleId = request.ScheduleId;
            }
            if (request.DateAndTime != null || !request.DateAndTime.Equals(""))
            {
                session.DateAndTime = request.DateAndTime;
            }

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Unable to Update Session");
            }
            return false;
        }
    }
}