﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eGTS_Backend.Data.Models;
using eGTS.Bussiness.AccountService;
using eGTS.Bussiness.SessionService;
using coffee_kiosk_solution.Data.Responses;
using eGTS_Backend.Data.ViewModel;
using Microsoft.AspNetCore.Http.HttpResults;
using Azure.Core;

namespace eGTS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SessionsController : ControllerBase
    {
        private readonly EGtsContext _context;
        private readonly ILogger<AccountsController> _logger;
        private readonly IConfiguration _configuration;
        private readonly ISessionService _sessionService;

        public SessionsController(EGtsContext context, ILogger<AccountsController> logger, IConfiguration configuration, ISessionService accountService)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
            _sessionService = accountService;
        }

        // GET: api/Sessions
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NoContent)]//NOT FOUND
        [ProducesResponseType(StatusCodes.Status200OK)]//OK
        public async Task<ActionResult<IEnumerable<SessionViewModel>>> DEBUGGetALLSessions()
        {
            var resultList = await _sessionService.DebugGetAllSessionList();
            if (resultList != null)
            {
                return Ok(new SuccessResponse<List<SessionViewModel>>(200, "List of Sessions found", resultList));
            }
            else
                return NotFound(new ErrorResponse(204, "No Session Found"));
        }

        // GET: api/Sessions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SessionViewModel>> GetSessionByID(Guid id)
        {
            var result = await _sessionService.GetSessionByID(id);
            if (result == null)
                return NotFound(new ErrorResponse(400, "ID Not Match With session in DB"));
            else
                return result;
        }

        // PUT: api/Sessions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]//BAD REQUEST
        [ProducesResponseType(StatusCodes.Status204NoContent)]//NOT FOUND
        [ProducesResponseType(StatusCodes.Status200OK)]//OK
        public async Task<IActionResult> UpdateSession(Guid id, SessionUpdateViewModel request)
        {
            if (await _sessionService.UpdateSession(id, request))
            {
                _logger.LogInformation($"Update Session with ID: {id}");
                return Ok(new SuccessResponse<SessionUpdateViewModel>(200, "Update Success.", request));
            }
            else
                return BadRequest(new ErrorResponse(400, "Unable to update Session"));
        }

        // POST: api/Sessions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Session>> CreatNewSession(SessionCreateViewModel model)
        {

            if (model.ScheduleId == null || model.ScheduleId.Equals(""))
            {
                return BadRequest(new ErrorResponse(400, "Schedule ID is empty."));
            }

            if (model.DateAndTime == null || model.DateAndTime.Equals(""))
            {
                return BadRequest(new ErrorResponse(400, "Date And Time is empty."));
            }

            if (await _sessionService.CreateSession(model))
            {
                _logger.LogInformation($"Created Session with for schedule with ID: {model.ScheduleId}");
                return Ok(new SuccessResponse<SessionCreateViewModel>(200, "Create Success.", model));
            }
            else
                return BadRequest(new ErrorResponse(400, "Invalid Data"));
        }

        // DELETE: api/Sessions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSession(Guid id)
        {
            if (await _sessionService.DeleteSession(id))
            {
                _logger.LogInformation($"Deleted Session with ID: {id}");
                return NoContent();
            }
            else
                return NotFound(new ErrorResponse(204, "Session Not Found In DataBase"));
        }

        private bool SessionExists(Guid id)
        {
            return (_context.Sessions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
