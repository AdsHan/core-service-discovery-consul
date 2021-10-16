using SDC.Products.Domain.Entities;
using SDC.Products.Domain.Repositories;
using SDC.Products.Infrastructure.Data;
using System.Threading.Tasks;

namespace SDC.Products.API.Service
{
    public class ProductPopulateService
    {
        private readonly IProductRepository _productRepository;
        private readonly CatalogDbContext _context;

        public ProductPopulateService(IProductRepository productRepository, CatalogDbContext context)
        {
            _productRepository = productRepository;
            _context = context;
        }

        public void Initialize()
        {
            if (_context.Database.EnsureCreated())
            {
                CreateUserAsync(new ProductModel()
                {
                    Title = "Sandalia",
                    Description = "Sand�lia Preta Couro Salto Fino",
                    Price = 249.50,
                    Quantity = 100
                });

                CreateUserAsync(new ProductModel()
                {
                    Title = "Sapatilha",
                    Description = "Sapatilha Tecido Platino ",
                    Price = 142.50,
                    Quantity = 25
                });

                CreateUserAsync(new ProductModel()
                {
                    Title = "Chinelo",
                    Description = "Chinelo Tradicional Adulto-Unissex",
                    Price = 60.50,
                    Quantity = 50
                });

                _productRepository.SaveAsync();
            };
        }

        private async Task CreateUserAsync(ProductModel product)
        {

            if (_productRepository.GetByIdAsync(product.Id).Result == null)
            {
                var resultado = _productRepository.AddAsync(product);
            }

        }
    }
}