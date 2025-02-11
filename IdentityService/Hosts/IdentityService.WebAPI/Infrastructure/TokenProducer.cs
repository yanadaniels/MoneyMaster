using Microsoft.IdentityModel.Tokens;
using MoneyMaster.Common.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IdentityService.WebAPI.Infrastructure
{
    public static class TokenProducer
    {
        public static string GetJWTToken(IEnumerable<Claim> inedtityClaims, AuthOptions authOptions)
        {
            var now = DateTime.UtcNow;

            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: authOptions.Issuer,
                    audience: authOptions.Audience,
                    notBefore: now,
                    claims: inedtityClaims,
                    expires: now.Add(TimeSpan.FromMinutes(authOptions.LifeTime)),
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authOptions.Key)), SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }
    }
}
