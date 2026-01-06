using Models.Request;

namespace Services.User
{
    public interface IUserService
    {
        Task<int> CreateUser(RegisterRequest user);
    }
}
