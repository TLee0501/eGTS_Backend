﻿using coffee_kiosk_solution.Data.Responses;
using eGTS.Bussiness.ExcerciseService;
using eGTS_Backend.Data.Models;
using eGTS_Backend.Data.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace eGTS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ExcercisesController : ControllerBase
    {
        private readonly EGtsContext _context;
        private readonly ILogger<AccountsController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IExcerciseService _excerciseService;

        public ExcercisesController(EGtsContext context, ILogger<AccountsController> logger, IConfiguration configuration, IExcerciseService excerciseService)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
            _excerciseService = excerciseService;
        }

        /// <summary>
        /// Get all excercises
        /// </summary>
        /// <returns></returns>
        // GET: api/Excercises
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]//BAD REQUEST
        [ProducesResponseType(StatusCodes.Status200OK)]//OK
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Exercise>>> GetAllExcercises()
        {
            var result = await _excerciseService.GetAllExcercise();
            if (result == null)
            {
                return NoContent();
            }
            return Ok(new SuccessResponse<List<ExcerciseViewModel>>(200, "Danh sách các bài tập.", result));
        }

        /// <summary>
        /// Get excercises by name
        /// </summary>
        /// <returns></returns>
        // GET: api/ExcercisesByName
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]//BAD REQUEST
        public async Task<ActionResult<IEnumerable<Exercise>>> GetExcercisesByName(string Name)
        {

            var result = await _excerciseService.GetExcerciseByName(Name);
            if (result == null)
            {
                return BadRequest(new ErrorResponse(400, "Không tìm thấy bài tập!"));
            }
            return Ok(new SuccessResponse<List<ExcerciseViewModel>>(200, "Danh sách các bài tập.", result));
        }

        /// <summary>
        /// Get Excercise by PTID
        /// </summary>
        /// <param name="PTID"></param>
        /// <returns></returns>
        // GET: api/Excercises
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExcerciseViewModel>>> GetExcercisesByPTID(Guid PTID)
        {
            if (PTID == Guid.Empty)
            {
                return BadRequest(new ErrorResponse(400, "PTID đang bị bỏ trống."));
            }
            var result = await _excerciseService.GetExcerciseByPTID(PTID);
            if (result == null)
            {
                return BadRequest(new ErrorResponse(400, "Không tìm thấy bài tập!"));
            }
            return Ok(new SuccessResponse<List<ExcerciseViewModel>>(200, "Danh sách các bài tập.", result));
        }

        /// <summary>
        /// Get Excercise by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Excercises/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]//BAD REQUEST
        [ProducesResponseType(StatusCodes.Status200OK)]//OK
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ExcerciseViewModel>> GetExcerciseByID(Guid id)
        {
            var result = await _excerciseService.GetExcerciseByID(id);
            if (result != null)
            {
                return Ok(new SuccessResponse<ExcerciseViewModel>(200, "Bài tập tìm thấy.", result));
            }
            else
            {
                return BadRequest(new ErrorResponse(400, "Không tìm thấy bài tập"));
            }

        }

        /// <summary>
        /// Update Excercise with {ID}
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        // PUT: api/Excercises/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]//BAD REQUEST
        [ProducesResponseType(StatusCodes.Status200OK)]//OK
        public async Task<IActionResult> UpdateExcercise(Guid id, ExcerciseUpdateViewModel request)
        {
            if (request.CalorieCumsumption < 0) return BadRequest(new ErrorResponse(400, "Calory không hợp lệ!"));
            if (request.RepTime < 0) return BadRequest(new ErrorResponse(400, "RepTime không hợp lệ!"));
            if (!string.IsNullOrEmpty(request.Description) && request.Description.Length >= 300)
                return BadRequest(new ErrorResponse(400, "Mô tả không hợp lệ!"));

            if (await _excerciseService.UpdateExcercise(id, request))
                return Ok(new SuccessResponse<ExcerciseUpdateViewModel>(200, $"Bài tập có ID: {id} cập nhập thành công.", request));
            else
                return BadRequest(new ErrorResponse(400, "Cập nhập bài tập không thành công"));
        }

        /// <summary>
        /// Create new Excercise
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        // POST: api/Excercises
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]//BAD REQUEST
        [ProducesResponseType(StatusCodes.Status201Created)]//CREATED
        [ProducesResponseType(StatusCodes.Status200OK)]//OK
        public async Task<ActionResult<Exercise>> CreateExcercise(ExcerciseCreateViewModel model)
        {
            if (model.Ptid.Equals("") || model.Ptid == Guid.Empty || model.Ptid.Equals("string"))
            {
                return BadRequest(new ErrorResponse(400, "PTID đang bị bỏ trống."));
            }
            if (model.Name.IsNullOrEmpty())
            {
                return BadRequest(new ErrorResponse(400, "Name đang bị bỏ trống."));
            }
            if (model.CalorieCumsumption < 0) return BadRequest(new ErrorResponse(400, "Calory không hợp lệ!"));
            if (model.RepTime < 0) return BadRequest(new ErrorResponse(400, "RepTime không hợp lệ!"));

            if (!string.IsNullOrEmpty(model.Name) && model.Name.Length >= 50)
                return BadRequest(new ErrorResponse(400, "Tên không hợp lệ!"));

            if (!string.IsNullOrEmpty(model.Description) && model.Description.Length >= 300)
                return BadRequest(new ErrorResponse(400, "Mô tả không hợp lệ!"));


            if (await _excerciseService.CreateExcercise(model))
            {
                _logger.LogInformation($"Excercise Created by PT with ID: {model.Ptid}");
                return Ok(new SuccessResponse<ExcerciseCreateViewModel>(200, "Tạo thành công.", model));
            }
            else
                return BadRequest(new ErrorResponse(400, "Dữ liệu không hợp lệ"));

        }


        /// <summary>
        /// Delete Excercise with ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Excercises/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExcercise(Guid id)
        {
            if (await _excerciseService.DeleteExcercise(id))
            {
                _logger.LogInformation($"Deleted Excercise with ID: {id}");
                return Ok(new ErrorResponse(200, "Thành công!"));
            }
            return BadRequest(new ErrorResponse(400, $"Unable to delete Excercise with ID: {id}"));

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExcercisePARMANENT(Guid id)
        {
            if (await _excerciseService.DeleteExcercisePEMANENT(id))
            {
                _logger.LogInformation($"Deleted Excercise with ID: {id}");
                return Ok(new ErrorResponse(200, "Thành công!"));
            }
            return BadRequest(new ErrorResponse(400, $"Xóa bài tập thất bại ID: {id}"));

        }
    }
}
