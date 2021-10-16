using Microsoft.EntityFrameworkCore;
using SDC.Products.Domain.Entities;

namespace SDC.Products.Infrastructure.Data
{
    public class CatalogDbContext : DbContext
    {

        public CatalogDbContext()
        {

        }

        public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
        {

        }

        public DbSet<ProductModel> Products { get; set; }

    }
}
