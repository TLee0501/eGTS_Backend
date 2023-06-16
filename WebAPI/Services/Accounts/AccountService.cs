using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using WebAPI.Models;

namespace WebAPI.Services.Accounts
{
    public class AccountService : IAccountService
    {
        private readonly DataContext _context;
        public AccountService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Account>> GetAccounts()
        {
            var result = await _context.Accounts.ToListAsync();
            return result;
        }
    }
}
