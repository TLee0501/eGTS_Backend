using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eGTS_Backend.Data.Models;
using eGTS.Bussiness.ExcerciseService;
using eGTS_Backend.Data.ViewModel;
using coffee_kiosk_solution.Data.Responses;
using Azure.Core;

namespace eGTS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ExcerciseTypesController : ControllerBase
    {
        private readonly EGtsContext _context;
        private readonly ILogger<AccountsController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IExcerciseService _excerciseService;

        public ExcerciseTypesController(EGtsContext context, ILogger<AccountsController> logger, IConfiguration configuration, IExcerciseService excerciseService)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
            _excerciseService = excerciseService;
        }

        /// <summary>
        /// Get Excercise Type with ID 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/ExcerciseTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExcerciseType>> GetExcerciseTypeByID(Guid id)
        {
            var result = await _excerciseService.GetExcerciseTypeByID(id);
            if (result != null)
            {
                return Ok(new SuccessResponse<ExcerciseTypeViewModel>(200, "Excercise Found.", result));
            }
            else
            {
                return BadRequest(new ErrorResponse(400, "Excercise Type not found"));
            }
        }

        // GET: api/ExcerciseTypesByID
        [HttpGet("{PTID}")]
        public async Task<ActionResult<ExcerciseType>> GetExcerciseTypeByPTID(Guid PTID)
        {
            if (PTID.Equals("") || PTID == null)
            {
                return BadRequest(new ErrorResponse(400, "PTID is empty."));
            }
            var result = await _excerciseService.GetExcerciseTypeByPTID(PTID);
            if (result == null)
            {
                return NotFound(new ErrorResponse(204, "No Excercise Found"));
            }
            return Ok(new SuccessResponse<List<ExcerciseTypeViewModel>>(200, "Excercises Found.", result));
        }

        // GET: api/ExcerciseTypesByName
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]//BAD REQUEST
        [ProducesResponseType(StatusCodes.Status200OK)]//OK
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Excercise>>> GetExcercisesByName(string Name)
        {
            var result = await _excerciseService.GetExcerciseTypeByName(Name);
            if (result == null)
            {
                return NotFound(new ErrorResponse(204, "No Excercise Found"));
            }
            return Ok(new SuccessResponse<List<ExcerciseTypeViewModel>>(200, "Excercises Found.", result));
        }

        // PUT: api/ExcerciseTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExcerciseType(Guid id, ExcerciseTypeUpdateViewModel request)
        {
            if (await _excerciseService.UpdateExcerciseType(id, request))
                return Ok(new SuccessResponse<ExcerciseTypeUpdateViewModel>(200, $"Excercise with ID: {id} Updated.", request));
            else
                return BadRequest(new ErrorResponse(400, "Unable to update Excercise"));
        }

        /// <summary>
        /// Create new Excercise Type
        /// </summary>
        /// <param name="excerciseType"></param>
        /// <returns></returns>
        // POST: api/ExcerciseTypes
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]//BAD REQUEST
        [ProducesResponseType(StatusCodes.Status201Created)]//CREATED
        [ProducesResponseType(StatusCodes.Status200OK)]//OK
        public async Task<ActionResult<ExcerciseType>> CreateExcerciseType(ExcerciseTypeCreateViewModel model)
        {
            if (model.Ptid.Equals("") || model.Ptid == null || model.Ptid.Equals("string"))
            {
                return BadRequest(new ErrorResponse(400, "PTID is empty."));
            }
            if (model.Name.Equals("") || model.Name == null || model.Name.Equals("string"))
            {
                return BadRequest(new ErrorResponse(400, "Name is empty."));
            }

            if (await _excerciseService.CreateExcerciseType(model))
            {
                _logger.LogInformation($"Excercise Type Created by PT with ID: {model.Ptid}");
                return Ok(new SuccessResponse<ExcerciseTypeCreateViewModel>(200, "Create Success.", model));
            }
            else
                return BadRequest(new ErrorResponse(400, "Invalid Data"));
        }

        /// <summary>
        /// Delete Excercise Type
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/ExcerciseTypes/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]//BAD REQUEST
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteExcerciseType(Guid id)
        {
            if (await _excerciseService.DeleteExcerciseType(id))
            {
                _logger.LogInformation($"Deleted Excercise Type with ID: {id}");
                return NoContent();
            }
            return BadRequest(new ErrorResponse(400, $"Unable to delete Excercise Type with ID: {id}"));
        }

        private bool ExcerciseTypeExists(Guid id)
        {
            return (_context.ExcerciseTypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
