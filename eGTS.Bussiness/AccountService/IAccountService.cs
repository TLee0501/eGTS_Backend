using eGTS_Backend.Data.Models;
using eGTS_Backend.Data.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace eGTS.Bussiness.AccountService
{
    public interface IAccountService
    {
        Task<ActionResult<Account>> CreateAccount(AccountCreateViewModel model);
        Task<IActionResult> DeleteAccount(Guid id);
        Task<IActionResult> PutAccount(Guid id, Account account);
        Task<ActionResult<IEnumerable<Account>>> SearchAccountByName(string Fullname);
        Task<ActionResult<IEnumerable<Account>>> SearchAccountByPhoneNo(string PhoneNo);
        Task<ActionResult<Account>> GetAccountByID(Guid id);
        Task<ActionResult<IEnumerable<Account>>> GetAllGymerAccountsWithIsLock(bool IsLock);
        Task<ActionResult<IEnumerable<Account>>> GetAllGymerAccounts();
        Task<ActionResult<IEnumerable<Account>>> GetAllNEAccountsWithIsLock(bool IsLock);
        Task<ActionResult<IEnumerable<Account>>> GetAllNEAccounts();
        Task<ActionResult<IEnumerable<Account>>> GetAllPTAccountsWithIsLock(bool IsLock);
        Task<ActionResult<IEnumerable<Account>>> GetAllPTAccounts();
        Task<ActionResult<IEnumerable<Account>>> GetAllStaffAccountsWithStatus(bool IsLock);
        Task<ActionResult<IEnumerable<Account>>> GetAllStaffAccounts();
        Task<ActionResult<IEnumerable<Account>>> GetAllAccountsWithStatus(bool IsLock);
        Task<ActionResult<IEnumerable<Account>>> GetAllAccounts();
    }
}
