using System;
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
        private readonly ILogger<SessionsController> _logger;
        private readonly IConfiguration _configuration;
        private readonly ISessionService _sessionService;

        public SessionsController(EGtsContext context, ILogger<SessionsController> logger, IConfiguration configuration, ISessionService accountService)
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
                return Ok(new SuccessResponse<List<SessionViewModel>>(200, "Danh sách các buổi tập: ", resultList));
            }
            else
                return NoContent();
        }

        // GET: api/Sessions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SessionViewModel>> GetSessionByID(Guid id)
        {
            var result = await _sessionService.GetSessionByID(id);
            if (result == null)
                return BadRequest(new ErrorResponse(400, "Không có buổi tập cùng ID trong DB"));
            else
                return Ok(new SuccessResponse<SessionViewModel>(200, "Tìm thấy buổi tập", result));
        }

        // GET: api/Sessions/5
        [HttpGet]
        public async Task<ActionResult<ExInSessionWithSessionIDViewModel>> GetAllExcerciseInSessionWithSessionID(Guid SessionID)
        {
            var result = await _sessionService.GetAllExcerciseInSessionWithSessionID(SessionID);
            if (result == null)
                return BadRequest(new ErrorResponse(400, "Không có buổi tập cùng ID trong DB"));
            else
                return Ok(new SuccessResponse<ExInSessionWithSessionIDViewModel>(200, "Danh Sách các bài tập trong buổi tập", result));
        }

        // GET: api/Sessions/5
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SessionViewModel>>> GetSessionByScheduleID(Guid id)
        {
            var result = await _sessionService.GetSessionListWithSceduleID(id);
            if (result == null)
                return BadRequest(new ErrorResponse(400, "Không có lịch tập cùng ID trong DB"));
            else
                return Ok(new SuccessResponse<IEnumerable<SessionViewModel>>(200, "Danh Sách các buổi tập trong lịch tập", result));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExInSessionWithSessionIDViewModel>>> GetAllExcerciseInSessionWithScheduleIDAndDateTime(Guid ScheduleID, DateTime dateTime)
        {
            var result = await _sessionService.GetAllExcerciseInSessionWithScheduleIDAndDateTime(ScheduleID, dateTime);
            if (result == null)
                return BadRequest(new ErrorResponse(400, "Không có buổi tập cùng dữ liệu trong DB"));
            else
                return Ok(new SuccessResponse<ExInSessionWithSessionIDViewModel>(200, "Danh Sách các bài tập trong buổi tập", result));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActiveSessionsViewModel>>> GetListOfActiveSessionByGymerID(Guid GymerID)
        {
            var result = await _sessionService.GetListOfActiveSessionByGymerID(GymerID);
            if (result == null)
                return BadRequest(new ErrorResponse(400, "Không tìm thấy buổi tập nào đang hoạt động trong DB"));
            else
                return Ok(new SuccessResponse<List<ActiveSessionsViewModel>>(200, "Danh Sách các buổi tập", result));
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
                return Ok(new SuccessResponse<SessionUpdateViewModel>(200, "Cập nhật thành công.", request));
            }
            else
                return BadRequest(new ErrorResponse(400, "Không cập nhật được buổi tập"));
        }

        // POST: api/Sessions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Session>> CreatNewSession(SessionCreateViewModel model)
        {

            if (model.ScheduleId == null || model.ScheduleId.Equals(""))
            {
                return BadRequest(new ErrorResponse(400, "ID đang bị bỏ trống."));
            }

            if (model.DateAndTime == null || model.DateAndTime.Equals(""))
            {
                return BadRequest(new ErrorResponse(400, "Ngày và giờ đang bị bỏ trống."));
            }

            if (await _sessionService.CreateSession(model))
            {
                _logger.LogInformation($"Created Session with for schedule with ID: {model.ScheduleId}");
                return Ok(new SuccessResponse<SessionCreateViewModel>(200, "Tạo thành công.", model));
            }
            else
                return BadRequest(new ErrorResponse(400, "Dữ liệu bị sai"));
        }

        // DELETE: api/Sessions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSession(Guid id)
        {
            if (await _sessionService.DeleteSession(id))
            {
                _logger.LogInformation($"Deleted Session with ID: {id}");
                return Ok(new SuccessResponse<SessionCreateViewModel>(200, "Xóa thành công.", null));
            }
            else
                return NoContent();
        }

        // DELETE: api/Sessions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSessionPEMANENT(Guid id)
        {
            if (await _sessionService.DeleteSessionPERMANENT(id))
            {
                _logger.LogInformation($"REMOVE Session with ID: {id}");
                return Ok(new SuccessResponse<SessionCreateViewModel>(200, "Xóa thành công.", null));
            }
            else
                return NoContent();
        }

        private bool SessionExists(Guid id)
        {
            return (_context.Sessions?.Any(e => e.Id == id)).GetValueOrDefault();
        }



    }
}
