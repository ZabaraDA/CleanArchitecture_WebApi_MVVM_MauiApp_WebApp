using Npgsql;
using System.Data;
using Dapper;
using Backend.Core.Application.Services;
using Backend.Core.Domain.Models;

namespace Backend.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly string _connectionString;
        public CategoryRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<bool> AddAsync(Category category)
        {
            using (IDbConnection connection = new NpgsqlConnection(_connectionString))
            {
                const string sqlCommand = "INSERT INTO category (name) VALUES (@name)";

                var result = await connection.ExecuteAsync(sqlCommand, new { category.Name });

                return Convert.ToBoolean(result);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<Category>> GetAllAsync()
        {
            using (IDbConnection connection = new NpgsqlConnection(_connectionString))
            {
                const string sqlCommand = "SELECT * FROM category";
                var result = await connection.QueryAsync<Category>(sqlCommand);
                return result.ToList();
            }
        }

        public Task<IReadOnlyCollection<Product>> GetAllProductsAsync(int categoryId)
        {
            throw new NotImplementedException();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            using (IDbConnection connection = new NpgsqlConnection(_connectionString))
            {
                const string sqlCommand = "SELECT * FROM category WHERE id = @id";
                return await connection.QueryFirstOrDefaultAsync<Category>(sqlCommand, new { id });
            }
        }

        public async Task<Category> GetLatest()
        {
            using (IDbConnection connection = new NpgsqlConnection(_connectionString))
            {
                //SELECT * FROM category ORDER BY id DESC LIMIT 1
                const string sqlCommand = "SELECT * FROM category WHERE id=(SELECT max(id) FROM category ) LIMIT 1";
                return await connection.QueryFirstOrDefaultAsync<Category>(sqlCommand);
            }
        }

        public Task<bool> UpdateAsync(Category category)
        {
            throw new NotImplementedException();
        }
    }
}
