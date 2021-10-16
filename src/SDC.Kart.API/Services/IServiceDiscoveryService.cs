using System;
using System.Threading.Tasks;

namespace SDC.Kart.API.Services.ServiceDiscovery
{
    public interface IServiceDiscoveryService
    {
        Task<Uri> GetServiceUri(string serviceName, string requestUrl);
    }
}