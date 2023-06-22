using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using eGTS_Backend.Data.Models;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using eGTS_Backend.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace eGTS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly EGtsContext _context;
        public LoginController(EGtsContext context)
        {
            _context = context;
        }

        // GET: api/Login
        /// <summary>
        /// This function is the Login Function
        /// </summary>
        /// <param name="PhoneNo"></param>
        /// <param name="Password"></param>
        /// <returns>IEnumerable<Account></returns>
        [HttpGet(Name = "Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]//OK
        [ProducesResponseType(StatusCodes.Status404NotFound)]//NOT FOUND
        [ProducesResponseType(StatusCodes.Status400BadRequest)]//BAD REQUEST
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]//UNAUTHORIZED REQUEST
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]//NOT FOUND
        public async Task<ActionResult<IEnumerable<Account>>> Login(string PhoneNo, String Password)
        {
            if (PhoneNo == "")
                return BadRequest();
            if (Password == "")
                return BadRequest();
            var account = await _context.Accounts.SingleOrDefaultAsync(a => a.PhoneNo == PhoneNo && a.Password == Password);

            if (account == default)
                return NotFound();
            else if (account.IsLock == true)
                return Unauthorized();

            return Ok(account);
        }
    }
}
