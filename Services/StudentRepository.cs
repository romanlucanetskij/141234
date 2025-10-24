using Dapper;
using Microsoft.Data.Sqlite;
using PracticalTask5_RestApi.Models;
using System.Text;

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
            var queryBuilder = new StringBuilder("SELECT Id, FullName, [Group], GPA FROM Students WHERE 1=1");
            var parameters = new DynamicParameters();

            if (!string.IsNullOrWhiteSpace(group))
            {
                queryBuilder.Append(" AND [Group] = @Group");
                parameters.Add("Group", group);
            }

            if (gpa.HasValue)
            {
                queryBuilder.Append(" AND GPA >= @Gpa");
                parameters.Add("Gpa", gpa.Value);
            }

            queryBuilder.Append(" ORDER BY Id");

            return await conn.QueryAsync<Student>(queryBuilder.ToString(), parameters);
        }

        public async Task<Student?> GetByIdAsync(int id)
        {
            using var conn = CreateConnection();
            return await conn.QueryFirstOrDefaultAsync<Student>("SELECT * FROM Students WHERE Id=@Id", new { Id = id });
        }

        public async Task<Student> AddAsync(Student student)
        {
            using var conn = CreateConnection();
            const string sql = @"INSERT INTO Students (FullName, [Group], GPA)
                                 VALUES (@FullName, @Group, @GPA);
                                 SELECT last_insert_rowid();";

            var newId = await conn.ExecuteScalarAsync<long>(sql, student);

            return new Student
            {
                Id = (int)newId,
                FullName = student.FullName,
                Group = student.Group,
                GPA = student.GPA
            };
        }

        public async Task<bool> UpdateAsync(Student student)
        {
            using var conn = CreateConnection();
            const string sql = "UPDATE Students SET FullName=@FullName, [Group]=@Group, GPA=@GPA WHERE Id=@Id";
            var affectedRows = await conn.ExecuteAsync(sql, student);
            return affectedRows > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var conn = CreateConnection();
            const string sql = "DELETE FROM Students WHERE Id=@Id";
            var affectedRows = await conn.ExecuteAsync(sql, new { Id = id });
            return affectedRows > 0;
        }
    }
}
