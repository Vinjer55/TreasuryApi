using Models.DBModel;
using Models.Request;

namespace Providers.Account
{
    public interface IAccountProvider
    {
        Task<AccountModel> CheckAccountExist(string userId, CreateAccountRequest request);
        Task<int> CreateAccount(string userId, CreateAccountRequest request);
    }
}
