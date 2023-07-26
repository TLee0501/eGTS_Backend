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
using Microsoft.Identity.Client;

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
        public async Task<ActionResult<IEnumerable<AccountViewModel>>> GetAllAccountsWithConditons(string? role, bool? IsLock)
        {
            var result = await _accountService.GetAllAccountsOtionalRoleAndIsLock(role, IsLock);
            if (result != null)
            {
                return Ok(new SuccessResponse<List<AccountViewModel>>(200, "Danh sách các Tài Khoản", result));
            }
            else
                return NoContent();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]//NO Content
        [ProducesResponseType(StatusCodes.Status200OK)]//OK
        public async Task<ActionResult<AccountViewModel>> GetAccountByID(Guid id)
        {
            var result = await _accountService.GetAccountByID(id);
            if (result == null)
                return NoContent();
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
                return BadRequest(new ErrorResponse(400, "SDT đang bị bỏ trống."));

            var result = await _accountService.SearchAccountByPhoneNo(PhoneNo);

            if (result.Count == 0)
                return NoContent();

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
                return BadRequest(new ErrorResponse(400, "Họ và tên đang bị bỏ trống."));

            var result = await _accountService.SearchAccountByName(Fullname);

            if (result.Count == 0)
                return NoContent();

            return result;

        }

        /// <summary>
        /// Update Account 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        // PUT: api/Accounts/5
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]//BAD REQUEST
        [ProducesResponseType(StatusCodes.Status204NoContent)]//NOT FOUND
        [ProducesResponseType(StatusCodes.Status200OK)]//OK
        public async Task<IActionResult> UpdateAccount(Guid id, AccountUpdateViewModel request)
        {



            if (!request.PhoneNo.Equals("") && !PhoneNoIsValid(request.PhoneNo))

                return BadRequest(new ErrorResponse(400, "SDT không đúng cú pháp."));

            if (PhoneNoExists(request.PhoneNo))

                return BadRequest(new ErrorResponse(400, "SDT đã tồn tại."));

            if (request.Gender == null)

                return BadRequest(new ErrorResponse(400, "Giới tính sai"));



            if (!request.Role.Equals("PT") && !request.Role.Equals("NE") && !request.Role.Equals("Gymer") && !request.Role.Equals("Staff") && !request.Role.Equals("") && !request.PhoneNo.Equals("string"))
                return BadRequest(new ErrorResponse(400, "Chức vụ sai"));

            if (request.IsDelete == null)
            {
                return BadRequest(new ErrorResponse(400, "Trạng thái xóa sai"));
            }

            if (await _accountService.UpdateAccount(id, request))
            {
                _logger.LogInformation($"Update Account with ID: {id}");
                return Ok(new SuccessResponse<AccountUpdateViewModel>(200, "Update thành công.", request));
            }
            else
            {
                return BadRequest(new ErrorResponse(400, "Không thể update tài khoản"));
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
        public async Task<ActionResult<Guid>> CreateAccount(AccountCreateViewModel model)
        {
            if (model.PhoneNo.Equals("") || model.Password.Equals(""))
                return BadRequest(new ErrorResponse(400, "SDT Or mật khẩu đang bị bỏ trống."));
            if (!PhoneNoIsValid(model.PhoneNo))
                return BadRequest(new ErrorResponse(400, "SDT sai cú pháp."));
            if (PhoneNoExists(model.PhoneNo))
                return BadRequest(new ErrorResponse(400, "SDT đang được sử dụng."));

            var id = await _accountService.CreateAccount(model);
            if (id != Guid.Empty)
            {
                _logger.LogInformation($"Created Account with Phone number: {model.PhoneNo}");
                return Ok(id);
            }
            else
                return BadRequest(new ErrorResponse(400, "Dữ liệu bị sai"));
        }

        /// <summary>
        /// PERMANENT Delete Account by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Accounts/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteAccountPERMANENT(Guid id)
        {
            if (await _accountService.DeleteAccountPERMANENT(id))
            {
                _logger.LogInformation($"REMOVE Account with ID: {id}");
                return Ok(new SuccessResponse<ExScheduleCreateViewModel>(200, "Xóa vĩn viễn thành công.", null));
            }
            else
            {
                return NoContent();
            }

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
            if (await _accountService.CheckAccountStatus(id) == true) return BadRequest("Tài khoản đã bị xóa rồi");
            if (await _accountService.DeleteAccount(id))
            {
                _logger.LogInformation($"Deleted Account with ID: {id}");
                return Ok(new SuccessResponse<ExScheduleCreateViewModel>(200, "Xóa thành công.", null));
            }
            else
            {
                return NoContent();
            }

        }

        [HttpGet]
        public async Task<IActionResult> CheckPhoneNoExist(string phoneNo)
        {
            if (!PhoneNoExists(phoneNo))
                return BadRequest(new ErrorResponse(400, "STD đang được sử dụng."));
            else return Ok("Có thể dùng SDT này");
        }

        [HttpPost]
        public async Task<ActionResult> UnDeleteAccountById(Guid AccountId)
        {
            if (AccountId == Guid.Empty) return BadRequest();
            if (await _accountService.CheckAccountStatus(AccountId) == false) return BadRequest("Tài khoản đã bỏ xóa rồi!");
            var result = await _accountService.UndeleteAccount(AccountId);
            if (result == true) return Ok();
            else { return Ok(new SuccessResponse<ExScheduleCreateViewModel>(200, "Bỏ xóa thành công.", null)); }
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
