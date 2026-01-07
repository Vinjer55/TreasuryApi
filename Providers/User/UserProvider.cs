using Dapper;
using Models.DBModel;
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
        public async Task<AppUser> GetUserByPhone(string phone)
        {
            using (var conn = _sqlContext.CreateConnection())
            {
                var User = await conn.QuerySingleOrDefaultAsync<AppUser>(
                    "Get_UserByPhone",
                    new
                    {
                        phone
                    },
                    commandType: CommandType.StoredProcedure
                );
                return User;
            }
        }
        public async Task<AppUser> GetUserByEmail(string email)
        {
            using (var conn = _sqlContext.CreateConnection())
            {
                var User = await conn.QuerySingleOrDefaultAsync<AppUser>(
                    "Get_UserByEmail",
                    new
                    {
                        email
                    },
                    commandType: CommandType.StoredProcedure
                );
                return User;
            }
        }

        public async Task<AppUser> GetUserById(int id)
        {
            using (var conn = _sqlContext.CreateConnection())
            {
                var User = await conn.QueryFirstOrDefaultAsync<AppUser>(
                    "Get_UserById",
                    new
                    {
                        id
                    },
                    commandType: CommandType.StoredProcedure
                );
                return User;
            }
        }

        public async Task UpdateUser(AppUser user)
        {
            using (var conn = _sqlContext.CreateConnection())
            {
                var parameters = new
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Phone = user.Phone
                };

                await conn.ExecuteAsync(
                    "Update_AppUser",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
            }
        }
    }
}
