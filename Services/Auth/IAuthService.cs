using Microsoft.AspNetCore.Identity.Data;

namespace TreasuryApi.Service.Auth
{
    public interface IAuthService
    {
        Task<string> LoginAuth(LoginRequest user);
    }
}
