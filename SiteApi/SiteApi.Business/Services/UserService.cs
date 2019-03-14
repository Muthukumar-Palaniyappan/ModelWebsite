using Microsoft.IdentityModel.Tokens;
using SiteApi.Business.Interfaces;
using SiteApi.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SiteApi.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentException($"Parm is invalid {userRepository}");
        }
        public bool Authenticate(string userName, string password, string secretKey, out string jwtToken)
        {
            jwtToken = string.Empty;
            if (!_userRepository.ValidateCredentials(userName, password))
                return false;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userName)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            jwtToken = tokenHandler.WriteToken(token);
            return true;
        }

        public bool Validate(string jwtToken,string secretKey)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token;
            var principal = tokenHandler.ValidateToken(jwtToken,
                GetValidationParameters(Encoding.ASCII.GetBytes(secretKey)), out token);
            return principal.Claims.Any();
        }

        private TokenValidationParameters GetValidationParameters(byte[] key)
        {
            return new TokenValidationParameters()
            {
                ValidateLifetime = true,
                ValidateAudience = false,
                ValidateIssuer = false,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };
        }
    }
}
