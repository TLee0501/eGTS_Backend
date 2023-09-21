using coffee_kiosk_solution.Data.Responses;
using eGTS.Bussiness.BodyParameters;
using eGTS_Backend.Data.Models;
using eGTS_Backend.Data.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace eGTS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BodyPerametersController : ControllerBase
    {
        private readonly EGtsContext _context;
        private readonly ILogger<AccountsController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IBodyParametersService _bodyParametersService;

        public BodyPerametersController(EGtsContext context, ILogger<AccountsController> logger, IConfiguration configuration, IBodyParametersService _bodyParametersService)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
            this._bodyParametersService = _bodyParametersService;
        }


        // GET: api/BodyPerameters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BodyPerameterViewModel>>> DEBUGGetALLBodyPerameters()
        {
            var result = await _bodyParametersService.DEBUGGetAllBodyParameters();
            if (result != null)
            {
                return Ok(new SuccessResponse<List<BodyPerameterViewModel>>(200, "Danh sách tất cả các tỉ lệ cơ thể", result));
            }
            else
                return NoContent();
        }

        // GET: api/BodyPerameters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BodyPerameterViewModel>> GetBodyPerameterByID(Guid id)
        {

            var result = await _bodyParametersService.GetBodyParametersByID(id);
            if (result == null)
                return NoContent();
            else
                return Ok(new SuccessResponse<BodyPerameterViewModel>(200, "Tìm thấy tỉ lệ cơ thể", result)); ;

        }

        // GET: api/BodyPerameters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<BodyPerameterViewModel>>> GetBodyPerameterByGymerID(Guid id)
        {

            var result = await _bodyParametersService.GetBodyParameterByGymerID(id);
            if (result == null)
                return NoContent();
            else
                return Ok(new SuccessResponse<BodyPerameterViewModel>(200, "Tìm thấy các tỉ lệ cơ thể", result)); ;

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<BodyPerameterViewModel>>> GetBodyPerametersByGymerID(Guid id)
        {

            var result = await _bodyParametersService.GetBodyParametersByGymerID(id);
            if (result == null)
                return NoContent();
            else
                return Ok(new SuccessResponse<List<BodyPerameterViewModel>>(200, "Tìm thấy các tỉ lệ cơ thể", result)); ;

        }

        // PUT: api/BodyPerameters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBodyPerameter(Guid id, BodyPerameterUpdateViewModel request)
        {
            if (request.Weight.HasValue && request.Weight.Value < 0)
                return BadRequest(new ErrorResponse(400, "Cân nặng không hợp lệ!"));
            if (request.Height.HasValue && request.Height.Value < 0)
                return BadRequest(new ErrorResponse(400, "Chiều cao không hợp lệ!"));
            if (request.Bone.HasValue && request.Bone.Value < 0)
                return BadRequest(new ErrorResponse(400, "Thông số Bone không hợp lệ!"));
            if (request.Fat.HasValue && request.Fat.Value < 0)
                return BadRequest(new ErrorResponse(400, "Thông số Fat không hợp lệ không hợp lệ!"));
            if (request.Muscle.HasValue && request.Muscle.Value < 0)
                return BadRequest(new ErrorResponse(400, "Thông số Muscle không hợp lệ!"));
            if (!string.IsNullOrEmpty(request.Goal) && request.Goal.Length >= 300)
                return BadRequest(new ErrorResponse(400, "Thông số Goal không hợp lệ!"));

            if (await _bodyParametersService.UpdateBodyParameters(id, request))
            {
                _logger.LogInformation($"Update Body parameters with ID: {id}");
                return Ok(new SuccessResponse<BodyPerameterUpdateViewModel>(200, "Update thành công.", request));
            }
            else
            {
                return BadRequest(new ErrorResponse(400, "Không thể cập nhật thông số cơ thể thành công!"));
            }
        }

        // POST: api/BodyPerameters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BodyPerameter>> CreateBodyPerameter(BodyPerameterCreateViewModel model)
        {
            if (model.Weight.HasValue && model.Weight.Value < 0)
                return BadRequest(new ErrorResponse(400, "Cân nặng không hợp lệ!"));
            if (model.Height.HasValue && model.Height.Value < 0)
                return BadRequest(new ErrorResponse(400, "Chiều cao không hợp lệ!"));
            if (model.Bone.HasValue && model.Bone.Value < 0)
                return BadRequest(new ErrorResponse(400, "Thông số Bone không hợp lệ!"));
            if (model.Fat.HasValue && model.Fat.Value < 0)
                return BadRequest(new ErrorResponse(400, "Thông số Fat không hợp lệ không hợp lệ!"));
            if (model.Muscle.HasValue && model.Muscle.Value < 0)
                return BadRequest(new ErrorResponse(400, "Thông số Muscle không hợp lệ!"));
            if (!string.IsNullOrEmpty(model.Goal) && model.Goal.Length >= 300)
                return BadRequest(new ErrorResponse(400, "Thông số Goal không hợp lệ!"));

            var result = await _bodyParametersService.CreateBodyParameters(model);
            if (result)
            {
                _logger.LogInformation($"Created Body Parameters for gymer with ID: {model.GymerId}");
                return Ok(new SuccessResponse<BodyPerameterCreateViewModel>(200, "Tạo thành công.", model));
            }
            else
                return BadRequest(new ErrorResponse(400, "Dữ liệu bị sai"));
        }

        // DELETE: api/BodyPerameters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBodyPerameter(Guid id)
        {
            if (await _bodyParametersService.DeleteBodyParameters(id))
            {
                _logger.LogInformation($"Deleted BodyParaMeter with ID: {id}");
                return Ok(new SuccessResponse<BodyPerameterCreateViewModel>(200, "Xóa thành công.", null));
            }
            else
            {
                return NoContent();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBodyPerameterPERMANENT(Guid id)
        {
            if (await _bodyParametersService.DeleteBodyParametersPERMANENT(id))
            {
                _logger.LogInformation($"Deleted BodyParaMeter with ID: {id}");
                return Ok(new SuccessResponse<BodyPerameterCreateViewModel>(200, "Xóa thành công.", null));
            }
            else
            {
                return NoContent();
            }
        }

        private bool BodyPerameterExists(Guid id)
        {
            return (_context.BodyPerameters?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
