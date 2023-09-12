using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eGTS_Backend.Data.Models;
using eGTS.Bussiness.AccountService;
using eGTS.Bussiness.BodyParameters;
using coffee_kiosk_solution.Data.Responses;
using eGTS_Backend.Data.ViewModel;

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
            if (await _bodyParametersService.UpdateBodyParameters(id, request))
            {
                _logger.LogInformation($"Update Body parameters with ID: {id}");
                return Ok(new SuccessResponse<BodyPerameterUpdateViewModel>(200, "Update thành công.", request));
            }
            else
            {
                return BadRequest(new ErrorResponse(400, "Không thể update tài khoản"));
            }
        }

        // POST: api/BodyPerameters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BodyPerameter>> CreateBodyPerameter(BodyPerameterCreateViewModel model)
        {

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
