using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AmazingFileVersionControl.Core.Infrastructure
{
    public class JwtService : IJwtService
    {
        private readonly string _secret;
        private readonly string _expDate;
        private readonly string _issuer;
        private readonly string _audience;

        public JwtService(IConfiguration config)
        {
            _secret = config.GetSection("JwtConfig:Secret").Value;
            _expDate = config.GetSection("JwtConfig:ExpirationInMinutes").Value;
            _issuer = config.GetSection("JwtConfig:Issuer").Value;
            _audience = config.GetSection("JwtConfig:Audience").Value;
        }

        public string GenerateToken(Guid userId, string role, string login)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                    new Claim(ClaimTypes.Role, role),
                    new Claim(ClaimTypes.Name, login)
                }),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_expDate)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _issuer,
                Audience = _audience
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
