using System;
using System.Data;
using Npgsql;
using Microsoft.Extensions.Configuration;

namespace backend.Data
{
    public class DatabaseConnection : IDisposable
    {
        private readonly string _connectionString;
        private NpgsqlConnection? _connection = null;
        private bool _disposed = false;

        public DatabaseConnection(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            _connection = new NpgsqlConnection(_connectionString);
        }

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

        public void Dispose()
        {
            if (!_disposed)
            {
                _connection?.Dispose();
                _disposed = true;
            }
        }
    }
}