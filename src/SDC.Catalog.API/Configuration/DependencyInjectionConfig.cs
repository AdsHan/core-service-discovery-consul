using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SDC.Products.API.Service;
using SDC.Products.Domain.Repositories;
using SDC.Products.Infrastructure.Data;
using SDC.Products.Infrastructure.Data.Repositories;

namespace SDC.Products.API.Configuration
{
    public static class DependencyInjectionConfig
    {

        public static IServiceCollection AddDependencyConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CatalogDbContext>(options => options.UseInMemoryDatabase("ProductsDB"));

            services.AddTransient<ProductPopulateService>();

            services.AddScoped<IProductRepository, ProductRepository>();

            return services;
        }
    }
}