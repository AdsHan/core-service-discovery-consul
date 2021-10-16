using Microsoft.EntityFrameworkCore;
using SDC.Products.Domain.Entities;
using SDC.Products.Domain.Enums;
using SDC.Products.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDC.Products.Infrastructure.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly CatalogDbContext _dbContext;

        public ProductRepository(CatalogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ProductModel>> GetAllAsync()
        {
            return await _dbContext.Products
                .Where(a => a.Status == EntityStatusEnum.Active)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ProductModel> GetByIdAsync(Guid id)
        {
            return await _dbContext.Products
                .Where(a => a.Status == EntityStatusEnum.Active)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task UpdateAsync(ProductModel product)
        {
            // Refor�o que a entidade foi alterada
            _dbContext.Entry(product).State = EntityState.Modified;
            _dbContext.Update(product);
        }

        public async Task AddAsync(ProductModel product)
        {
            _dbContext.Add(product);
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

    }
}