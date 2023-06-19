using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using eGTS_Backend.Data.Models;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using eGTS_Backend.Data.Repositories;

namespace eGTS.Controllers
{
    [Route("api/LoginController")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        IAccountRepository accountRepository = new AccountRepository();

        [HttpGet(Name = "Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]//OK
        [ProducesResponseType(StatusCodes.Status404NotFound)]//NOT FOUND
        [ProducesResponseType(StatusCodes.Status400BadRequest)]//BAD REQUEST
        public async Task<ActionResult<IEnumerable<Account>>> Login(string PhoneNo, String Password)
        {
            if (PhoneNo == "")
                return BadRequest();
            if (Password == "")
                return BadRequest();
            Account account = accountRepository.Login(PhoneNo, Password);
            if (account == null)
                return NotFound();
            return Ok(account);
        }
    }
}
