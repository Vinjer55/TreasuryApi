using Microsoft.Extensions.Caching.Distributed;
using Models.DBModel;
using Models.Request;
using Providers.User;
using System.IdentityModel.Tokens.Jwt;
using TreasuryApi.Provider;

namespace TreasuryApi.Service.Auth
{
    public class AuthService : IAuthService
    {
        private readonly TokenProvider _tokenProvider;
        private readonly IDistributedCache _distributedCache;
        private readonly IUserProvider _userProvider;

        public AuthService(TokenProvider tokenProvider, IDistributedCache distributedCache, IUserProvider userProvider)
        {
            _tokenProvider = tokenProvider;
            _distributedCache = distributedCache;
            _userProvider = userProvider;
        }

        public async Task<string> LoginAuth(LoginRequest user)
        {
            //check if email is not empty then we do by email else by phone
            AppUser AppUserquery = new AppUser();
            if (string.IsNullOrEmpty(user.Email))
            {
                AppUserquery = await _userProvider.GetUserByPhone(user.Phone);
            }
            else
            {
                AppUserquery = await _userProvider.GetUserByEmail(user.Email);
            }
            if (AppUserquery == null)
            {
                throw new UnauthorizedAccessException("User not found");
            }

            //verify password if matched
            bool isValidPassword = BC.EnhancedVerify(user.Password, AppUserquery.Password);
            if (!isValidPassword)
            {
                throw new UnauthorizedAccessException("Invalid password");
            }

            string cacheKey = $"Treasury:AUTH:{AppUserquery.Id}_token";

            // Check if a token already exists in the cache
            var existingToken = await _distributedCache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(existingToken))
            {
                var handler = new JwtSecurityTokenHandler();

                if (handler.CanReadToken(existingToken))
                {
                    var jwt = handler.ReadJwtToken(existingToken);

                    // Look for exp claim
                    var expClaim = jwt.Claims.FirstOrDefault(c => c.Type == "exp")?.Value;
                    if (expClaim != null && long.TryParse(expClaim, out var expSeconds))
                    {
                        // Convert exp (which is in seconds since Unix epoch) to DateTime
                        var expDate = DateTimeOffset.FromUnixTimeSeconds(expSeconds).UtcDateTime;

                        if (expDate > DateTime.UtcNow)
                        {
                            // Token is still valid
                            return existingToken;
                        }
                    }
                }
            }
            //generate token
            string token = await _tokenProvider.Create(AppUserquery);
            await _distributedCache.SetStringAsync(cacheKey, token, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1440) // Set expiration time as needed
            });
            return token;
        }
    }
}