
namespace Backend.Core.Application.Services
{
    public interface IApplicationDbContext
    {
        ICategoryRepository Categories { get; set; }
        IProductRepository Products { get; set; }
    }
}
