using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;
        private readonly ClientRepository _clientRepository;
        private readonly IConfiguration _configuration;

        public UserController(UserRepository userRepository, ClientRepository clientRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _clientRepository = clientRepository;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterClient([FromBody] RegisterRequestModel request)
        {
            var user = new UserModel
            {
                Nome = request.Nome,
                Email = request.Email,
                Senha = request.Senha
            };
            var userId = await _userRepository.CreateUserAsync(user);

            var clientId = await _clientRepository.CreateClienteAsync(userId, request.Cpf, request.DataNascimento, request.Telefone);

            var token = GenerateJwtToken(user);
            await _userRepository.SaveSessionTokenAsync(userId, token);

            return Ok(new { UserId = userId, ClienteId = clientId, Token = token });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            var user = await _userRepository.GetUserByEmailAsync(login.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(login.Senha, user.Senha))
            {
                return Unauthorized("Senha ou email inválidos.");
            }

            var token = GenerateJwtToken(user);
            await _userRepository.SaveSessionTokenAsync(user.Id, token);
            return Ok(new { Token = token });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] string token)
        {
            await _userRepository.DeactivateSessionTokenAsync(token);
            return Ok("Logout realizado com sucesso.");
        }

        private string GenerateJwtToken(UserModel user)
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