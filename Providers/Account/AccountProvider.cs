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
    }
}
