using System;
using System.Data;
using Npgsql;
using Microsoft.Extensions.Configuration;

namespace backend.Data
{
    /// <summary>
    /// Classe responsável por gerenciar a conexão com o banco de dados.
    /// </summary>
    public class DatabaseConnection : IDisposable
    {
        private readonly string _connectionString;
        private NpgsqlConnection? _connection = null;
        private bool _disposed = false;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="DatabaseConnection"/>.
        /// </summary>
        /// <param name="configuration">A configuração da aplicação.</param>
        /// <exception cref="InvalidOperationException">Lançada quando a string de conexão 'DefaultConnection' não é encontrada.</exception>
        public DatabaseConnection(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            _connection = new NpgsqlConnection(_connectionString);
        }

        /// <summary>
        /// Obtém a conexão com o banco de dados.
        /// </summary>
        /// <returns>Uma instância de <see cref="IDbConnection"/>.</returns>
        public IDbConnection GetConnection()
        {
            if (_connection == null)
            {
                _connection = new NpgsqlConnection(_connectionString);
            }

            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }

            return _connection;
        }

        /// <summary>
        /// Libera os recursos não gerenciados usados pela instância da classe <see cref="DatabaseConnection"/>.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Libera os recursos não gerenciados usados pela instância da classe <see cref="DatabaseConnection"/>.
        /// </summary>
        /// <param name="disposing">Indica se os recursos gerenciados devem ser liberados.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _connection?.Dispose();
                }

                _disposed = true;
            }
        }
    }
}