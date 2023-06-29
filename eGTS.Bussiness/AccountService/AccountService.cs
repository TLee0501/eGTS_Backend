using eGTS_Backend.Data.Models;
using eGTS_Backend.Data.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGTS.Bussiness.AccountService
{
    public class AccountService : IAccountService
    {
        public Task<ActionResult<Account>> CreateAccount(AccountCreateViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> DeleteAccount(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<Account>> GetAccountByID(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<IEnumerable<Account>>> GetAllAccounts()
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<IEnumerable<Account>>> GetAllAccountsWithStatus(bool IsLock)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<IEnumerable<Account>>> GetAllGymerAccounts()
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<IEnumerable<Account>>> GetAllGymerAccountsWithIsLock(bool IsLock)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<IEnumerable<Account>>> GetAllNEAccounts()
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<IEnumerable<Account>>> GetAllNEAccountsWithIsLock(bool IsLock)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<IEnumerable<Account>>> GetAllPTAccounts()
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<IEnumerable<Account>>> GetAllPTAccountsWithIsLock(bool IsLock)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<IEnumerable<Account>>> GetAllStaffAccounts()
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<IEnumerable<Account>>> GetAllStaffAccountsWithStatus(bool IsLock)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> PutAccount(Guid id, Account account)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<IEnumerable<Account>>> SearchAccountByName(string Fullname)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<IEnumerable<Account>>> SearchAccountByPhoneNo(string PhoneNo)
        {
            throw new NotImplementedException();
        }
    }
}
