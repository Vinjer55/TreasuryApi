using Models.Request;
using Providers.Account;

namespace Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly IAccountProvider _accountProvider;
        public AccountService(IAccountProvider accountProvider) 
        {
            _accountProvider = accountProvider;
        }
        public async Task<int> CreateAccount(string userId, CreateAccountRequest request)
        {
            var checkExist = await _accountProvider.CheckAccountExist(userId, request);
            if (checkExist != null)
                throw new ArgumentException("Account details already exists for this user.");

            var accountId = await _accountProvider.CreateAccount(userId, request);
            return accountId;
        }
    }
}
