using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using eGTS_Backend.Data.Models;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using eGTS.Bussiness.LoginService;
using Azure.Core;
using coffee_kiosk_solution.Data.Responses;
using eGTS_Backend.Data.ViewModel;

namespace eGTS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly ILogger<LoginController> _logger;
        private IConfiguration _configuration;

        private readonly EGtsContext _context;
        /*public LoginController(EGtsContext context)
        {
            _context = context;
        }*/

        public LoginController(ILoginService loginService, ILogger<LoginController> logger, IConfiguration configuration)
        {
            _loginService = loginService;
            _logger = logger;
            _configuration = configuration;
        }



        // GET: api/Login
        /// <summary>
        /// This function is the Login Function
        /// </summary>
        /// <param name="PhoneNo"></param>
        /// <param name="Password"></param>
        /// <returns>IEnumerable<Account></returns>
        [HttpPost(Name = "Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]//OK
        [ProducesResponseType(StatusCodes.Status404NotFound)]//NOT FOUND
        [ProducesResponseType(StatusCodes.Status400BadRequest)]//BAD REQUEST
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]//UNAUTHORIZED REQUEST
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]//NOT FOUND
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var result = await _loginService.Login(model);
            _logger.LogInformation($"Login by {model.PhoneNo}");
            return Ok(new SuccessResponse<AccountViewModel>(200, "Login Success.", result));
        }
        /*// GET: api/Login
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
        }*/
    }
}
