using Dapper;
using Microsoft.Data.Sqlite;
using PracticalTask5_RestApi.Models;

namespace PracticalTask5_RestApi.Services
{
    public class StudentRepository
    {
        private readonly string _connectionString;

        public StudentRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        private SqliteConnection CreateConnection() => new SqliteConnection(_connectionString);

        public async Task<IEnumerable<Student>> GetStudentsAsync(string? group = null, double? gpa = null)
        {
            using var conn = CreateConnection();
            var query = "SELECT * FROM Students WHERE 1=1";
            if (!string.IsNullOrEmpty(group)) query += " AND [Group] = @Group";
            if (gpa.HasValue) query += " AND GPA >= @GPA";
            return await conn.QueryAsync<Student>(query, new { Group = group, GPA = gpa });
        }

        public async Task<Student?> GetByIdAsync(int id)
        {
            using var conn = CreateConnection();
            return await conn.QueryFirstOrDefaultAsync<Student>("SELECT * FROM Students WHERE Id=@Id", new { Id = id });
        }

        public async Task<int> AddAsync(Student student)
        {
            using var conn = CreateConnection();
            var sql = "INSERT INTO Students (FullName, [Group], GPA) VALUES (@FullName, @Group, @GPA)";
            return await conn.ExecuteAsync(sql, student);
        }

        public async Task<int> UpdateAsync(Student student)
        {
            using var conn = CreateConnection();
            var sql = "UPDATE Students SET FullName=@FullName, [Group]=@Group, GPA=@GPA WHERE Id=@Id";
            return await conn.ExecuteAsync(sql, student);
        }

        public async Task<int> DeleteAsync(int id)
        {
            using var conn = CreateConnection();
            return await conn.ExecuteAsync("DELETE FROM Students WHERE Id=@Id", new { Id = id });
        }
    }
}
