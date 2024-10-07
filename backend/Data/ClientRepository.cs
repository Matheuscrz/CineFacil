using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models;
using Dapper;
using Npgsql;

namespace backend.Data
{
    public class ClientRepository(DatabaseConnection databaseConnection)
    {
        private readonly NpgsqlConnection _connection = (NpgsqlConnection?)databaseConnection.GetConnection() ?? throw new InvalidOperationException("Database connection is not initialized.");
        private const string InsertCliente = "INSERT INTO cinefacil.cliente (usuarioId, cpf, dataNascimento, telefone) VALUES (@UsuarioId, @Cpf, @DataNascimento, @Telefone) RETURNING id";

        public async Task<int> CreateClienteAsync(int usuarioId, string cpf, DateTime dataNascimento, string telefone)
        {
            var clientId = await _connection.ExecuteScalarAsync<int>(InsertCliente, new { UsuarioId = usuarioId, Cpf = cpf, DataNascimento = dataNascimento, Telefone = telefone });
            return clientId;
        }
    }
}