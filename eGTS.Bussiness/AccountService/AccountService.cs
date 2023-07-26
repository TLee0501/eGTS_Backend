using eGTS_Backend.Data.Models;
using eGTS_Backend.Data.ViewModel;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace eGTS.Bussiness.AccountService
{
    public class AccountService : IAccountService
    {
        private readonly EGtsContext _context;
        private readonly ILogger<IAccountService> _logger;

        public AccountService(EGtsContext context, ILogger<IAccountService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Guid> CreateAccount(AccountCreateViewModel model)
        {
            Guid id = Guid.NewGuid();
            

            Account account = new Account(id, model.PhoneNo, model.Password, null, model.Fullname, model.Gender, model.Role, DateTime.Now, false);
            try
            {
                await _context.Accounts.AddAsync(account);
                await _context.SaveChangesAsync();
                return id;
            }
            catch (Exception ex)
            {
                _logger.LogError("Invalid data.");
                return Guid.Empty;
            }

        }

        public async Task<bool> DeleteAccountPERMANENT(Guid id)
        {
            var account = await _context.Accounts.FindAsync(id);

            if (account != null)
            {
                _context.Accounts.Remove(account);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<AccountViewModel> GetAccountByID(Guid id)
        {

            var account = await _context.Accounts.FindAsync(id);

            if (account == null)
            {
                return null;
            }
            else
            {
                AccountViewModel result = new AccountViewModel();
                result.Id = account.Id;
                result.PhoneNo = account.PhoneNo;
                result.Password = account.Password;
                result.Image = account.Image;
                result.Fullname = account.Fullname;
                result.Gender = account.Gender;
                result.Role = account.Role;
                result.CreateDate = account.CreateDate;
                result.IsDelete = account.IsDelete;
                return result;
            }
        }


        public async Task<List<AccountViewModel>> GetAllAccountsOtionalRoleAndIsLock(string? role, bool? isDelete)
        {
            List<AccountViewModel> resultList = new List<AccountViewModel>();
            if (role != null && isDelete != null)
            {
                var accounts = await _context.Accounts.Where(a => a.IsDelete == isDelete && a.Role.Equals(role)).ToListAsync();
                foreach (Account account in accounts)
                {
                    AccountViewModel result = new AccountViewModel();
                    result.Id = account.Id;
                    result.PhoneNo = account.PhoneNo;
                    result.Password = account.Password;
                    result.Image = account.Image;
                    result.Fullname = account.Fullname;
                    result.Gender = account.Gender;
                    result.Role = account.Role;
                    result.CreateDate = account.CreateDate;
                    result.IsDelete = account.IsDelete;
                    resultList.Add(result);
                }
            }
            else
            if (role != null)
            {
                var accounts = await _context.Accounts.Where(a => a.Role.Equals(role)).ToListAsync();
                foreach (Account account in accounts)
                {
                    AccountViewModel result = new AccountViewModel();
                    result.Id = account.Id;
                    result.PhoneNo = account.PhoneNo;
                    result.Password = account.Password;
                    result.Image = account.Image;
                    result.Fullname = account.Fullname;
                    result.Gender = account.Gender;
                    result.Role = account.Role;
                    result.CreateDate = account.CreateDate;
                    result.IsDelete = account.IsDelete;
                    resultList.Add(result);
                }
            }
            else
            if (isDelete != null)
            {
                var accounts = await _context.Accounts.Where(a => a.IsDelete == isDelete).ToListAsync();
                foreach (Account account in accounts)
                {
                    AccountViewModel result = new AccountViewModel();
                    result.Id = account.Id;
                    result.PhoneNo = account.PhoneNo;
                    result.Password = account.Password;
                    result.Image = account.Image;
                    result.Fullname = account.Fullname;
                    result.Gender = account.Gender;
                    result.Role = account.Role;
                    result.CreateDate = account.CreateDate;
                    result.IsDelete = account.IsDelete;
                    resultList.Add(result);
                }
            }
            else
            {
                var accounts = await _context.Accounts.ToListAsync();
                foreach (Account account in accounts)
                {
                    AccountViewModel result = new AccountViewModel();
                    result.Id = account.Id;
                    result.PhoneNo = account.PhoneNo;
                    result.Password = account.Password;
                    result.Image = account.Image;
                    result.Fullname = account.Fullname;
                    result.Gender = account.Gender;
                    result.Role = account.Role;
                    result.CreateDate = account.CreateDate;
                    result.IsDelete = account.IsDelete;
                    resultList.Add(result);
                }
            }

            if (resultList.Count > 0)
                return resultList;
            return null;
        }

        public async Task<bool> UpdateAccount(Guid id, AccountUpdateViewModel request)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
                return false;

            if (!request.PhoneNo.Equals(""))
                account.PhoneNo = request.PhoneNo;

            if (!request.Password.Equals(""))
                account.Password = request.Password;

            if (!request.Image.Equals(""))
                account.Image = request.Image;

            if (!request.Fullname.Equals("") && !request.Fullname.Equals("string"))
                account.Fullname = request.Fullname;

            if (!request.Gender.Equals(""))
                account.Gender = request.Gender;

            if (!request.Role.Equals("") && !request.Role.Equals("string"))
                account.Role = request.Role;

            account.IsDelete = request.IsDelete;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Unable to save changes");
            }
            return false;
        }

        public async Task<List<AccountViewModel>> SearchAccountByName(string Fullname)
        {
            List<AccountViewModel> resultList = new List<AccountViewModel>();

            var accounts = await _context.Accounts.Where(a => a.Fullname.Contains(Fullname)).ToListAsync();
            foreach (Account account in accounts)
            {
                AccountViewModel result = new AccountViewModel();
                result.Id = account.Id;
                result.PhoneNo = account.PhoneNo;
                result.Password = account.Password;
                result.Image = account.Image;
                result.Fullname = account.Fullname;
                result.Gender = account.Gender;
                result.Role = account.Role;
                result.CreateDate = account.CreateDate;
                result.IsDelete = account.IsDelete;
                resultList.Add(result);
            }
            return resultList;
        }

        public async Task<List<AccountViewModel>> SearchAccountByPhoneNo(string PhoneNo)
        {
            List<AccountViewModel> resultList = new List<AccountViewModel>();

            var accounts = await _context.Accounts.Where(a => a.PhoneNo.Contains(PhoneNo)).ToListAsync();
            foreach (Account account in accounts)
            {
                AccountViewModel result = new AccountViewModel();
                result.Id = account.Id;
                result.PhoneNo = account.PhoneNo;
                result.Password = account.Password;
                result.Image = account.Image;
                result.Fullname = account.Fullname;
                result.Gender = account.Gender;
                result.Role = account.Role;
                result.CreateDate = account.CreateDate;
                result.IsDelete = account.IsDelete;
                resultList.Add(result);
            }
            return resultList;
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

        public async Task<bool> DeleteAccount(Guid id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
                return false;

            account.IsDelete = true;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Unable to Delete");
            }
            return false;
        }

        public async Task<bool> UndeleteAccount(Guid id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
                return false;

            account.IsDelete = false;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Unable to Undelete");
                return false;
            }
        }

        public async Task<bool> CheckAccountStatus(Guid id)
        {
            var account = await _context.Accounts.FindAsync(id);
            return account.IsDelete;
        }
    }
}
