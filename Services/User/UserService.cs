using Models.Request;
using Providers.User;

namespace Services.User
{
    public class UserService : IUserService
    {
        private readonly IUserProvider _userProvider;
        public UserService(IUserProvider userProvider) 
        {
            _userProvider = userProvider;
        }

        public async Task<int> CreateUser(RegisterRequest user)
        {
            int userId = await _userProvider.CreateUser(user);
            return userId;
        }
    }
}
