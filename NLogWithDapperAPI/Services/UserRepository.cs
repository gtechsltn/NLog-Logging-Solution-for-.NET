using Dapper;
using Microsoft.Data.SqlClient;
using NLogWithDapperAPI.Data;
using NLogWithDapperAPI.Models;
using System.Data;

namespace NLogWithDapperAPI.Services
{
    public class UserRepository
    {
        private readonly DapperContext _connectionString;

        public UserRepository(IConfiguration configuration, DapperContext connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            using (var db = _connectionString.CreateDbConnection())
            {
                var sql = "SELECT * FROM Users";
                return await db.QueryAsync<User>(sql);
            }
        }

        public async Task<int> AddUserAsync(User user)
        {
            using (var db = _connectionString.CreateDbConnection())
            {
                var sql = "INSERT INTO Users (Name, Email) VALUES (@Name, @Email)";
                return await db.ExecuteAsync(sql, new { user.Name, user.Email });
            }
        }
    }
}
