using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using ufollow.Domain.Entities;

namespace ufollow.API.Authorization
{
    public sealed class ApiToken
    {
        private readonly ApiTokenOptions _options;

        public ApiToken(ApiTokenOptions options)
        {
            _options = options;
        }

        private byte[] EncodedKey => Encoding.ASCII.GetBytes(_options.Key);

        public SymmetricSecurityKey SecurityKey => new SymmetricSecurityKey(EncodedKey);

        public SigningCredentials SigningCredentials =>
            new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);

        public string Generate(User user)
        {
            return new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken
            (
                issuer: _options.Issuer,
                audience: _options.Audience,
                notBefore: DateTime.UtcNow,
                signingCredentials: SigningCredentials,
                claims: new[] {
                    new Claim(ClaimTypes.Actor, nameof(User)),
                    new Claim(ApiClaimTypes.AccountId, user.AccountId.ToString()),
                    new Claim(ApiClaimTypes.UserId, user.Id.ToString()),
                    new Claim(ApiClaimTypes.Salt, user.Credentials.Password.Salt)
                }
            ));
        }
    }
}
