using Models.Request;
using Models.Response;
using Providers.Corporation;
using Providers.User;

namespace Services.Corporation
{
    public class CorporationService : ICorporationService
    {
        private readonly ICorporationProvider _corporationProvider;
        private readonly IUserProvider _userProvider;
        public CorporationService(ICorporationProvider corporationProvider, IUserProvider userProvider) 
        {
            _corporationProvider = corporationProvider;
            _userProvider = userProvider;
        }
        public async Task<int> CreateCorp(string userId, CreateCorpRequest request)
        {
            var checkCorpName = await _corporationProvider.GetCorpByName(request.Name);
            if (checkCorpName != null)
                throw new ArgumentException("Corporation name already exists");

            var corpId = await _corporationProvider.CreateCorp(userId, request);
            await _corporationProvider.AddUserToCorp(userId, corpId, "Admin");
            return corpId;
        }

        public async Task<IEnumerable<CorpUsers>> GetCorpUsers(string corporationId)
        {
            var users = await _corporationProvider.GetAllCorpUsers(corporationId);
            return users;
        }

        public async Task DeleteCorp(string userId, string corporationId)
        {
            await _userProvider.UpdateCorp(corporationId);
            await _corporationProvider.DeleteCorpById(corporationId);
            await _userProvider.UpdateRole(userId, "User");
        }
    }
}
