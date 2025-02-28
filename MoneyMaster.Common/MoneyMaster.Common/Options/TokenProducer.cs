using Microsoft.IdentityModel.Tokens;
using MoneyMaster.Common.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MoneyMaster.Common.Options
{
    public static class TokenProducer
    {
        /// <summary>
        /// Метод для генерации AccessToke
        /// </summary>
        /// <param name="inedtityClaims"></param>
        /// <param name="authOptions"></param>
        /// <returns>новый accessToken</returns>
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

        /// <summary>
        /// Метод для генерации RefreshToken
        /// </summary>
        /// <returns>новый RefreshToken</returns>
        public static string GenerateRefreshToken()
        {
            var randomBytes = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }
            return Convert.ToBase64String(randomBytes);
        }
    }
}
