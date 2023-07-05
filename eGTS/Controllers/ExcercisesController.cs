using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eGTS_Backend.Data.Models;
using eGTS.Bussiness.AccountService;
using eGTS_Backend.Data.ViewModel;
using coffee_kiosk_solution.Data.Responses;
using eGTS.Bussiness.ExcerciseService;

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
        public async Task<ActionResult<IEnumerable<Excercise>>> GetAllExcercises()
        {
            var result = await _excerciseService.GetAllExcercise();
            if (result == null)
            {
                return NotFound(new ErrorResponse(204, "No Excercise Found"));
            }
            return Ok(new SuccessResponse<List<ExcerciseViewModel>>(200, "Excercises Found.", result));
        }

        /// <summary>
        /// Get Excercise by PTID
        /// </summary>
        /// <param name="PTID"></param>
        /// <returns></returns>
        // GET: api/Excercises
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]//BAD REQUEST
        [ProducesResponseType(StatusCodes.Status200OK)]//OK
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ExcerciseViewModel>>> GetExcercisesByPTID(Guid PTID)
        {
            if (PTID.Equals("") || PTID == null)
            {
                return BadRequest(new ErrorResponse(400, "PTID is empty."));
            }
            var result = await _excerciseService.GetExcerciseByPTID(PTID);
            if (result == null)
            {
                return NotFound(new ErrorResponse(204, "No Excercise Found"));
            }
            return Ok(new SuccessResponse<List<ExcerciseViewModel>>(200, "Excercises Found.", result));
        }

        /// <summary>
        /// Get Excercise by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Excercises/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Excercise>> GetExcerciseByID(Guid id)
        {
            if (_context.Excercises == null)
            {
                return NotFound();
            }
            var excercise = await _context.Excercises.FindAsync(id);

            if (excercise == null)
            {
                return NotFound();
            }

            return excercise;
        }

        // PUT: api/Excercises/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExcercise(Guid id, Excercise excercise)
        {
            if (id != excercise.Id)
            {
                return BadRequest();
            }

            _context.Entry(excercise).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExcerciseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
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
        public async Task<ActionResult<Excercise>> CreateExcercise(ExcerciseCreateViewModel model)
        {
            if (model.Ptid.Equals("") || model.Ptid == null || model.Ptid.Equals("string"))
            {
                return BadRequest(new ErrorResponse(400, "PTID is empty."));
            }
            if (model.Name.Equals("") || model.Name == null || model.Name.Equals("string"))
            {
                return BadRequest(new ErrorResponse(400, "PTID is empty."));
            }

            if (await _excerciseService.CreateExcercise(model))
            {
                _logger.LogInformation($"Excercise Created by PT with ID: {model.Ptid}");
                return Ok(new SuccessResponse<ExcerciseCreateViewModel>(200, "Create Success.", model));
            }
            else
                return BadRequest(new ErrorResponse(400, "Invalid Data"));

        }

        // DELETE: api/Excercises/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]//BAD REQUEST
        [ProducesResponseType(StatusCodes.Status204NoContent)]//OK
        public async Task<IActionResult> DeleteExcercise(Guid id)
        {
            if (await _excerciseService.DeleteExcercise(id))
            {
                _logger.LogInformation($"Deleted Excercise with ID: {id}");
                return NoContent();
            }
            return BadRequest(new ErrorResponse(400, $"Unable to delete Excercise with ID: {id}"));

        }

        private bool ExcerciseExists(Guid id)
        {
            return (_context.Excercises?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
