using eGTS_Backend.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGTS_Backend.Data.Repositories
{
    public interface IAccountRepository
    {
        public Account Login(string PhoneNo, string Password);
    }
}
