using coffee_kiosk_solution.Data.Responses;
using eGTS.Bussiness.SessionService;
using eGTS_Backend.Data.Models;
using eGTS_Backend.Data.ViewModel;
using Microsoft.AspNetCore.Mvc;

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
            return Ok(new SuccessResponse<List<ExInSessionViewModel>>(200, "Tìm thấy dữ liệu!", result));
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
                return Ok(new SuccessResponse<ExInSessionUpdateViewModel>(200, $"Dữ liệu với ID: {id} đã được cập nhập.", request));
            else
                return BadRequest(new ErrorResponse(400, "Không thể cập nhập dữ liệu"));
        }

        // POST: api/ExcerciseInSessions
        [HttpPost]
        public async Task<ActionResult<ExerciseInSession>> CreateExserciseInSession(ExInSessionCreateViewModel model)
        {
            if (model.SessionId == Guid.Empty)
            {
                return BadRequest(new ErrorResponse(400, "ID bữa tập đang bị rỗng."));
            }
            if (model.ExerciseId == Guid.Empty)
            {
                return BadRequest(new ErrorResponse(400, "ID bài tập đang bị rỗng."));
            }
            if (await _sessionService.CreateExcerciseInSession(model))
            {
                _logger.LogInformation($"Excercise Added to Session with ID:{model.SessionId}");
                return Ok(new SuccessResponse<ExInSessionCreateViewModel>(200, "Tạo thành công", model));
            }
            else
                return BadRequest(new ErrorResponse(400, "Sai dữ liệu"));
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
            return BadRequest(new ErrorResponse(400, $"Không xóa được bài tập trong bữa tập có ID là: {id}"));
        }

        private bool ExserciseInSessionExists(Guid id)
        {
            return (_context.ExerciseInSessions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
