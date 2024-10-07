using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using backend.Models;
using Microsoft.IdentityModel.Tokens;

namespace backend.Services
{
    /// <summary>
    /// Serviço de token.
    /// </summary>
    public class TokenService
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Construtor do serviço de token.
        /// </summary>
        /// <param name="configuration"></param>
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Gera um token JWT.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public string GenerateJwtToken(UserModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secret = _configuration["Jwt:Secret"];
            if (string.IsNullOrEmpty(secret))
            {
                throw new InvalidOperationException("JWT Secret is not configured.");
            }
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Email)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}