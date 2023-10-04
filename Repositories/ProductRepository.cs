using Backend.Core.Application.Services;
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

        public Task<bool> AddAsync(Product entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Product>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetLatest()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
