using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SDC.Products.API.Configuration;
using SDC.Products.API.Service;

namespace SDC.Products.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApiConfiguration(Configuration);

            services.AddDependencyConfiguration(Configuration);

            services.AddSwaggerConfiguration();

            services.AddConsulConfiguration(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ProductPopulateService productPopulateService)
        {
            app.UseApiConfiguration(env, productPopulateService);

            app.UseSwaggerConfiguration();

            app.UseConsul(Configuration);
        }
    }
}
