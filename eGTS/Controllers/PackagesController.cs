using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eGTS_Backend.Data.Models;
using eGTS.Bussiness.PackageService;
using coffee_kiosk_solution.Data.Responses;
using eGTS_Backend.Data.ViewModel;
using System.Data;
using System.Runtime.CompilerServices;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using System.Collections;

namespace eGTS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PackagesController : ControllerBase
    {
        private readonly EGtsContext _context;
        private readonly IPackageService _packageService;

        public PackagesController(EGtsContext context, IPackageService packageService)
        {
            _context = context;
            _packageService = packageService;
        }

        // GET: api/Packages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PackageViewModel>>> GetPackages()
        {
            var result = await _packageService.GetPackages();
            if (result != null)
            {
                return Ok(new SuccessResponse<List<PackageViewModel>>(200, "List of Packages found", result));
            }
            else
                return StatusCode(204);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PackageMobileViewModel>>> GetPackagesForMobile()
        {
            var result = await _packageService.GetPackagesForMobile();
            if (result != null)
            {
                return Ok(new SuccessResponse<List<PackageMobileViewModel>>(200, "List of Packages found", result));
            }
            else
                return StatusCode(204);
        }

        // GET: api/Packages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Package>> GetPackage(Guid id)
        {
            var result = await _packageService.GetPackage(id);
            if (result != null)
            {
                return Ok(new SuccessResponse<PackageViewModel>(200, "Packages found", result));
            }
            else
                return NotFound(new ErrorResponse(404, "No Package Found"));

        }

        // PUT: api/Packages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> UpdatePackage(PackageViewModel package)
        {
            if (package == null) return BadRequest();
            try
            {
                var result = await _packageService.UpdatePackage(package);
                return StatusCode(200);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
                return StatusCode(400);
            }
        }

        // POST: api/Packages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Package>> CreatePackage(PackageCreateViewModel package)
        {
            /*if (_context.Packages == null)
            {
                return Problem("Entity set 'EGtsContext.Packages'  is null.");
            }*/
            if (package == null) return BadRequest();
            try
            {
                var result = await _packageService.CreatePackage(package);
                return StatusCode(200);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
                return StatusCode(400);
            }
        }

        // DELETE: api/Packages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePackage(Guid id)
        {
            if (id == null) return BadRequest();
            var result = await _packageService.DeletePackage(id);
            if (result == true) return StatusCode(200);
            else
            {
                return StatusCode(400);
            }
        }

    }
}
