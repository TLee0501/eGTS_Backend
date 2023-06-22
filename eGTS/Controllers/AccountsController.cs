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

namespace eGTS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly EGtsContext _context;

        public AccountsController(EGtsContext context)
        {
            _context = context;
        }

        /// <summary>
        /// This function is use to get all accounts in DB
        /// </summary>
        /// <returns> List<Account> </returns>
        // GET: api/Accounts/GetAllAccounts
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]//NOT FOUND
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
        /// This function is use to get all accounts with isLock Condition appllied in DB
        /// </summary>
        /// <param name="isLock"></param>
        /// <returns> List<Account> </returns>
        // GET: api/Accounts/GetAllAccountsWithStatus
        [HttpGet("{isLock}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]//NOT FOUND
        [ProducesResponseType(StatusCodes.Status200OK)]//OK
        public async Task<ActionResult<IEnumerable<Account>>> GetAllAccountsWithStatus(bool isLock)
        {
            if (_context.Accounts == null)
            {
                return NotFound();
            }

            if (isLock)
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
        [ProducesResponseType(StatusCodes.Status404NotFound)]//NOT FOUND
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
        /// This function is use to get all Staff accounts with isLock Condition appllied in DB
        /// </summary>
        /// <param name="isLock"></param>
        /// <returns> List<Account> </returns>
        // GET: api/Accounts/GetAllStaffAccountsWithStatus
        [HttpGet("{isLock}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]//NOT FOUND
        [ProducesResponseType(StatusCodes.Status200OK)]//OK
        public async Task<ActionResult<IEnumerable<Account>>> GetAllStaffAccountsWithStatus(bool isLock)
        {
            if (_context.Accounts == null)
            {
                return NotFound();
            }

            if (isLock)
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
        [ProducesResponseType(StatusCodes.Status404NotFound)]//NOT FOUND
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
        /// This function is use to get all PT accounts with isLock Condition appllied in DB
        /// </summary>
        /// <param name="isLock"></param>
        /// <returns> List<Account> </returns>
        // GET: api/Accounts/GetAllPTAccountsWithStatus
        [HttpGet("{isLock}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]//NOT FOUND
        [ProducesResponseType(StatusCodes.Status200OK)]//OK
        public async Task<ActionResult<IEnumerable<Account>>> GetAllPTAccountsWithStatus(bool isLock)
        {
            if (_context.Accounts == null)
            {
                return NotFound();
            }

            if (isLock)
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
        [ProducesResponseType(StatusCodes.Status404NotFound)]//NOT FOUND
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
        /// This function is use to get all NE accounts with isLock Condition appllied in DB
        /// </summary>
        /// <param name="isLock"></param>
        /// <returns> List<Account> </returns>
        // GET: api/Accounts/GetAllNEAccountsWithStatus
        [HttpGet("{isLock}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]//NOT FOUND
        [ProducesResponseType(StatusCodes.Status200OK)]//OK
        public async Task<ActionResult<IEnumerable<Account>>> GetAllNEAccountsWithStatus(bool isLock)
        {
            if (_context.Accounts == null)
            {
                return NotFound();
            }

            if (isLock)
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
        [ProducesResponseType(StatusCodes.Status404NotFound)]//NOT FOUND
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
        /// This function is use to get all NE accounts with isLock Condition appllied in DB
        /// </summary>
        /// /// <param name="isLock"></param>
        /// <returns> List<Account> </returns>
        // GET: api/Accounts/GetAllGymerAccountsWithStatus
        [HttpGet("{isLock}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]//NOT FOUND
        [ProducesResponseType(StatusCodes.Status200OK)]//OK
        public async Task<ActionResult<IEnumerable<Account>>> GetAllGymerAccountsWithStatus(bool isLock)
        {
            if (_context.Accounts == null)
            {
                return NotFound();
            }

            if (isLock)
                return await _context.Accounts.Where(a => a.IsLock == true && a.Role.Equals("Gymer")).ToListAsync();
            else
                return await _context.Accounts.Where(a => a.IsLock == false && a.Role.Equals("Gymer")).ToListAsync();

        }

        /// <summary>
        /// This Function is use to get Account by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Account</returns>
        // GET: api/Accounts/GetAccountByID
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]//NOT FOUND
        [ProducesResponseType(StatusCodes.Status200OK)]//OK
        public async Task<ActionResult<Account>> GetAccountByID(Guid id)
        {
            if (_context.Accounts == null)
            {
                return NotFound();
            }
            var result = await _context.Accounts.FindAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return result;
        }

        /// <summary>
        /// This Function is use to search for accounts by Phone Number
        /// </summary>
        /// <param name="PhoneNo"></param>
        /// <returns>List<Account></returns>
        // GET: api/Accounts/SearchAccountByPhoneNo
        [HttpGet("{PhoneNo}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]//NOT FOUND
        [ProducesResponseType(StatusCodes.Status400BadRequest)]//BAD REQUEST
        [ProducesResponseType(StatusCodes.Status200OK)]//OK
        public async Task<ActionResult<IEnumerable<Account>>> SearchAccountByPhoneNo(string PhoneNo)
        {
            if (PhoneNo == null)
                return BadRequest();
            if (PhoneNo.Length > 11)
                return BadRequest();

            var result = await _context.Accounts.Where(a => a.PhoneNo.Contains(PhoneNo)).ToListAsync();

            if (result.Count == 0)
                return NotFound();

            return result;

        }
        /// <summary>
        /// This Function is use to search for accounts by Phone Number
        /// </summary>
        /// <param name="Fullname"></param>
        /// <returns>List<Account></returns>
        // GET: api/Accounts/SearchAccountByName
        [HttpGet("{Fullname}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]//NOT FOUND
        [ProducesResponseType(StatusCodes.Status400BadRequest)]//BAD REQUEST
        [ProducesResponseType(StatusCodes.Status200OK)]//OK
        public async Task<ActionResult<IEnumerable<Account>>> SearchAccountByName(string Fullname)
        {
            if (Fullname == null)
                return BadRequest();
            if (Fullname.Length > 11)
                return BadRequest();

            var result = await _context.Accounts.Where(a => a.FullName.Contains(Fullname)).ToListAsync();

            if (result.Count == 0)
                return NotFound();

            return result;

        }

        /// <summary>
        /// Update Account TBA
        /// </summary>
        /// <param name="id"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        // PUT: api/Accounts/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]//BAD REQUEST
        [ProducesResponseType(StatusCodes.Status400BadRequest)]//NOT FOUND
        public async Task<IActionResult> PutAccount(Guid id, Account account)
        {
            if (id != account.Id)
            {
                return BadRequest();
            }

            _context.Entry(account).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
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
        public async Task<ActionResult<Account>> CreateAccount(AccountCreateViewModel model)
        {
            if (model.PhoneNo.Equals("") || model.Password.Equals(""))
                return BadRequest();
            if (!PhoneNoIsValid(model.PhoneNo))
                return BadRequest();
            if (PhoneNoExists(model.PhoneNo))
                return BadRequest();

            Guid id = Guid.NewGuid();
            Account account = new Account(id, model.PhoneNo, model.Password, model.Role, model.FullName, DateTime.Now, true);

            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();

            return CreatedAtAction("CreateAccount", new { id = account.Id }, account);
        }

        // DELETE: api/Accounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(Guid id)
        {
            if (_context.Accounts == null)
            {
                return NotFound();
            }
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();

            return NoContent();
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
        private bool PhoneNoIsValid(string PhoneNo)
        {
            return Regex.IsMatch(PhoneNo, "/(84|0[3|5|7|8|9])+([0-9]{8})\b/g");
        }
    }
}
