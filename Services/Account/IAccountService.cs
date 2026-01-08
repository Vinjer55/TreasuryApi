using Models.DBModel;
using Models.Request;

namespace Services.Account
{
    public interface IAccountService
    {
        Task<int> CreateAccount(string userId, CreateAccountRequest request);
        Task DeleteAccount(int accountId);
        Task<AccountModel> GetAccountById(int id);
        Task<IEnumerable<AccountModel>> GetAccounts(string userId);
        Task UpdateAccount(string userId, int id, UpdateAccount request);
    }
}
