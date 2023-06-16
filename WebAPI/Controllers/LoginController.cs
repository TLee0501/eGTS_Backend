using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Services.Accounts;

namespace WebAPI.Controllers
{
    [Route("api/Web/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public LoginController(IAccountService accountService)
        {
            _accountService = accountService;
        }


        [HttpGet]
        public async Task<ActionResult<List<Account>>> GetAccounts()
        {
            return await _accountService.GetAccounts();
        }
    }
}
