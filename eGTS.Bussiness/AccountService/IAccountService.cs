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
        Task<bool> CreateAccount(AccountCreateViewModel model);
        Task<bool> DeleteAccount(Guid id);
        Task<bool> UpdateAccount(Guid id, AccountUpdateViewModel request);
        Task<List<AccountViewModel>> SearchAccountByName(string Fullname);
        Task<List<AccountViewModel>> SearchAccountByPhoneNo(string PhoneNo);
        Task<AccountViewModel> GetAccountByID(Guid id);
        Task<List<AccountViewModel>> GetAllAccountsOtionalRoleAndIsLock(string? role, bool? isLock);
    }
}
