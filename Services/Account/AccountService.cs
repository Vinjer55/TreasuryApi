using Azure.Core;
using Models.DBModel;
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

        public async Task<IEnumerable<AccountModel>> GetAccounts(string userId)
        {
            try
            {
                var accounts = await _accountProvider.GetAccountsByUserId(userId);
                return accounts ?? new List<AccountModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching accounts for user {userId}: {ex.Message}");
                throw;
            }
        }

        public async Task UpdateAccount(string userId, int id, UpdateAccount request)
        {
            // Fetch existing account
            var existingAccount = await _accountProvider.GetAccountById(id);
            if (existingAccount == null)
                throw new ArgumentException("Account not found.");

            // Check duplicate ONLY if relevant fields are changing
            if (

                request.AccountKind != null ||
                request.AssetType != null ||
                request.CurrencyCode != null ||
                request.Provider != null
            )
            {
                var duplicate = await _accountProvider.CheckAccountExist(
                    userId,
                    new CreateAccountRequest
                    {
                        AccountKind = request.AccountKind ?? existingAccount.AccountKind,
                        AssetType = request.AssetType ?? existingAccount.AssetType,
                        CurrencyCode = request.CurrencyCode ?? existingAccount.CurrencyCode,
                        Provider = request.Provider ?? existingAccount.Provider
                    });

                if (duplicate != null)
                    throw new ArgumentException(
                        "Account with the same Account Type, Currency, and Provider already exists."
                    );
            }

            // Merge values
            var accountToUpdate = new AccountModel
            {
                Id = existingAccount.Id,
                AccountKind = request.AccountKind ?? existingAccount.AccountKind,
                AssetType = request.AssetType ?? existingAccount.AssetType,
                CurrencyCode = request.CurrencyCode ?? existingAccount.CurrencyCode,
                Provider = request.Provider ?? existingAccount.Provider,
                Balance = request.Balance ?? existingAccount.Balance
            };

            await _accountProvider.UpdateAccount(accountToUpdate);
        }

        public async Task DeleteAccount(int accountId)
        {
            var checkAccount = await _accountProvider.GetAccountById(accountId);
            if (checkAccount == null)
                throw new ArgumentException("Account ID mismatch.");

            await _accountProvider.DeleteAccount(accountId);
        }

        public async Task<AccountModel> GetAccountById(int id)
        {
            return await _accountProvider.GetAccountById(id);
        }
    }
}
