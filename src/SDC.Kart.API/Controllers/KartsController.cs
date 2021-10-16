using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SDC.Kart.API.DTOs;
using SDC.Kart.API.Services.ServiceDiscovery;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SDC.Products.API.Controllers
{
    [Route("api/karts")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IServiceDiscoveryService _serviceDiscovery;
        private readonly HttpClient _httpClient;

        public ProductsController(IServiceDiscoveryService serviceDiscovery, HttpClient httpClient)
        {
            _serviceDiscovery = serviceDiscovery;
            _httpClient = httpClient;
        }

        // GET
        [HttpGet("add-product/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AddProduct(Guid id)
        {
            var catalogUrl = await _serviceDiscovery.GetServiceUri("CatalogService", $"api/products/{id}");

            if (catalogUrl != null)
            {
                var result = await _httpClient.GetAsync(catalogUrl);
                var stringResult = await result.Content.ReadAsStringAsync();
                var productDTO = JsonConvert.DeserializeObject<ProductDTO>(stringResult);
            }

            // ...
            // ...
            // ...

            return Ok();
        }
    }
}
