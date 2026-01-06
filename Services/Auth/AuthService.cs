using Microsoft.AspNetCore.Identity.Data;
using TreasuryApi.Provider;

namespace TreasuryApi.Service.Auth
{
    public class AuthService : IAuthService
    {
        private readonly TokenProvider _tokenProvider;

        public AuthService(TokenProvider tokenProvider)
        {
            _tokenProvider = tokenProvider;
        }

        public async Task<string> LoginAuth(LoginRequest user)
        {
            string token = await _tokenProvider.Create(user);
            return token;
        }
    }
}