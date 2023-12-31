﻿using coffee_kiosk_solution.Data.Responses;
using eGTS.Bussiness.SessionService;
using eGTS_Backend.Data.Models;
using eGTS_Backend.Data.ViewModel;
using Microsoft.AspNetCore.Mvc;
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
                return BadRequest(new ErrorResponse(400, "Không tìm thấy kết quả luyện tập!"));
            else
                return Ok(new SuccessResponse<SessionResultViewModel>(200, "Kết quả luyện tập:", result));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<SessionResultViewModel>>> GetSessionResultBySessionID(Guid id)
        {
            var resultList = await _sessionService.GetSessionResultBySessionID(id);
            if (resultList == null)
                return BadRequest(new ErrorResponse(400, "Không tìm thấy kết quả luyện tập!"));
            else
                return Ok(new SuccessResponse<List<SessionResultViewModel>>(200, "Kết quả luyện tập:", resultList));
        }

        // PUT: api/SessionResults/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSessionResult(Guid id, SessionResultUpdateViewModel request)
        {
            if (request.Note.IsNullOrEmpty())
                return BadRequest(new ErrorResponse(400, "Ghi chú không hợp lệ!"));
            if (request.CaloConsump <= 0)
                return BadRequest(new ErrorResponse(400, "Lượng Calorie không hợp lệ!"));

            if (await _sessionService.UpdateSessionResult(id, request))
            {
                _logger.LogInformation($"Update Session with ID: {id}");
                return Ok(new SuccessResponse<SessionResultUpdateViewModel>(200, "Thành công!", request));
            }
            else
                return BadRequest(new ErrorResponse(400, "Thất bại!"));
        }

        // POST: api/SessionResults
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SessionResultCreateViewModel>> CreateSessionResult(SessionResultCreateViewModel model)
        {
            if (model.SessionId == Guid.Empty)
                return BadRequest(new ErrorResponse(400, "Session ID is empty."));
            if (model.Note.IsNullOrEmpty())
                return BadRequest(new ErrorResponse(400, "Ghi chú không hợp lệ!"));
            if (model.CaloConsump <= 0)
                return BadRequest(new ErrorResponse(400, "Lượng Calorie không hợp lệ!"));

            var result = await _sessionService.CreateSessionResult(model);
            if (result == 1)
            {
                _logger.LogInformation($"Created Session Result with for Session with ID: {model.SessionId}");
                return Ok(new ErrorResponse(200, "Thành công!"));
            }
            else if (result == 2) return BadRequest(new ErrorResponse(400, "Đã nhập kết quả buổi tập!"));
            else if (result == 3) return BadRequest(new ErrorResponse(400, "Chưa đến buổi tập!"));
            else
                return BadRequest(new ErrorResponse(400, "Thất bại!"));

        }

        // DELETE: api/SessionResults/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSessionResult(Guid id)
        {
            if (await _sessionService.DeleteSessionResult(id))
            {
                _logger.LogInformation($"Deleted Session Result with ID: {id}");
                return Ok(new SuccessResponse<SessionCreateViewModelV2>(200, "Thành công!", null));
            }
            else
                return BadRequest(new ErrorResponse(400, "Thất bại!"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSessionResultPEMANENT(Guid id)
        {
            if (await _sessionService.DeleteSessionResultPERMANENT(id))
            {
                _logger.LogInformation($"Deleted Session Result with ID: {id}");
                return Ok(new SuccessResponse<SessionCreateViewModelV2>(200, "Thành công!", null));
            }
            else
                return BadRequest(new ErrorResponse(400, "Thất bại!"));
        }
    }
}
