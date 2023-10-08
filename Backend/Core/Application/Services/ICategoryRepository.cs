using Backend.Core.Domain.Models;

namespace Backend.Core.Application.Services
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IReadOnlyList<Product>> GetAllProductsAsync(int categoryId);
    }
}
