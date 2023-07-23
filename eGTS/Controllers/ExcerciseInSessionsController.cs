﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eGTS_Backend.Data.Models;
using eGTS.Bussiness.ExcerciseService;
using eGTS.Bussiness.SessionService;
using coffee_kiosk_solution.Data.Responses;
using eGTS_Backend.Data.ViewModel;

namespace eGTS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ExcerciseInSessionsController : ControllerBase
    {
        private readonly EGtsContext _context;
        private readonly ILogger<AccountsController> _logger;
        private readonly IConfiguration _configuration;
        private readonly ISessionService _sessionService;

        public ExcerciseInSessionsController(EGtsContext context, ILogger<AccountsController> logger, IConfiguration configuration, ISessionService sessionService)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
            _sessionService = sessionService;
        }



        // GET: api/ExcerciseInSessions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExInSessionViewModel>>> DEBUGGetAllExserciseInSessions()
        {
            var result = await _sessionService.DebugGetAllExcerciseInSessionList();
            if (result == null)
            {
                return NoContent();
            }
            return Ok(new SuccessResponse<List<ExInSessionViewModel>>(200, "Data Found.", result));
        }

        // GET: api/ExcerciseInSessions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExInSessionViewModel>> GetExserciseInSessionByID(Guid id)
        {
            var result = await _sessionService.GetExcerciseInSessionByID(id);
            if (result == null)
                return NoContent();
            else
                return result;
        }

        // GET: api/ExcerciseInSessions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<ExInSessionViewModel>>> GetExserciseInSessionBySessionID(Guid id)
        {
            var result = await _sessionService.GetExcerciseInSessionBySessionID(id);
            if (result == null)
                return NoContent();
            else
                return result;
        }

        // PUT: api/ExcerciseInSessions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExserciseInSession(Guid id, ExInSessionUpdateViewModel request)
        {
            if (await _sessionService.UpdateExcerciseInSession(id, request))
                return Ok(new SuccessResponse<ExInSessionUpdateViewModel>(200, $"Data with ID: {id} Updated.", request));
            else
                return BadRequest(new ErrorResponse(400, "Unable to update Data"));
        }

        // POST: api/ExcerciseInSessions
        [HttpPost]
        public async Task<ActionResult<ExserciseInSession>> CreateExserciseInSession(ExInSessionCreateViewModel model)
        {
            if (model.SessionId.Equals("") || model.SessionId == null)
            {
                return BadRequest(new ErrorResponse(400, "Session ID is empty."));
            }
            if (model.ExerciseId.Equals("") || model.ExerciseId == null)
            {
                return BadRequest(new ErrorResponse(400, "Excercise ID is empty."));
            }
            if (await _sessionService.CreateExcerciseInSession(model))
            {
                _logger.LogInformation($"Excercise Added to Session with ID:{model.SessionId}");
                return Ok(new SuccessResponse<ExInSessionCreateViewModel>(200, "Create Success.", model));
            }
            else
                return BadRequest(new ErrorResponse(400, "Invalid Data"));
        }

        // DELETE: api/ExcerciseInSessions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExserciseInSession(Guid id)
        {
            if (await _sessionService.DeleteExcerciseInSessionPERMANENT(id))
            {
                _logger.LogInformation($"Deleted Excercise in Session with ID: {id}");
                return NoContent();
            }
            return BadRequest(new ErrorResponse(400, $"Unable to delete Excercise in Session with ID: {id}"));
        }

        private bool ExserciseInSessionExists(Guid id)
        {
            return (_context.ExserciseInSessions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}