using Models.DBModel;
using Models.Request;

namespace Services.User
{
    public interface IUserService
    {
        Task<int> CreateUser(RegisterRequest user);
        Task<AppUser> GetUserById(int Id);
        Task UpdateUser(int id, UpdateUser request);
    }
}
