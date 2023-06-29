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
        Task<AccountViewModel> CreateAccount(AccountCreateViewModel model);
        Task<AccountViewModel> DeleteAccount(Guid id);
        Task<AccountViewModel> PutAccount(Guid id, Account account);
        Task<List<AccountViewModel>> SearchAccountByName(string Fullname);
        Task<List<AccountViewModel>> SearchAccountByPhoneNo(string PhoneNo);
        Task<AccountViewModel> GetAccountByID(Guid id);
        Task<List<AccountViewModel>> GetAllGymerAccountsWithIsLock(bool IsLock);
        Task<List<AccountViewModel>> GetAllGymerAccounts();
        Task<List<AccountViewModel>> GetAllNEAccountsWithIsLock(bool IsLock);
        Task<List<AccountViewModel>> GetAllNEAccounts();
        Task<List<AccountViewModel>> GetAllPTAccountsWithIsLock(bool IsLock);
        Task<List<AccountViewModel>> GetAllPTAccounts();
        Task<List<AccountViewModel>> GetAllStaffAccountsWithStatus(bool IsLock);
        Task<List<AccountViewModel>> GetAllStaffAccounts();
        Task<List<AccountViewModel>> GetAllAccountsWithStatus(bool IsLock);
        Task<List<AccountViewModel>> GetAllAccounts();
    }
}
