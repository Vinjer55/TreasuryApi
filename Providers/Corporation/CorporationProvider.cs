using Dapper;
using Models.DBModel;
using Models.Request;
using Models.Response;
using System.Data;

namespace Providers.Corporation
{
    public class CorporationProvider(SqlConnectionFactory _sqlContext) : ICorporationProvider
    {
        public async Task<CorporationModel> GetCorpByName(string name)
        {
            using (var conn = _sqlContext.CreateConnection())
            {
                var corp = await conn.QueryFirstOrDefaultAsync<CorporationModel>(
                    "Get_CorpByName",
                    new
                    {
                        Name = name
                    },
                    commandType: CommandType.StoredProcedure
                );
                return corp;
            }
        }

        public async Task<int> CreateCorp(string userId, CreateCorpRequest request)
        {
            using (var conn = _sqlContext.CreateConnection())
            {
                var parameters = new
                {
                    Name = request.Name,
                };

                var newCorpId = await conn.QuerySingleAsync<int>(
                    "Create_Corporation",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return newCorpId;
            }
        }

        public async Task AddUserToCorp(string userId, int corpId, string role)
        {
            using (var conn = _sqlContext.CreateConnection())
            {
                var parameters = new
                {
                    AppUserId = userId,
                    CorporationId = corpId,
                    Role = role
                };
                await conn.ExecuteAsync(
                    "Add_UserToCorp",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
            }
        }

        public async Task<IEnumerable<CorpUsers>> GetAllCorpUsers(string corporationId)
        {
            using (var conn = _sqlContext.CreateConnection())
            {
                var corpUsers = await conn.QueryAsync<CorpUsers>(
                    "Get_AllCorpUsers",
                    new
                    {
                        CorporationId = corporationId
                    },
                    commandType: CommandType.StoredProcedure
                );
                return corpUsers;
            }
        }

        public async Task DeleteCorpById(string corporationId)
        {
            using (var conn = _sqlContext.CreateConnection())
            {
                await conn.ExecuteAsync(
                "Delete_CorpById",
                new
                {
                    Id = corporationId
                },
                commandType: CommandType.StoredProcedure
                );
            }
        }
    }
}
