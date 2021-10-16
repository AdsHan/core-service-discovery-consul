using Consul;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SDC.Kart.API.Services.ServiceDiscovery
{
    public class ServiceDiscoveryService : IServiceDiscoveryService
    {
        private readonly IConsulClient _consulClient;
        public ServiceDiscoveryService(IConsulClient consulClient)
        {
            _consulClient = consulClient;
        }

        public async Task<Uri> GetServiceUri(string name, string url)
        {
            var registeredServices = await _consulClient.Agent.Services();

            var instances = registeredServices.Response?
                .Where(s => s.Value.Service.Equals(name, StringComparison.OrdinalIgnoreCase))
                .Select(s => s.Value)
                .ToList();

            var service = instances.FirstOrDefault();

            if (service == null) return null;

            var uri = new Uri($"http://{service.Address}:{service.Port}/{url}");

            return uri;
        }
    }
}