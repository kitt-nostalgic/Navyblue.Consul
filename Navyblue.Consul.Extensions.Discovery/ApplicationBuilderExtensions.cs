using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Navyblue.BaseLibrary;
using Navyblue.Consul.Agent.Model;
using Navyblue.Consul.Extensions.Discovery.ServiceRegistry;

namespace Navyblue.Consul.Extensions.Discovery
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseConsulRegisterService(this IApplicationBuilder app, IServiceCollection serviceCollection)
        {
            var configuration = app.ApplicationServices.GetRequiredService<IConfiguration>() ?? throw new ArgumentException("Missing dependency", nameof(IOptions<IConfiguration>));
            serviceCollection.Configure<ConsulDiscoveryConfiguration>(p=> configuration.GetSection("Consul:Discovery").Bind(p));
            
            var appLife = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>() ?? throw new ArgumentException("Missing dependency", nameof(IHostApplicationLifetime));
            var consulServiceRegistryClient = app.ApplicationServices.GetRequiredService<IConsulServiceRegistryClient>() ?? throw new ArgumentException("Missing dependency", nameof(IOptions<IConsulServiceRegistryClient>));
            var consoleDiscoveryConfiguration = app.ApplicationServices.GetRequiredService<IOptions<ConsulDiscoveryConfiguration>>() ?? throw new ArgumentException("Missing dependency", nameof(IOptions<IConsulServiceRegistryClient>));

            if (string.IsNullOrEmpty(consoleDiscoveryConfiguration.Value.ServiceName))
            {
                throw new ArgumentException("Service Name must be configured", nameof(consoleDiscoveryConfiguration.Value.ServiceName));
            }

            var serviceId = consoleDiscoveryConfiguration.Value.InstanceId.IsNotNullOrEmpty() ? 
                $"{consoleDiscoveryConfiguration.Value.ServiceName}_{consoleDiscoveryConfiguration.Value.IpAddress}:{consoleDiscoveryConfiguration.Value.Port}": 
                consoleDiscoveryConfiguration.Value.InstanceId.FormatWith(consoleDiscoveryConfiguration.Value.IpAddress);

            var serviceChecks = new List<NewService.Check>();

            if (!string.IsNullOrEmpty(consoleDiscoveryConfiguration.Value.HealthCheckPath))
            {
                serviceChecks.Add(new NewService.Check()
                {
                    Status = CheckStatus.Passing.ToString(),
                    DeregisterCriticalServiceAfter = "1m",
                    Interval = "5s",
                    Http = consoleDiscoveryConfiguration.Value.Scheme + "://"+ consoleDiscoveryConfiguration.Value.IpAddress + ":" + consoleDiscoveryConfiguration.Value.Port + "/" + consoleDiscoveryConfiguration.Value.HealthCheckPath
                });
            }

            var registration = new NewService
            {
                Checks = serviceChecks.ToList(),
                Address = consoleDiscoveryConfiguration.Value.IpAddress,
                Id = serviceId,
                Name = consoleDiscoveryConfiguration.Value.ServiceName,
                Port = consoleDiscoveryConfiguration.Value.Port,
            };

            serviceCollection.AddScoped(p=> registration);

            appLife.ApplicationStarted.Register(() =>
            {
                consulServiceRegistryClient.Register();
            });

            appLife.ApplicationStopping.Register(() =>
            {
                consulServiceRegistryClient.Deregister();
            });

            return app;
        }
    }
}