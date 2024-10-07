using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    /// <summary>
    /// Controller para operações relacionadas a clientes.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly ClientRepository _clientRepository;
        private readonly UserRepository _userRepository;
        private readonly TokenService _tokenService;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="ClientController"/>.
        /// </summary>
        /// <param name="clientRepository">O repositório de clientes.</param>
        /// <param name="userRepository">O repositório de usuários.</param>
        /// <param name="tokenService">O serviço de tokens.</param>
        public ClientController(ClientRepository clientRepository, UserRepository userRepository, TokenService tokenService)
        {
            _clientRepository = clientRepository;
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Registra um novo cliente.
        /// </summary>
        /// <param name="request">Os dados de registro do cliente.</param>
        /// <returns>Um objeto <see cref="IActionResult"/> contendo o resultado da operação.</returns>
        [HttpPost("register")]
        public async Task<IActionResult> RegisterClient([FromBody] RegisterRequestModel request)
        {
            // Criar usuário
            var user = new UserModel
            {
                Nome = request.Nome,
                Email = request.Email,
                Senha = request.Senha
            };
            var userId = await _userRepository.CreateUserAsync(user);

            // Criar cliente
            var clientId = await _clientRepository.CreateClienteAsync(userId, request.Cpf, request.DataNascimento, request.Telefone);

            // Gerar token JWT
            var token = _tokenService.GenerateJwtToken(user);
            await _userRepository.SaveSessionTokenAsync(userId, token);

            return Ok(new { UserId = userId, ClienteId = clientId, Token = token });
        }
    }
}