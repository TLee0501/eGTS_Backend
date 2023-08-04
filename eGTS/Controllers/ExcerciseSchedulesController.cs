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
    }
}
