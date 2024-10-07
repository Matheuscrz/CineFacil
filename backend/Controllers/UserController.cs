using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using backend.Data;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace backend.Controllers
{
    /// <summary>
    /// Controller para operações relacionadas a usuários.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;
        private readonly TokenService _tokenService;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="UserController"/>.
        /// </summary>
        /// <param name="userRepository">O repositório de usuários.</param>
        /// <param name="tokenService">O serviço de tokens.</param>
        public UserController(UserRepository userRepository, TokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Realiza o login de um usuário.
        /// </summary>
        /// <param name="login">Os dados de login do usuário.</param>
        /// <returns>Um objeto <see cref="IActionResult"/> contendo o resultado da operação.</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            var user = await _userRepository.GetUserByEmailAsync(login.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(login.Senha, user.Senha))
            {
                return Unauthorized("Senha ou email inválidos.");
            }

            var token = _tokenService.GenerateJwtToken(user);
            await _userRepository.SaveSessionTokenAsync(user.Id, token);
            return Ok(new { Token = token });
        }

        /// <summary>
        /// Realiza o logout de um usuário.
        /// </summary>
        /// <param name="token">O token JWT do usuário.</param>
        /// <returns>Um objeto <see cref="IActionResult"/> contendo o resultado da operação.</returns>
        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] string token)
        {
            await _userRepository.DeactivateSessionTokenAsync(token);
            return Ok("Logout realizado com sucesso.");
        }
    }
}