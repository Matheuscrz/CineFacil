using System;
using System.Threading.Tasks;
using backend.Models;
using Dapper;
using Npgsql;

namespace backend.Data
{
    /// <summary>
    /// Repositório para operações relacionadas a clientes.
    /// </summary>
    public class ClientRepository
    {
        private readonly NpgsqlConnection _connection;
        private const string InsertCliente = "INSERT INTO cinefacil.cliente (usuarioId, cpf, dataNascimento, telefone) VALUES (@UserId, @Cpf, @DataNascimento, @Telefone) RETURNING id";

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="ClientRepository"/>.
        /// </summary>
        /// <param name="databaseConnection">A conexão com o banco de dados.</param>
        public ClientRepository(DatabaseConnection databaseConnection)
        {
            _connection = (NpgsqlConnection?)databaseConnection.GetConnection() ?? throw new InvalidOperationException("Database connection is not initialized.");
        }

        /// <summary>
        /// Cria um novo cliente no banco de dados.
        /// </summary>
        /// <param name="userId">O ID do usuário associado ao cliente.</param>
        /// <param name="cpf">O CPF do cliente.</param>
        /// <param name="dataNascimento">A data de nascimento do cliente.</param>
        /// <param name="telefone">O telefone do cliente.</param>
        /// <returns>O ID do cliente criado.</returns>
        public async Task<int> CreateClienteAsync(int userId, string cpf, DateTime dataNascimento, string telefone)
        {
            var clientId = await _connection.ExecuteScalarAsync<int>(InsertCliente, new { UserId = userId, Cpf = cpf, DataNascimento = dataNascimento, Telefone = telefone });
            return clientId;
        }
    }
}