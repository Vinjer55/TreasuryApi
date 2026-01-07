using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Models.DBModel;
using System.Security.Claims;
using System.Text;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace TreasuryApi.Provider
{
    public class TokenProvider(IConfiguration configuration)
    {
        public async Task<string> Create(AppUser User)
        {
            string secretKey = configuration["Jwt:SecretKey"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, User.Email ?? string.Empty),
                new Claim("UserId", User.Id.ToString()),
                new Claim("Verified", User.Verified.ToString()),
                new Claim("IsActive", User.IsActive.ToString()),
            };

            if (!string.IsNullOrEmpty(User.Phone))
                claims.Add(new Claim("Phone", User.Phone));

            if (!string.IsNullOrEmpty(User.Name))
                claims.Add(new Claim("Name", User.Name));

            if (User.CorporationId.HasValue)
                claims.Add(new Claim("CorpId", User.CorporationId.Value.ToString()));

            if (User.AccountId.HasValue)
                claims.Add(new Claim("AccountId", User.AccountId.Value.ToString()));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = credentials,
                Issuer = configuration["Jwt:Issuer"],
                Audience = configuration["Jwt:Audience"]
            };

            var handler = new JsonWebTokenHandler();
            return handler.CreateToken(tokenDescriptor);
        }
    }
}