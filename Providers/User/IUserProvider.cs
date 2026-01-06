using Models.Request;

namespace Providers.User
{
    public interface IUserProvider
    {
        Task<int> CreateUser(RegisterRequest user);
    }
}
