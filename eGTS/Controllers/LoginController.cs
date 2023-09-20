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
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.IdentityModel.JsonWebTokens;
using System.IdentityModel.Tokens.Jwt;
using eGTS_Backend.Data.Responses;
using Microsoft.AspNetCore.Authorization;

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

        public LoginController(EGtsContext context, ILoginService loginService, ILogger<LoginController> logger, IConfiguration configuration)
        {
            _context = context;
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]//NO Content
        [ProducesResponseType(StatusCodes.Status400BadRequest)]//BAD REQUEST
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]//UNAUTHORIZED REQUEST
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]//NOT FOUND
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (model.PhoneNo == "")
                return BadRequest(new ErrorResponse(400, "Vui lòng kiểm tả lại số điện thoại!"));
            if (model.Password == "")
                return BadRequest(new ErrorResponse(400, "Vui lòng kiểm tả lại mật khẩu!"));

            var result = await _loginService.Login(model);

            if (result == null)
                return NotFound(new ErrorResponse(204, "Vui lòng kiểm tra lại số điện thoại/mật khẩu!"));
            else if (result.IsDelete == true)
                return Unauthorized(new ErrorResponse(401, "Tài khoản đã bị khóa!"));


            _logger.LogInformation($"Login by {model.PhoneNo}");

            var token = CreateToken(result);
            return Ok(new LoginResponse<AccountViewModel>(200, "Đăng nhập thành công.", result, token));
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
        [ProducesResponseType(StatusCodes.Status204NotFound)]//NOT FOUND
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

        private string CreateToken(AccountViewModel request)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, request.PhoneNo),
                new Claim(ClaimTypes.Role, request.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Appsettings:Token").Value!));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(15),
                signingCredentials: cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        [HttpGet, Authorize]
        public ActionResult<string> getPhoneNoAndID()
        {
            var roleClaim = User?.FindAll(ClaimTypes.Name);
            var phoneNo = roleClaim?.Select(c => c.Value).SingleOrDefault().ToString();
            if (phoneNo == null)
            {
                return Ok(new { phoneNo, V = "Null" });
            }
            var id = _context.Accounts.SingleOrDefault(x => x.PhoneNo.Equals(phoneNo))?.Id.ToString();
            return Ok(new { phoneNo, id });
        }
    }
}
