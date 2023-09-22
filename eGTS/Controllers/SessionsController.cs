using coffee_kiosk_solution.Data.Responses;
using eGTS.Bussiness.SessionService;
using eGTS_Backend.Data.Models;
using eGTS_Backend.Data.ViewModel;
using Microsoft.AspNetCore.Mvc;

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
        [HttpPut("{sessionId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]//BAD REQUEST
        [ProducesResponseType(StatusCodes.Status204NoContent)]//NOT FOUND
        [ProducesResponseType(StatusCodes.Status200OK)]//OK
        public async Task<IActionResult> UpdateSession(Guid sessionId, SessionUpdateViewModel request)
        {
            if (request.DateTime.Date < DateTime.Now.Date)
                return BadRequest(new ErrorResponse(400, "Sai ngày bắt đầu!"));
            if (TimeSpan.Parse(request.To) < TimeSpan.Parse(request.From))
                return BadRequest(new ErrorResponse(400, "Sai giờ bắt đầu và kết thúc!"));
            if (await _sessionService.UpdateSessionV3(sessionId, request))
            {
                _logger.LogInformation($"Update Session with ID: {sessionId}");
                return Ok(new SuccessResponse<SessionUpdateViewModel>(200, "Cập nhật thành công.", request));
            }
            else
                return BadRequest(new ErrorResponse(400, "Không cập nhật được buổi tập"));
        }

        // POST: api/Sessions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*[HttpPost]
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
        }*/

        // DELETE: api/Sessions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSession(Guid id)
        {
            if (await _sessionService.DeleteSession(id))
            {
                _logger.LogInformation($"Deleted Session with ID: {id}");
                return Ok(new SuccessResponse<SessionCreateViewModelV2>(200, "Xóa thành công.", null));
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
                return Ok(new SuccessResponse<SessionCreateViewModelV2>(200, "Xóa thành công.", null));
            }
            else
                return NoContent();
        }

        /*[HttpPost]
        public async Task<ActionResult<Session>> CreateSessionV2(SessionCreateViewModelV2 model)
        {

            if (model.PackageGymerID == null || model.PackageGymerID.Equals(""))
            {
                return BadRequest(new ErrorResponse(400, "PackageGymerID đang bị bỏ trống."));
            }

            if (model.DateAndTime == null || model.DateAndTime.Equals(""))
            {
                return BadRequest(new ErrorResponse(400, "Ngày và giờ đang bị bỏ trống."));
            }

            if (await _sessionService.CreateSessionV2(model))
            {
                _logger.LogInformation($"Created Session with for schedule with PackageGymerID: {model.PackageGymerID}");
                return Ok(new SuccessResponse<SessionCreateViewModelV2>(200, "Tạo thành công.", model));
            }
            else
                return BadRequest(new ErrorResponse(400, "Dữ liệu bị sai"));
        }*/
    }
}
