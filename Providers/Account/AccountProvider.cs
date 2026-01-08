using Dapper;
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
                    AccountType = request.AccountType,
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
                        AccountType = request.AccountType,
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
                    AccountType = account.AccountType,
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
    }
}
