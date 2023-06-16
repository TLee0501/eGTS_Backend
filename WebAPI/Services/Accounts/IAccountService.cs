using WebAPI.Models;

namespace WebAPI.Services.Accounts
{
    public interface IAccountService
    {
        Task<List<Account>> GetAccounts();
    }
}
