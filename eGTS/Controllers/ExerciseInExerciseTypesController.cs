using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eGTS_Backend.Data.Models;
using eGTS_Backend.Data.ViewModel;
using coffee_kiosk_solution.Data.Responses;
using eGTS.Bussiness.ExcerciseService;
using Azure.Core;

namespace eGTS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ExerciseInExerciseTypesController : ControllerBase
    {
        private readonly EGtsContext _context;
        private readonly ILogger<AccountsController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IExcerciseService _excerciseService;

        public ExerciseInExerciseTypesController(EGtsContext context, ILogger<AccountsController> logger, IConfiguration configuration, IExcerciseService excerciseService)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
            _excerciseService = excerciseService;
        }

        // GET: api/ExerciseInExerciseTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExerciseInExerciseType>>> GetAllExerciseInExerciseTypes()
        {
            var result = await _excerciseService.GetAllExcerciseInType();
            if (result == null)
            {
                return NotFound(new ErrorResponse(204, "No Data Found"));
            }
            return Ok(new SuccessResponse<List<ExcerciseInTypeViewModel>>(200, "Data Found.", result));
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExerciseInExerciseType(Guid id, ExcerciseInTypeUpdateViewModel request)
        {
            if (await _excerciseService.UpdateExcerciseInType(id, request))
                return Ok(new SuccessResponse<ExcerciseInTypeUpdateViewModel>(200, $"Data with ID: {id} Updated.", request));
            else
                return BadRequest(new ErrorResponse(400, "Unable to update Data"));
        }

        // POST: api/ExerciseInExerciseTypes
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]//BAD REQUEST
        [ProducesResponseType(StatusCodes.Status201Created)]//CREATED
        [ProducesResponseType(StatusCodes.Status200OK)]//OK
        public async Task<ActionResult<ExerciseInExerciseType>> CreateExerciseInType(ExcerciseInTypeCreateViewModel model)
        {
            if (model.ExerciseTypeId.Equals("") || model.ExerciseTypeId == null)
            {
                return BadRequest(new ErrorResponse(400, "Type ID is empty."));
            }
            if (model.ExerciseId.Equals("") || model.ExerciseId == null)
            {
                return BadRequest(new ErrorResponse(400, "Excercise ID is empty."));
            }
            if (await _excerciseService.CreateExcerciseInType(model))
            {
                _logger.LogInformation($"Excercise Added to type with ID:{model.ExerciseTypeId}");
                return Ok(new SuccessResponse<ExcerciseInTypeCreateViewModel>(200, "Create Success.", model));
            }
            else
                return BadRequest(new ErrorResponse(400, "Invalid Data"));
        }

        // DELETE: api/ExerciseInExerciseTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExerciseInExerciseType(Guid id)
        {
            if (await _excerciseService.DeleteExcerciseInType(id))
            {
                _logger.LogInformation($"Deleted Excercise with ID: {id}");
                return NoContent();
            }
            return BadRequest(new ErrorResponse(400, $"Unable to delete Excercise with ID: {id}"));
        }

        private bool ExerciseInExerciseTypeExists(Guid id)
        {
            return (_context.ExerciseInExerciseTypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
