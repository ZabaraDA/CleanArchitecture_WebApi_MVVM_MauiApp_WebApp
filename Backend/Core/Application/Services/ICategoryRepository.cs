using Backend.Core.Domain.Models;

namespace Backend.Core.Application.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IReadOnlyList<Product>> GetAllProductsAsync(int categoryId);
    }
}
