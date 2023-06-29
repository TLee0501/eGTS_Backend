using eGTS_Backend.Data.Models;
using eGTS_Backend.Data.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace eGTS.Bussiness.AccountService
{
    public class AccountService : IAccountService
    {
        private readonly EGtsContext _context;
        public AccountService(EGtsContext context)
        {
            _context = context;
        }

        public Task<AccountViewModel> CreateAccount(AccountCreateViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task<AccountViewModel> DeleteAccount(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<AccountViewModel> GetAccountByID(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<AccountViewModel>> GetAllAccounts()
        {
            throw new NotImplementedException();
        }

        public Task<List<AccountViewModel>> GetAllAccountsWithStatus(bool IsLock)
        {
            throw new NotImplementedException();
        }

        public Task<List<AccountViewModel>> GetAllGymerAccounts()
        {
            throw new NotImplementedException();
        }

        public Task<List<AccountViewModel>> GetAllGymerAccountsWithIsLock(bool IsLock)
        {
            throw new NotImplementedException();
        }

        public Task<List<AccountViewModel>> GetAllNEAccounts()
        {
            throw new NotImplementedException();
        }

        public Task<List<AccountViewModel>> GetAllNEAccountsWithIsLock(bool IsLock)
        {
            throw new NotImplementedException();
        }

        public Task<List<AccountViewModel>> GetAllPTAccounts()
        {
            throw new NotImplementedException();
        }

        public Task<List<AccountViewModel>> GetAllPTAccountsWithIsLock(bool IsLock)
        {
            throw new NotImplementedException();
        }

        public Task<List<AccountViewModel>> GetAllStaffAccounts()
        {
            throw new NotImplementedException();
        }

        public Task<List<AccountViewModel>> GetAllStaffAccountsWithStatus(bool IsLock)
        {
            throw new NotImplementedException();
        }

        public Task<AccountViewModel> PutAccount(Guid id, Account account)
        {
            throw new NotImplementedException();
        }

        public Task<List<AccountViewModel>> SearchAccountByName(string Fullname)
        {
            throw new NotImplementedException();
        }

        public Task<List<AccountViewModel>> SearchAccountByPhoneNo(string PhoneNo)
        {
            throw new NotImplementedException();
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
