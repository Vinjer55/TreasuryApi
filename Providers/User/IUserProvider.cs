using Models.DBModel;
using Models.Request;

namespace Providers.User
{
    public interface IUserProvider
    {
        Task<int> CreateUser(RegisterRequest user);
        Task<AppUser> GetUserByEmail(string email);
        Task<AppUser> GetUserById(int id);
        Task<AppUser> GetUserByPhone(string phone);
        Task UpdateCorp(string corporationId);
        Task UpdateRole(string userId, string role);
        Task UpdateUser(AppUser user);
    }
}
