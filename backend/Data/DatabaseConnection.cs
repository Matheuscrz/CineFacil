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
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new ArgumentNullException(nameof(configuration));
        }

        public IDbConnection GetConnection()
        {
            if (_connection == null || _connection.State == ConnectionState.Closed)
            {
                _connection = new NpgsqlConnection(_connectionString);
                _connection.Open();
            }
            return _connection;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _connection?.Close();
                    _connection?.Dispose();
                    _connection = null;
                }
                _disposed = true;
            }
        }

        ~DatabaseConnection()
        {
            Dispose(false);
        }
    }
}