using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SDC.Kart.API.Services.ServiceDiscovery;
using System;

namespace SDC.Kart.API.Configuration
{
    public static class ConsulConfig
    {
        public static IServiceCollection AddConsulConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(consulConfig =>
            {
                var address = configuration.GetValue<string>("Consul:ConsulAddress");
                consulConfig.Address = new Uri(address);
            }));

            services.AddTransient<IServiceDiscoveryService, ServiceDiscoveryService>();

            return services;
        }

        public static IApplicationBuilder UseConsul(this IApplicationBuilder app, IConfiguration configuration)
        {
            var logger = app.ApplicationServices.GetRequiredService<ILoggerFactory>().CreateLogger("AppExtensions");
            var consulClient = app.ApplicationServices.GetRequiredService<IConsulClient>();
            var lifeTime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();

            var serviceName = configuration.GetValue<string>("Consul:ServiceName");
            var serviceId = configuration.GetValue<string>("Consul:ServiceId");
            var servicePort = configuration.GetValue<int>("Consul:ServicePort");
            var serviceIP = configuration.GetValue<string>("Consul:ServiceIP");
            var serviceHealthCheck = configuration.GetValue<string>("Consul:ServiceHealthCheck");

            var registration = new AgentServiceRegistration()
            {
                ID = $"{serviceId}{Guid.NewGuid()}",
                Name = serviceName,
                Address = serviceIP,
                Port = servicePort
            };

            consulClient.Agent.ServiceDeregister(registration.ID).ConfigureAwait(true);
            consulClient.Agent.ServiceRegister(registration).ConfigureAwait(true);

            logger.LogInformation("Serviço registrando no Consul");

            lifeTime.ApplicationStopping.Register(() =>
            {
                consulClient.Agent.ServiceDeregister(registration.ID).ConfigureAwait(true);
                logger.LogInformation("Serviço desregistrando no Consul");
            });

            return app;
        }
    }
}