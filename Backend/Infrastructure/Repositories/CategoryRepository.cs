using Npgsql;
using System.Data;
using Dapper;
using Backend.Core.Application.Interfaces;
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
            using (IDbConnection connection = new NpgsqlConnection(_connectionString))
            {
                const string sqlCommand = "DELETE FROM category WHERE id = @id";
                var result = await connection.ExecuteAsync(sqlCommand, new { id });
                return Convert.ToBoolean(result);
            }
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

        public async Task<IReadOnlyList<Product>> GetAllProductsAsync(int categoryId)
        {
            using (IDbConnection connection = new NpgsqlConnection(_connectionString))
            {
                const string sqlCommand = "SELECT * FROM product WHERE categoryid = @categoryId ";
                var result = await connection.QueryAsync<Product>(sqlCommand, new { categoryId });
                return result.ToList();
            }
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            using (IDbConnection connection = new NpgsqlConnection(_connectionString))
            {
                const string sqlCommand = "SELECT * FROM category WHERE id = @id";
                return await connection.QueryFirstOrDefaultAsync<Category>(sqlCommand, new { id });
            }
        }

        public async Task<Category?> GetLatest()
        {
            using (IDbConnection connection = new NpgsqlConnection(_connectionString))
            {
                //SELECT * FROM category ORDER BY id DESC LIMIT 1
                const string sqlCommand = "SELECT * FROM category WHERE id=(SELECT max(id) FROM category ) LIMIT 1";
                return await connection.QueryFirstOrDefaultAsync<Category>(sqlCommand);
            }
        }

        public async Task<bool> UpdateAsync(Category category)
        {
            using (IDbConnection connection = new NpgsqlConnection(_connectionString))
            {
                const string sqlCommand = "UPDATE category SET name = @name WHERE id = @id";
                var result = await connection.ExecuteAsync(sqlCommand, new
                {
                    category.Name,
                    category.Id
                });
                return Convert.ToBoolean(result);
            }
        }
    }
}
