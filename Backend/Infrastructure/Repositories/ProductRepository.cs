using Backend.Core.Application.Interfaces;
using Backend.Core.Domain.Models;
using Dapper;
using Npgsql;
using System.Data;

namespace Backend.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;
        public ProductRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<bool> AddAsync(Product product)
        {
            using (IDbConnection connection = new NpgsqlConnection(_connectionString))
            {
                const string sqlCommand = "INSERT INTO product (name,photo,categoryid) VALUES (@name,@photo,@categoryid)";

                var result = await connection.ExecuteAsync(sqlCommand, new { product.Name, product.Photo,product.CategoryId });

                return Convert.ToBoolean(result);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (IDbConnection connection = new NpgsqlConnection(_connectionString))
            {
                const string sqlCommand = "DELETE FROM product WHERE id = @id";
                var result = await connection.ExecuteAsync(sqlCommand, new { id });
                return Convert.ToBoolean(result);
            }
        }

        public async Task<IReadOnlyList<Product>> GetAllAsync()
        {
            using (IDbConnection connection = new NpgsqlConnection(_connectionString))
            {
                const string sqlCommand = "SELECT * FROM product JOIN category ON product.categoryid = category.id";
                var result = await connection.QueryAsync<Product, Category, Product>(sqlCommand, (product, category) =>
                {
                    product.Category = category;
                    return product;
                });
                return result.ToList();
            }
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            using (IDbConnection connection = new NpgsqlConnection(_connectionString))
            {
                const string sqlCommand = "SELECT * FROM product JOIN category ON product.categoryid = category.id WHERE product.id = @id LIMIT 1";
                var product = (await connection.QueryAsync<Product, Category, Product>(sqlCommand, (product, category) =>
                {
                    product.Category = category;
                    return product;
                }, new { id })).FirstOrDefault();
                return product;
            }
        }

        public async Task<Product?> GetLatest()
        {
            using (IDbConnection connection = new NpgsqlConnection(_connectionString))
            {
                const string sqlCommand = "SELECT * FROM product JOIN category ON product.categoryid = category.id ORDER BY product.id DESC LIMIT 1";

                var result = (await connection.QueryAsync<Product, Category, Product>(sqlCommand, (product, category) =>
                {
                    product.Category = category;
                    return product;
                })).FirstOrDefault();
                return result;
            }
        }

        public Task<bool> UpdateAsync(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
