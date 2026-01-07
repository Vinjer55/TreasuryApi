using Models.DBModel;
using Models.Request;

namespace Providers.User
{
    public interface IUserProvider
    {
        Task<int> CreateUser(RegisterRequest user);
        Task<AppUser> GetUserByEmail(string email);
        Task<AppUser> GetUserByPhone(string phone);
    }
}
