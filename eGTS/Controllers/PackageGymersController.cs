using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eGTS_Backend.Data.Models;
using eGTS_Backend.Data.ViewModel;
using eGTS.Bussiness.PackageGymersService;
using coffee_kiosk_solution.Data.Responses;
using System.Net;
using Microsoft.AspNetCore.Http.HttpResults;

namespace eGTS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PackageGymersController : ControllerBase
    {
        private readonly EGtsContext _context;
        private readonly IPackageGymersService _packageGymersService;

        public PackageGymersController(EGtsContext context, IPackageGymersService packageGymersService)
        {
            _context = context;
            _packageGymersService = packageGymersService;
        }

        // GET: api/PackageGymers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PackageGymer>>> GetPackageGymersForTest()
        {
          if (_context.PackageGymers == null)
          {
              return NotFound();
          }
            return await _context.PackageGymers.ToListAsync();
        }

        // GET: api/PackageGymers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PackageGymer>> GetPackageGymer(Guid id)
        {
          if (_context.PackageGymers == null)
          {
              return NotFound();
          }
            var packageGymer = await _context.PackageGymers.FindAsync(id);

            if (packageGymer == null)
            {
                return NotFound();
            }

            return packageGymer;
        }

        // PUT: api/PackageGymers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*[HttpPut]
        public async Task<IActionResult> UpdatePackageGymer(PackageGymerViewModel request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            try
            {
                await _packageGymersService.UpdatePackageGymer(request);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return Ok();
        }*/

        // POST: api/PackageGymers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<bool>> CreatePackageGymer(PackageGymerCreateViewModel request)
        {
            if (request == null) return BadRequest();
            try
            {
                await _packageGymersService.CreatePackageGymer(request);
            }
            catch (DbUpdateConcurrencyException) { return BadRequest(); }
            return Ok();
        }

        // DELETE: api/PackageGymers/5
        /* [HttpDelete("{id}")]
         public async Task<IActionResult> DeletePackageGymer(Guid id)
         {
             if (_context.PackageGymers == null)
             {
                 return NotFound();
             }
             var packageGymer = await _context.PackageGymers.FindAsync(id);
             if (packageGymer == null)
             {
                 return NotFound();
             }

             _context.PackageGymers.Remove(packageGymer);
             await _context.SaveChangesAsync();

             return NoContent();
         }*/

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PackageGymerViewModel>>> GetPackageGymerByGymerID(Guid request)
        {
            var data = await _packageGymersService.GetPackageGymerByGymerID(request);
            if (data == null) return NoContent();
            return Ok(new SuccessResponse<List<PackageGymerViewModel>>(200, "List of PackageGymer found", data)); ;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GymerPackageActiveViewModel>>> GetGymerPackageActiveByNE(Guid NEID)
        {
            try
            {
                var result = await _packageGymersService.GetGymerPackageActiveByNE(NEID);
                return Ok(result);
            }
            catch { return BadRequest(); }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GymerPackageActiveViewModel>>> GetGymerPackageActiveByPT(Guid PTID)
        {
            try
            {
                var result = await _packageGymersService.GetGymerPackageActiveByPT(PTID);
                return Ok(result);
            }
            catch { return BadRequest(); }
        }
    }
}
