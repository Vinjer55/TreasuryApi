using Models.Request;
using Models.Response;

namespace Services.Corporation
{
    public interface ICorporationService
    {
        Task<int> CreateCorp(string userId, CreateCorpRequest request);
        Task DeleteCorp(string userId, string corporationId);
        Task<IEnumerable<CorpUsers>> GetCorpUsers(string corporationId);
    }
}
