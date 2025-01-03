using IMDBClone.Models;
using IMDBClone.Types;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IMDBClone.Helpers
{
    public class JWTHelper
    {
        private JwtOptions _options { get; }

        public JWTHelper(JwtOptions options)
        {
            _options = options;
        }

        public async Task<string> GenerateAcessToken(IEnumerable<Claim> claims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _options.Issuer,
                Audience = _options.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SigningKey)),
                SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(claims)
            };
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(securityToken);
            return accessToken;
        }

        public async Task<IEnumerable<Claim>> GenerateUserClaims(User user, IEnumerable<Role> roles)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Name, user.Name)
            };
            foreach (var role in roles)
            {
                claims.Add(new(ClaimTypes.Role, role.Name));
            }

            return claims;
        }
    }
}
