using Backend.Core.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Infrastructure.Repositories
{
    public class ApplicationDbContext : IApplicationDbContext
    {
        public ApplicationDbContext(ICategoryRepository categories, IProductRepository products) 
        {
            Categories = categories;
            Products = products;
        }

        public ICategoryRepository Categories { get; set; }
        public IProductRepository Products { get; set; }
    }
}
