using eGTS_Backend.Data.Models;
using eGTS_Backend.Data.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGTS.Bussiness.LoginService
{
    public interface ILoginService
    {
        Task<AccountViewModel> Login(LoginViewModel model);
    }
}
