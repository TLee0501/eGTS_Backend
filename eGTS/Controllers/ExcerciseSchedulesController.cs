using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eGTS_Backend.Data.Models;

namespace eGTS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExcerciseSchedulesController : ControllerBase
    {
        private readonly EGtsContext _context;

        public ExcerciseSchedulesController(EGtsContext context)
        {
            _context = context;
        }

        // GET: api/ExcerciseSchedules
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExcerciseSchedule>>> GetExcerciseSchedules()
        {
          if (_context.ExcerciseSchedules == null)
          {
              return NotFound();
          }
            return await _context.ExcerciseSchedules.ToListAsync();
        }

        // GET: api/ExcerciseSchedules/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExcerciseSchedule>> GetExcerciseSchedule(Guid id)
        {
          if (_context.ExcerciseSchedules == null)
          {
              return NotFound();
          }
            var excerciseSchedule = await _context.ExcerciseSchedules.FindAsync(id);

            if (excerciseSchedule == null)
            {
                return NotFound();
            }

            return excerciseSchedule;
        }

        // PUT: api/ExcerciseSchedules/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExcerciseSchedule(Guid id, ExcerciseSchedule excerciseSchedule)
        {
            if (id != excerciseSchedule.Id)
            {
                return BadRequest();
            }

            _context.Entry(excerciseSchedule).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExcerciseScheduleExists(id))
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

        // POST: api/ExcerciseSchedules
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ExcerciseSchedule>> PostExcerciseSchedule(ExcerciseSchedule excerciseSchedule)
        {
          if (_context.ExcerciseSchedules == null)
          {
              return Problem("Entity set 'EGtsContext.ExcerciseSchedules'  is null.");
          }
            _context.ExcerciseSchedules.Add(excerciseSchedule);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ExcerciseScheduleExists(excerciseSchedule.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetExcerciseSchedule", new { id = excerciseSchedule.Id }, excerciseSchedule);
        }

        // DELETE: api/ExcerciseSchedules/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExcerciseSchedule(Guid id)
        {
            if (_context.ExcerciseSchedules == null)
            {
                return NotFound();
            }
            var excerciseSchedule = await _context.ExcerciseSchedules.FindAsync(id);
            if (excerciseSchedule == null)
            {
                return NotFound();
            }

            _context.ExcerciseSchedules.Remove(excerciseSchedule);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExcerciseScheduleExists(Guid id)
        {
            return (_context.ExcerciseSchedules?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
