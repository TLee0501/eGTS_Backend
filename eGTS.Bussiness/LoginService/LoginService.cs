using eGTS_Backend.Data.Models;
using eGTS_Backend.Data.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGTS.Bussiness.LoginService
{
    public class LoginService : ILoginService
    {
        private readonly EGtsContext _context;
        public LoginService(EGtsContext context)
        {
            _context = context;
        }
        public async Task<AccountViewModel> Login(LoginViewModel model)
        {
            var account = await _context.Accounts.SingleOrDefaultAsync(a => a.PhoneNo == model.PhoneNo && a.Password == model.Password);

            if (account == default)
                return null;

            AccountViewModel result = new AccountViewModel
            {
                Id = account.Id,
                PhoneNo = account.PhoneNo,
                Password = account.Password,
                Fullname = account.Fullname,
                Gender = account.Gender,
                Role = account.Role,
                IsLock = account.IsLock,
                CreateDate = account.CreateDate,

            };

            return result;
        }
    }
}
