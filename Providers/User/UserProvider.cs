using Dapper;
using Models.Request;
using System.Data;

namespace Providers.User
{
    public class UserProvider(SqlConnectionFactory _sqlContext) : IUserProvider
    {
        public async Task<int> CreateUser(RegisterRequest user)
        {
            using (var conn = _sqlContext.CreateConnection())
            {
                var parameters = new
                {
                    user.Name,
                    user.Email,
                    user.Phone,
                    user.Password,
                    Verified = 0, // Verified default 0
                };

                var newId = await conn.ExecuteScalarAsync<int>(
                    "Create_AppUser",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return newId;
            }
        }
    }
}
