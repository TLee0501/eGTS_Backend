using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eGTS_Backend.Data.Models;
using eGTS_Backend.Data.ViewModel;
using System.Data;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Net;
using coffee_kiosk_solution.Data.Responses;
using eGTS.Bussiness.AccountService;

namespace eGTS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly EGtsContext _context;
        private readonly ILogger<AccountsController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IAccountService _accountService;

        public AccountsController(EGtsContext context, ILogger<AccountsController> logger, IConfiguration configuration, IAccountService accountService)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
            _accountService = accountService;
        }




        /// <summary>
        /// This function is use to get all accounts in DB with conditions applied
        /// </summary>
        /// <returns> List<Account> </returns>
        // GET: api/Accounts/GetAllAccounts
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NoContent)]//NOT FOUND
        [ProducesResponseType(StatusCodes.Status200OK)]//OK
        public async Task<ActionResult<IEnumerable<Account>>> GetAllAccountsWithConditons(string? role, bool? IsLock)
        {
            var result = await _accountService.GetAllAccountsOtionalRoleAndIsLock(role, IsLock);
            if (result != null)
            {
                return Ok(new SuccessResponse<List<AccountViewModel>>(200, "List of Accounts found", result));
            }
            else
                return NotFound(new ErrorResponse(204, "No Account Found"));
        }

        /*/// <summary>
        /// This function is use to get all accounts in DB
        /// </summary>
        /// <returns> List<Account> </returns>
        // GET: api/Accounts/GetAllAccounts
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NotFound)]//NOT FOUND
        [ProducesResponseType(StatusCodes.Status200OK)]//OK
        public async Task<ActionResult<IEnumerable<Account>>> GetAllAccounts()
        {
            if (_context.Accounts == null)
            {
                return NotFound();
            }
            return await _context.Accounts.ToListAsync();
        }

        /// <summary>
        /// This function is use to get all accounts with IsLock Condition appllied in DB
        /// </summary>
        /// <param name="IsLock"></param>
        /// <returns> List<Account> </returns>
        // GET: api/Accounts/GetAllAccountsWithStatus
        [HttpGet("{IsLock}")]
        [ProducesResponseType(StatusCodes.Status204NotFound)]//NOT FOUND
        [ProducesResponseType(StatusCodes.Status200OK)]//OK
        public async Task<ActionResult<IEnumerable<Account>>> GetAllAccountsWithStatus(bool IsLock)
        {
            if (_context.Accounts == null)
            {
                return NotFound();
            }

            if (IsLock)
                return await _context.Accounts.Where(a => a.IsLock == true).ToListAsync();
            else
                return await _context.Accounts.Where(a => a.IsLock == false).ToListAsync();

        }

        /// <summary>
        /// This function is use to get all staff accounts in DB
        /// </summary>
        /// <returns> List<Account> </returns>
        // GET: api/Accounts/GetAllStaffAccounts
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NotFound)]//NOT FOUND
        [ProducesResponseType(StatusCodes.Status200OK)]//OK
        public async Task<ActionResult<IEnumerable<Account>>> GetAllStaffAccounts()
        {
            if (_context.Accounts == null)
            {
                return NotFound();
            }

            return await _context.Accounts.Where(a => a.Role.Equals("Staff")).ToListAsync();
        }

        /// <summary>
        /// This function is use to get all Staff accounts with IsLock Condition appllied in DB
        /// </summary>
        /// <param name="IsLock"></param>
        /// <returns> List<Account> </returns>
        // GET: api/Accounts/GetAllStaffAccountsWithStatus
        [HttpGet("{IsLock}")]
        [ProducesResponseType(StatusCodes.Status204NotFound)]//NOT FOUND
        [ProducesResponseType(StatusCodes.Status200OK)]//OK
        public async Task<ActionResult<IEnumerable<Account>>> GetAllStaffAccountsWithStatus(bool IsLock)
        {
            if (_context.Accounts == null)
            {
                return NotFound();
            }

            if (IsLock)
                return await _context.Accounts.Where(a => a.IsLock == true && a.Role.Equals("Staff")).ToListAsync();
            else
                return await _context.Accounts.Where(a => a.IsLock == false && a.Role.Equals("Staff")).ToListAsync();

        }

        /// <summary>
        /// This function is use to get all PT accounts in DB
        /// </summary>
        /// <returns> List<Account> </returns>
        // GET: api/Accounts/GetAllPTAccounts
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NotFound)]//NOT FOUND
        [ProducesResponseType(StatusCodes.Status200OK)]//OK
        public async Task<ActionResult<IEnumerable<Account>>> GetAllPTAccounts()
        {
            if (_context.Accounts == null)
            {
                return NotFound();
            }

            return await _context.Accounts.Where(a => a.Role.Equals("PT")).ToListAsync();
        }

        /// <summary>
        /// This function is use to get all PT accounts with IsLock Condition appllied in DB
        /// </summary>
        /// <param name="IsLock"></param>
        /// <returns> List<Account> </returns>
        // GET: api/Accounts/GetAllPTAccountsWithIsLock
        [HttpGet("{IsLock}")]
        [ProducesResponseType(StatusCodes.Status204NotFound)]//NOT FOUND
        [ProducesResponseType(StatusCodes.Status200OK)]//OK
        public async Task<ActionResult<IEnumerable<Account>>> GetAllPTAccountsWithIsLock(bool IsLock)
        {
            if (_context.Accounts == null)
            {
                return NotFound();
            }

            if (IsLock)
                return await _context.Accounts.Where(a => a.IsLock == true && a.Role.Equals("PT")).ToListAsync();
            else
                return await _context.Accounts.Where(a => a.IsLock == false && a.Role.Equals("PT")).ToListAsync();

        }

        /// <summary>
        /// This function is use to get all NE accounts in DB
        /// </summary>
        /// <returns> List<Account> </returns>
        // GET: api/Accounts/GetAllPTAccounts
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NotFound)]//NOT FOUND
        [ProducesResponseType(StatusCodes.Status200OK)]//OK
        public async Task<ActionResult<IEnumerable<Account>>> GetAllNEAccounts()
        {
            if (_context.Accounts == null)
            {
                return NotFound();
            }

            return await _context.Accounts.Where(a => a.Role.Equals("NE")).ToListAsync();
        }

        /// <summary>
        /// This function is use to get all NE accounts with IsLock Condition appllied in DB
        /// </summary>
        /// <param name="IsLock"></param>
        /// <returns> List<Account> </returns>
        // GET: api/Accounts/GetAllNEAccountsWithIsLock
        [HttpGet("{IsLock}")]
        [ProducesResponseType(StatusCodes.Status204NotFound)]//NOT FOUND
        [ProducesResponseType(StatusCodes.Status200OK)]//OK
        public async Task<ActionResult<IEnumerable<Account>>> GetAllNEAccountsWithIsLock(bool IsLock)
        {
            if (_context.Accounts == null)
            {
                return NotFound();
            }

            if (IsLock)
                return await _context.Accounts.Where(a => a.IsLock == true && a.Role.Equals("NE")).ToListAsync();
            else
                return await _context.Accounts.Where(a => a.IsLock == false && a.Role.Equals("NE")).ToListAsync();

        }

        /// <summary>
        /// This function is use to get all Gymer accounts in DB
        /// </summary>
        /// <returns> List<Account> </returns>
        // GET: api/Accounts/GetAllPTAccounts
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NotFound)]//NOT FOUND
        [ProducesResponseType(StatusCodes.Status200OK)]//OK
        public async Task<ActionResult<IEnumerable<Account>>> GetAllGymerAccounts()
        {
            if (_context.Accounts == null)
            {
                return NotFound();
            }

            return await _context.Accounts.Where(a => a.Role.Equals("Gymer")).ToListAsync();
        }

        /// <summary>
        /// This function is use to get all NE accounts with IsLock Condition appllied in DB
        /// </summary>
        /// /// <param name="IsLock"></param>
        /// <returns> List<Account> </returns>
        // GET: api/Accounts/GetAllGymerAccountsWithIsLock
        [HttpGet("{IsLock}")]
        [ProducesResponseType(StatusCodes.Status204NotFound)]//NOT FOUND
        [ProducesResponseType(StatusCodes.Status200OK)]//OK
        public async Task<ActionResult<IEnumerable<Account>>> GetAllGymerAccountsWithIsLock(bool IsLock)
        {
            if (_context.Accounts == null)
            {
                return NotFound();
            }

            if (IsLock)
                return await _context.Accounts.Where(a => a.IsLock == true && a.Role.Equals("Gymer")).ToListAsync();
            else
                return await _context.Accounts.Where(a => a.IsLock == false && a.Role.Equals("Gymer")).ToListAsync();

        }*/

        /// <summary>
        /// This Function is use to get Account by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Account</returns>
        // GET: api/Accounts/GetAccountByID
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]//NO Content
        [ProducesResponseType(StatusCodes.Status200OK)]//OK
        public async Task<ActionResult<AccountViewModel>> GetAccountByID(Guid id)
        {
            var result = await _accountService.GetAccountByID(id);
            if (result == null)
                return NotFound(new ErrorResponse(400, "Account ID Not Found"));
            else
                return result;
        }

        /// <summary>
        /// This Function is use to search for accounts by Phone Number
        /// </summary>
        /// <param name="PhoneNo"></param>
        /// <returns>List<Account></returns>
        // GET: api/Accounts/SearchAccountByPhoneNo
        [HttpGet("{PhoneNo}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]//NOT FOUND
        [ProducesResponseType(StatusCodes.Status400BadRequest)]//BAD REQUEST
        [ProducesResponseType(StatusCodes.Status200OK)]//OK
        public async Task<ActionResult<IEnumerable<AccountViewModel>>> SearchAccountByPhoneNo(string PhoneNo)
        {
            if (PhoneNo == null)
                return BadRequest(new ErrorResponse(400, "PhoneNo is empty."));

            var result = await _accountService.SearchAccountByPhoneNo(PhoneNo);

            if (result.Count == 0)
                return NotFound(new ErrorResponse(204, "No account found"));

            return result;

        }
        /// <summary>
        /// This Function is use to search for accounts by Name
        /// </summary>
        /// <param name="Fullname"></param>
        /// <returns>List<Account></returns>
        // GET: api/Accounts/SearchAccountByName
        [HttpGet("{Fullname}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]//NOT FOUND
        [ProducesResponseType(StatusCodes.Status400BadRequest)]//BAD REQUEST
        [ProducesResponseType(StatusCodes.Status200OK)]//OK
        public async Task<ActionResult<IEnumerable<AccountViewModel>>> SearchAccountByName(string Fullname)
        {
            if (Fullname == null)
                return BadRequest(new ErrorResponse(400, "Fullname is empty."));

            var result = await _accountService.SearchAccountByName(Fullname);

            if (result.Count == 0)
                return NotFound(new ErrorResponse(204, "No account found"));

            return result;

        }

        /// <summary>
        /// Update Account 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        // PUT: api/Accounts/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]//BAD REQUEST
        [ProducesResponseType(StatusCodes.Status204NotFound)]//NOT FOUND
        [ProducesResponseType(StatusCodes.Status200OK)]//OK
        public async Task<IActionResult> UpdateAccount(Guid id, AccountUpdateViewModel request)
        {



            if (!request.PhoneNo.Equals("") && !PhoneNoIsValid(request.PhoneNo))

                return BadRequest(new ErrorResponse(400, "Invalid Phone Numer"));

            if (PhoneNoExists(request.PhoneNo))

                return BadRequest(new ErrorResponse(400, "Phone Numer in use"));

            if (request.Gender == null)

                return BadRequest(new ErrorResponse(400, "Invalid Gender"));



            if (!request.Role.Equals("PT") && !request.Role.Equals("NE") && !request.Role.Equals("Gymer") && !request.Role.Equals("Staff") && !request.Role.Equals("") && !request.PhoneNo.Equals("string"))
                return BadRequest(new ErrorResponse(400, "Invalid Role"));

            if (request.IsLock == null)
            {
                return BadRequest(new ErrorResponse(400, "Invalid Lock State"));
            }

            if (await _accountService.UpdateAccount(id, request))
            {
                _logger.LogInformation($"Update Account with ID: {id}");
                return Ok(new SuccessResponse<AccountUpdateViewModel>(200, "Update Success.", request));
            }
            else
            {
                return BadRequest(new ErrorResponse(400, "Unable to update Account"));
            }



        }

        /// <summary>
        /// Create new Account
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        // POST: api/Accounts/CreateAccount
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]//BAD REQUEST
        [ProducesResponseType(StatusCodes.Status201Created)]//CREATED
        [ProducesResponseType(StatusCodes.Status200OK)]//OK
        public async Task<ActionResult<Account>> CreateAccount(AccountCreateViewModel model)
        {
            if (model.PhoneNo.Equals("") || model.Password.Equals(""))
                return BadRequest(new ErrorResponse(400, "PhoneNumer Or password is empty."));
            if (!PhoneNoIsValid(model.PhoneNo))
                return BadRequest(new ErrorResponse(400, "PhoneNumer is invalid."));
            if (PhoneNoExists(model.PhoneNo))
                return BadRequest(new ErrorResponse(400, "PhoneNumer is in use."));


            if (await _accountService.CreateAccount(model))
            {
                _logger.LogInformation($"Created Account with Phone number: {model.PhoneNo}");
                return Ok(new SuccessResponse<AccountCreateViewModel>(200, "Create Success.", model));
            }
            else
                return BadRequest(new ErrorResponse(400, "Invalid Data"));
        }

        /// <summary>
        /// Delete Account by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Accounts/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteAccount(Guid id)
        {
            if (await _accountService.DeleteAccount(id))
            {
                _logger.LogInformation($"Deleted Account with ID: {id}");
                return NoContent();
            }
            else
            {
                return NotFound(new ErrorResponse(204, "Account Not Found In DataBase"));
            }

        }


        // check if ID in use
        private bool AccountExists(Guid id)
        {
            return (_context.Accounts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        // check if PhoneNo in use
        private bool PhoneNoExists(string PhoneNo)
        {
            return (_context.Accounts?.Any(a => a.PhoneNo.Equals(PhoneNo))).GetValueOrDefault();
        }
        // Check if PhoneNo is valid
        private bool PhoneNoIsValid(string PhoneNo)
        {
            return Regex.IsMatch(PhoneNo, @"^\d{9,11}$");
        }
    }
}
