using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eGTS_Backend.Data.Models;
using eGTS.Bussiness.SessionService;
using eGTS_Backend.Data.ViewModel;
using coffee_kiosk_solution.Data.Responses;
using Azure.Core;
using Microsoft.IdentityModel.Tokens;

namespace eGTS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SessionResultsController : ControllerBase
    {
        private readonly EGtsContext _context;
        private readonly ILogger<SessionResultsController> _logger;
        private readonly IConfiguration _configuration;
        private readonly ISessionService _sessionService;

        public SessionResultsController(EGtsContext context, ILogger<SessionResultsController> logger, IConfiguration configuration, ISessionService sessionService)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
            _sessionService = sessionService;
        }



        // GET: api/SessionResults
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SessionResultViewModel>>> DEBUGGetSessionResultsList()
        {
            var resultList = await _sessionService.DebugGetAllSessionResultList();
            if (resultList != null)
            {
                return Ok(new SuccessResponse<List<SessionResultViewModel>>(200, "List of Session Results found", resultList));
            }
            else
                return NoContent();
        }

        // GET: api/SessionResults/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SessionResultViewModel>> GetSessionResultByID(Guid id)
        {
            var result = await _sessionService.GetSessionResultByID(id);
            if (result == null)
                return BadRequest(new ErrorResponse(400, "ID Not Found in Session Result DB"));
            else
                return Ok(new SuccessResponse<SessionResultViewModel>(200, "Session Result found", result));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<SessionResultViewModel>>> GetSessionResultBySessionID(Guid id)
        {
            var resultList = await _sessionService.GetSessionResultBySessionID(id);
            if (resultList == null)
                return BadRequest(new ErrorResponse(400, "Session ID Not Found in Session Result DB"));
            else
                return Ok(new SuccessResponse<List<SessionResultViewModel>>(200, "Session Result found", resultList));
        }

        // PUT: api/SessionResults/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSessionResult(Guid id, SessionResultUpdateViewModel request)
        {
            if (await _sessionService.UpdateSessionResult(id, request))
            {
                _logger.LogInformation($"Update Session with ID: {id}");
                return Ok(new SuccessResponse<SessionResultUpdateViewModel>(200, "Update Success.", request));
            }
            else
                return BadRequest(new ErrorResponse(400, "Unable to update Session"));
        }

        // POST: api/SessionResults
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SessionResultCreateViewModel>> CreateSessionResult(SessionResultCreateViewModel model)
        {
            if (model.SessionId.Equals("") || model.SessionId == null)
                return BadRequest(new ErrorResponse(400, "Session ID is empty."));
            if (model.Note.IsNullOrEmpty())
                return BadRequest(new ErrorResponse(400, "Note is empty."));
            if (model.CaloConsump == 0)
                return BadRequest(new ErrorResponse(400, "CaloConsump is equal 0."));

            if (await _sessionService.CreateSessionResult(model))
            {
                _logger.LogInformation($"Created Session Result with for Session with ID: {model.SessionId}");
                return Ok(new SuccessResponse<SessionResultCreateViewModel>(200, "Create Success.", model));
            }
            else
                return BadRequest(new ErrorResponse(400, "Invalid Data"));

        }

        // DELETE: api/SessionResults/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSessionResult(Guid id)
        {
            if (await _sessionService.DeleteSessionResult(id))
            {
                _logger.LogInformation($"Deleted Session Result with ID: {id}");
                return Ok(new SuccessResponse<SessionCreateViewModelV2>(200, "Delete Success.", null));
            }
            else
                return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSessionResultPEMANENT(Guid id)
        {
            if (await _sessionService.DeleteSessionResultPERMANENT(id))
            {
                _logger.LogInformation($"Deleted Session Result with ID: {id}");
                return Ok(new SuccessResponse<SessionCreateViewModelV2>(200, "Delete Success.", null));
            }
            else
                return NoContent();
        }

        private bool SessionResultExists(Guid id)
        {
            return (_context.SessionResults?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
