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
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NutritionSchedulesController : ControllerBase
    {
        private readonly EGtsContext _context;

        public NutritionSchedulesController(EGtsContext context)
        {
            _context = context;
        }

        // GET: api/NutritionSchedules
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NutritionSchedule>>> GetNutritionSchedulesForTest()
        {
          if (_context.NutritionSchedules == null)
          {
              return NotFound();
          }
            return await _context.NutritionSchedules.ToListAsync();
        }

        // GET: api/NutritionSchedules/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NutritionSchedule>> GetNutritionSchedule(Guid id)
        {
          if (_context.NutritionSchedules == null)
          {
              return NotFound();
          }
            var nutritionSchedule = await _context.NutritionSchedules.FindAsync(id);

            if (nutritionSchedule == null)
            {
                return NotFound();
            }

            return nutritionSchedule;
        }

        // PUT: api/NutritionSchedules/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*[HttpPut("{id}")]
        public async Task<IActionResult> UpdateNutritionSchedule(Guid id, NutritionSchedule nutritionSchedule)
        {
            if (id != nutritionSchedule.Id)
            {
                return BadRequest();
            }

            _context.Entry(nutritionSchedule).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NutritionScheduleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }*/

        // POST: api/NutritionSchedules
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*[HttpPost]
        public async Task<ActionResult<NutritionSchedule>> CreateNutritionSchedule(NutritionSchedule nutritionSchedule)
        {
          if (_context.NutritionSchedules == null)
          {
              return Problem("Entity set 'EGtsContext.NutritionSchedules'  is null.");
          }
            _context.NutritionSchedules.Add(nutritionSchedule);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (NutritionScheduleExists(nutritionSchedule.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetNutritionSchedule", new { id = nutritionSchedule.Id }, nutritionSchedule);
        }*/

        // DELETE: api/NutritionSchedules/5
        /*[HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNutritionSchedule(Guid id)
        {
            if (_context.NutritionSchedules == null)
            {
                return NotFound();
            }
            var nutritionSchedule = await _context.NutritionSchedules.FindAsync(id);
            if (nutritionSchedule == null)
            {
                return NotFound();
            }

            _context.NutritionSchedules.Remove(nutritionSchedule);
            await _context.SaveChangesAsync();

            return NoContent();
        }*/

        /*private bool NutritionScheduleExists(Guid id)
        {
            return (_context.NutritionSchedules?.Any(e => e.Id == id)).GetValueOrDefault();
        }*/
    }
}
