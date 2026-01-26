using Dapper;
using Microsoft.Identity.Client;
using Microsoft.VisualBasic;
using Models.DBModel;
using Models.Request;
using System.Data;

namespace Providers.Account
{
    public class AccountProvider(SqlConnectionFactory _sqlContext) : IAccountProvider
    {
        public async Task<int> CreateAccount(string userId, CreateAccountRequest request)
        {
            using (var conn = _sqlContext.CreateConnection())
            {
                var parameters = new
                {
                    AppUserId = userId,
                    AccountKind = request.AccountKind,
                    AssetType = request.AssetType,
                    CurrencyCode = request.CurrencyCode,
                    Balance = request.Balance,
                    Provider = request.Provider
                };

                var newAccountId = await conn.QuerySingleAsync<int>(
                    "Create_Account",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return newAccountId;
            }
        }
        public async Task<AccountModel> CheckAccountExist(string userId, CreateAccountRequest request)
        {
            using (var conn = _sqlContext.CreateConnection())
            {
                var account = await conn.QueryFirstOrDefaultAsync<AccountModel>(
                    "Check_AccountExist",
                    new
                    {
                        AppUserId = userId,
                        AccountKind = request.AccountKind,
                        AssetType = request.AssetType,
                        CurrencyCode = request.CurrencyCode,
                        Provider = request.Provider
                    },
                    commandType: CommandType.StoredProcedure
                );
                return account;
            }
        }

        public async Task<IEnumerable<AccountModel>> GetAccountsByUserId(string userId)
        {
            using (var conn = _sqlContext.CreateConnection())
            {
                var accounts = await conn.QueryAsync<AccountModel>(
                    "Get_AccountByUserId",
                     new
                     {
                        AppUserId = userId
                     },
                     commandType: CommandType.StoredProcedure
                );
                return accounts ?? new List<AccountModel>();
            }
        }

        public async Task<AccountModel> GetAccountById(int id)
        {
            using (var conn = _sqlContext.CreateConnection())
            {
                var account = await conn.QuerySingleOrDefaultAsync<AccountModel>(
                    "Get_AccountById",
                    new
                    {
                        Id = id,
                    },
                    commandType: CommandType.StoredProcedure
                );
                return account;
            }
        }

        public async Task UpdateAccount(AccountModel account)
        {
            using (var conn = _sqlContext.CreateConnection())
            {
                var parameters = new
                {
                    Id = account.Id,
                    AccountKind = account.AccountKind,
                    AssetType = account.AssetType,
                    CurrencyCode = account.CurrencyCode,
                    Balance = account.Balance,
                    Provider = account.Provider
                };

                await conn.ExecuteAsync(
                    "Update_Account",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
            }
        }

        public async Task DeleteAccount(int accountId)
        {
            using (var conn = _sqlContext.CreateConnection())
            {
                await conn.ExecuteAsync(
                "Delete_Account",
                new { Id = accountId },
                commandType: CommandType.StoredProcedure
                );
            }
        }

        public async Task<AccountModel> GetBankAndAccount(string userId, int bankId, string account)
        {
            using (var conn = _sqlContext.CreateConnection())
            {
                var userAccount = await conn.QuerySingleOrDefaultAsync<AccountModel>(
                    "Get_BankAndAccount",
                    new
                    {
                        Id = bankId,
                        AppUserId = userId,
                        CurrencyCode = account
                    },
                    commandType: CommandType.StoredProcedure
                );
                return userAccount;
            }
        }

        public async Task UpdateAmount(string userId, int accountId, decimal amount)
        {
            using (var conn = _sqlContext.CreateConnection())
            {
                var parameters = new
                {
                    Id = accountId,
                    AppUserId = userId,
                    Balance = amount
                };

                await conn.ExecuteAsync(
                    "Update_Amount",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
            }
        }

    }
}
