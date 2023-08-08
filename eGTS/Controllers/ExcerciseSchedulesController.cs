using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eGTS_Backend.Data.Models;
using eGTS.Bussiness.AccountService;
using eGTS.Bussiness.ExcerciseScheduleService;
using coffee_kiosk_solution.Data.Responses;
using eGTS_Backend.Data.ViewModel;

namespace eGTS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ExcerciseSchedulesController : ControllerBase
    {
        private readonly EGtsContext _context;
        private readonly ILogger<ExcerciseSchedulesController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IExcerciseScheduleService _exSCheduleService;

        public ExcerciseSchedulesController(EGtsContext context, ILogger<ExcerciseSchedulesController> logger, IConfiguration configuration, IExcerciseScheduleService exSCheduleService)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
            _exSCheduleService = exSCheduleService;
        }




        // GET: api/ExcerciseSchedules
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NoContent)]//NOT FOUND
        [ProducesResponseType(StatusCodes.Status200OK)]//OK
        public async Task<ActionResult<IEnumerable<ExScheduleViewModel>>> GetExcerciseSchedules(bool? isExpired)
        {
            var result = await _exSCheduleService.DEBUGGetAllExcerciseSchedule(isExpired);
            if (result != null)
            {
                return Ok(new SuccessResponse<List<ExScheduleViewModel>>(200, "Tìm được danh sách các lịch tập", result));
            }
            else
            {
                return NoContent();
            }
        }

        // GET: api/ExcerciseSchedules/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]//NOT FOUND
        [ProducesResponseType(StatusCodes.Status200OK)]//OK
        public async Task<ActionResult<ExScheduleViewModel>> GetExcerciseSchedule(Guid id)
        {
            var result = await _exSCheduleService.GetExcerciseScheduleByID(id);
            if (result != null)
            {
                return Ok(new SuccessResponse<ExScheduleViewModel>(200, "Tìm được lịch tập", result));
            }
            else
                return NoContent();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]//NOT FOUND
        [ProducesResponseType(StatusCodes.Status200OK)]//OK
        public async Task<ActionResult<List<ExScheduleViewModel>>> GetExcerciseScheduleByPTID(Guid id, bool? isExpired)
        {
            var result = await _exSCheduleService.GetExcerciseSchedulesWithPTID(id, isExpired);
            if (result != null)
            {
                return Ok(new SuccessResponse<List<ExScheduleViewModel>>(200, "Tìm được lịch tập", result));
            }
            else
                return NoContent();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]//NOT FOUND
        [ProducesResponseType(StatusCodes.Status200OK)]//OK
        public async Task<ActionResult<List<ExScheduleViewModel>>> GetExcerciseScheduleBygymerID(Guid id, bool? isExpired)
        {
            var result = await _exSCheduleService.GetExcerciseSchedulesWithGymerID(id, isExpired);
            if (result != null)
            {
                return Ok(new SuccessResponse<List<ExScheduleViewModel>>(200, "Tìm được lịch tập", result));
            }
            else
                return NoContent();
        }

        // PUT: api/ExcerciseSchedules/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExcerciseSchedule(Guid id, ExScheduleUpdateViewModel request)
        {
            DateTime fromDate = DateTime.MinValue;
            DateTime toDate = DateTime.MinValue;
            if (!request.From.Equals(""))
                fromDate = Convert.ToDateTime(request.From);
            if (!request.To.Equals(""))
                toDate = Convert.ToDateTime(request.To);

            if (await _exSCheduleService.UpdateExcerciseSchedule(id, request))
            {
                _logger.LogInformation($"Uodate ExCerciseSchedule with ID: {id}");
                return Ok(new SuccessResponse<ExScheduleUpdateViewModel>(200, "Update thành công.", request));
            }
            else
                return BadRequest(new ErrorResponse(400, "Invalid Data"));

        }

        // POST: api/ExcerciseSchedules
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]//BAD REQUEST
        [ProducesResponseType(StatusCodes.Status201Created)]//CREATED
        [ProducesResponseType(StatusCodes.Status200OK)]//OK
        public async Task<ActionResult<ExcerciseSchedule>> CreateNewExcerciseSchedule(ExScheduleCreateViewModel model)
        {
            if (model.GymerId.Equals("") || model.GymerId == null)
                return BadRequest(new ErrorResponse(400, "GymerId empty."));
            if (model.Ptid.Equals("") || model.Ptid == null)
                return BadRequest(new ErrorResponse(400, "Ptid empty."));
            if (model.PackageGymerId.Equals("") || model.PackageGymerId == null)
                return BadRequest(new ErrorResponse(400, "PackageGymerId empty."));
            if (model.To < DateTime.Now)
                return BadRequest(new ErrorResponse(400, "invalid End Date"));
            if (model.From >= model.To)
                return BadRequest(new ErrorResponse(400, "invalid Start Date"));

            if (await _exSCheduleService.CreateExcerciseSchedule(model))
            {
                _logger.LogInformation($"Created ExCerciseSchedule for Gymer with ID: {model.GymerId} and PT with ID {model.GymerId}");
                return Ok(new SuccessResponse<ExScheduleCreateViewModel>(200, "Create Success.", model));
            }
            else
                return BadRequest(new ErrorResponse(400, "Invalid Data"));
        }

        // DELETE: api/ExcerciseSchedules/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExcerciseSchedulePERMANENT(Guid id)
        {
            if (await _exSCheduleService.DeleteExcerciseSchedulePERMANENT(id))
            {
                _logger.LogInformation($"Deleted Schedule with ID: {id}");
                return Ok(new SuccessResponse<ExScheduleCreateViewModel>(200, "Delete Success.", null));
            }
            else
            {
                return NoContent();
            }
        }

        // DELETE: api/ExcerciseSchedules/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExcerciseSchedule(Guid id)
        {
            if (await _exSCheduleService.DeleteExcerciseSchedule(id))
            {
                _logger.LogInformation($"Deleted Schedule with ID: {id}");
                return Ok(new SuccessResponse<ExScheduleCreateViewModel>(200, "Delete Success.", null));
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("{GymerId}")]
        public async Task<ActionResult<IEnumerable<SessionDetailViewModel>>> GetExcerciseScheduleByGymerIDAndDate(Guid GymerId, DateTime date)
        {
            var result = await _exSCheduleService.GetExcerciseScheduleByGymerIDAndDate(GymerId, date);

            if (result == null)
            {
                return BadRequest("Không tìm thấy lịch tập!");
            }

            return Ok(new SuccessResponse<List<SessionDetailViewModel>>(200, "Danh sách lịch tập!", result));
        }

        [HttpGet("{GymerId}")]
        public async Task<ActionResult<IEnumerable<SessionDetailViewModel>>> GetExcerciseScheduleByGymerIDV2(Guid GymerId)
        {
            var result = await _exSCheduleService.GetExcerciseScheduleByGymerIDV2(GymerId);

            if (result == null)
            {
                return BadRequest("Không tìm thấy lịch tập!");
            }

            return Ok(new SuccessResponse<List<SessionDetailViewModel>>(200, "Danh sách lịch tập!", result));
        }

        [HttpGet("{PTId}")]
        public async Task<ActionResult<IEnumerable<SessionOfPTViewModel>>> GetWorkingScheduleByPTIDAndDate(Guid PTId, DateTime date)
        {
            var result = await _exSCheduleService.GetWorkingScheduleByPTIDAndDate(PTId, date);

            if (result == null)
            {
                return BadRequest("Không tìm thấy lịch tập!");
            }

            return Ok(new SuccessResponse<List<SessionOfPTViewModel>>(200, "Danh sách lịch tập!", result));
        }

        [HttpGet("{PTId}")]
        public async Task<ActionResult<IEnumerable<SessionOfPTViewModel>>> GetWorkingScheduleByPTID(Guid PTId)
        {
            var result = await _exSCheduleService.GetWorkingScheduleByPTID(PTId);

            if (result == null)
            {
                return BadRequest("Không tìm thấy lịch tập!");
            }

            return Ok(new SuccessResponse<List<SessionOfPTViewModel>>(200, "Danh sách lịch tập!", result));
        }

        [HttpPost]
        public async Task<ActionResult<ExcerciseSchedule>> CreateExcerciseScheduleV3(ExcerciseScheduleCreateViewModelV3 request)
        {
            if (request.PackageGymerID.Equals("") || request.PackageGymerID == null)
                return BadRequest(new ErrorResponse(400, "Không tìm thấy PackageGymerID!"));
            if (request.From.Equals("") || request.From == null)
                return BadRequest(new ErrorResponse(400, "Ptid empty."));
            if (request.From.Equals("") || request.From == null)
                return BadRequest(new ErrorResponse(400, "Không có ngày bắt đầu!"));
            if (request.To.Equals("") || request.To == null)
                return BadRequest(new ErrorResponse(400, "Không có ngày kết thúc!"));
            if (request.From < DateTime.Now)
                return BadRequest(new ErrorResponse(400, "Sai ngày bắt đầu!"));
            if (request.From > request.To)
                return BadRequest(new ErrorResponse(400, "Ngày bắt đầu lớn hơn ngày kết thúc!"));

            if (await _exSCheduleService.CreateExcerciseScheduleV3(request))
            {
                _logger.LogInformation($"Created ExCerciseSchedule for Gymer with ID: {request.PackageGymerID}");
                return Ok(new SuccessResponse<ExcerciseScheduleCreateViewModelV3>(200, "Tạo thành công!", request));
            }
            else
                return BadRequest(new ErrorResponse(400, "Không thành công!"));
        }

        [HttpGet("{packageGymerID}")]
        public async Task<ActionResult<IEnumerable<SessionDetailViewModel>>> GetExcerciseScheduleByPackageGymerIDAndDate(Guid packageGymerID, DateTime date)
        {
            var result = await _exSCheduleService.GetExcerciseScheduleByPackageGymerIDAndDate(packageGymerID, date);

            if (result == null)
            {
                return BadRequest("Không tìm thấy lịch tập!");
            }

            return Ok(new SuccessResponse<List<SessionDetailViewModel>>(200, "Danh sách lịch tập!", result));
        }

        [HttpGet("{packageGymerID}")]
        public async Task<ActionResult<IEnumerable<SessionDateViewModel>>> GetExcerciseScheduleByPackageGymerID(Guid packageGymerID)
        {
            var result = await _exSCheduleService.GetExcerciseScheduleByPackageGymerID(packageGymerID);

            if (result == null)
            {
                return BadRequest("Không tìm thấy lịch tập!");
            }

            return Ok(new SuccessResponse<List<SessionDateViewModel>>(200, "Danh sách lịch tập!", result));
        }
    }
}
