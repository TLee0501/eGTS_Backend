using eGTS_Backend.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGTS_Backend.Data.DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance;
        private static readonly object instanceLock = new object();
        public EGtsContext Context;
        public AccountDAO() { }
        public static AccountDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                        instance = new AccountDAO();
                    return instance;
                }
            }
        }
        public Account Login(string PhoneNo, string Password)
        {
            Context = new EGtsContext();
            Account result = null;
            try
            {
                result = Context.Accounts.SingleOrDefault(a => a.PhoneNo == PhoneNo);

                if (result == null)
                    return null;
                else if (!result.Password.Equals(Password))
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

    }
}
