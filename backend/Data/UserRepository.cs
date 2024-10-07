using System.Threading.Tasks;
using backend.Models;
using Npgsql;
using BCrypt.Net;
using Dapper;

namespace backend.Data
{
    public class UserRepository
    {
        private readonly NpgsqlConnection _connection;
        private const string InsertUser = "INSERT INTO cinefacil.usuario (nome, email, senha) VALUES (@Nome, @Email, @Senha) RETURNING id";
        private const string SelectUserByEmail = "SELECT * FROM cinefacil.usuario WHERE email = @Email";
        private const string InsertSessionToken = "INSERT INTO cinefacil.session_tokens (user_id, token) VALUES (@UserId, @Token)";
        private const string DeactivateSessionToken = "UPDATE cinefacil.session_tokens SET is_active = FALSE WHERE token = @Token";

        public UserRepository(DatabaseConnection databaseConnection)
        {
            _connection = (NpgsqlConnection?)databaseConnection.GetConnection() ?? throw new InvalidOperationException("Database connection is not initialized.");
        }

        public async Task<int> CreateUserAsync(UserModel user)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Senha);
            var userId = await _connection.ExecuteScalarAsync<int>(InsertUser, new { user.Nome, user.Email, Senha = hashedPassword });
            return userId;
        }

        public async Task<UserModel?> GetUserByEmailAsync(string email)
        {
            var user = await _connection.QueryFirstOrDefaultAsync<UserModel>(SelectUserByEmail, new { Email = email });
            return user;
        }

        public async Task SaveSessionTokenAsync(int userId, string token)
        {
            await _connection.ExecuteAsync(InsertSessionToken, new { UserId = userId, Token = token });
        }

        public async Task DeactivateSessionTokenAsync(string token)
        {
            await _connection.ExecuteAsync(DeactivateSessionToken, new { Token = token });
        }
    }
}