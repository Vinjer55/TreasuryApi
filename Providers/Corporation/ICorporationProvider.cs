using Models.DBModel;
using Models.Request;
using Models.Response;

namespace Providers.Corporation
{
    public interface ICorporationProvider
    {
        Task AddUserToCorp(string userId, int corpId, string role);
        Task<int> CreateCorp(string userId, CreateCorpRequest request);
        Task DeleteCorpById(string corporationId);
        Task<IEnumerable<CorpUsers>> GetAllCorpUsers(string corporationId);
        Task<CorporationModel> GetCorpByName(string name);
    }
}
