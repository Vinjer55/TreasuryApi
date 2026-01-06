using Models.Request;

namespace TreasuryApi.Service.Auth
{
    public interface IAuthService
    {
        Task<string> LoginAuth(LoginRequest user);
    }
}
