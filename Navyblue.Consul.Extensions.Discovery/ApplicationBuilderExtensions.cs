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
            //serviceCollection.AddSingleton<HeartbeatBackgroundService>();
            //serviceCollection.AddHostedService(provider => provider.GetService<HeartbeatBackgroundService>());

            RegisterDiscoveryConfiguration(app, serviceCollection);

            var consoleDiscoveryConfiguration = app.ApplicationServices.GetRequiredService<IOptions<ConsulDiscoveryConfiguration>>() ?? throw new ArgumentException("Missing dependency", nameof(IOptions<IConsulServiceRegistryClient>));

            if (string.IsNullOrEmpty(consoleDiscoveryConfiguration.Value.ServiceName))
            {
                throw new ArgumentException("Service Name must be configured", nameof(consoleDiscoveryConfiguration.Value.ServiceName));
            }

            var serviceId = GetServiceId(consoleDiscoveryConfiguration);

            var registration = BuildNewService(consoleDiscoveryConfiguration.Value, serviceId);

            serviceCollection.AddScoped(p=> registration);

            RegisterToRuntime(app);

            return app;
        }

        private static NewService BuildNewService(ConsulDiscoveryConfiguration consoleDiscoveryConfiguration, string serviceId)
        {
            var serviceChecks = new List<NewService.Check>();

            if (!string.IsNullOrEmpty(consoleDiscoveryConfiguration.HealthCheckPath))
            {
                serviceChecks.Add(new NewService.Check()
                {
                    Status = CheckStatus.Passing.ToString(),
                    DeregisterCriticalServiceAfter = "1m",
                    Interval = "5s",
                    Http = consoleDiscoveryConfiguration.Scheme + "://" + consoleDiscoveryConfiguration.IpAddress +
                           ":" + consoleDiscoveryConfiguration.Port + "/" +
                           consoleDiscoveryConfiguration.HealthCheckPath
                });
            }

            var registration = new NewService
            {
                Checks = serviceChecks.ToList(),
                Address = consoleDiscoveryConfiguration.IpAddress,
                Id = serviceId,
                Name = consoleDiscoveryConfiguration.ServiceName,
                Port = consoleDiscoveryConfiguration.Port,
            };

            return registration;
        }

        private static string GetServiceId(IOptions<ConsulDiscoveryConfiguration> consoleDiscoveryConfiguration)
        {
            var serviceId = consoleDiscoveryConfiguration.Value.InstanceId.IsNotNullOrEmpty()
                ? $"{consoleDiscoveryConfiguration.Value.ServiceName}_{consoleDiscoveryConfiguration.Value.IpAddress}:{consoleDiscoveryConfiguration.Value.Port}"
                : consoleDiscoveryConfiguration.Value.InstanceId.FormatWith(consoleDiscoveryConfiguration.Value.IpAddress);
            return serviceId;
        }

        private static void RegisterToRuntime(IApplicationBuilder app)
        {
            var appLife = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>() ??
                          throw new ArgumentException("Missing dependency", nameof(IHostApplicationLifetime));

            var consulServiceRegistryClient = app.ApplicationServices.GetRequiredService<IConsulServiceRegistryClient>() ??
                                              throw new ArgumentException("Missing dependency",
                                                  nameof(IOptions<IConsulServiceRegistryClient>));

            appLife.ApplicationStarted.Register(() => { consulServiceRegistryClient.Register(); });

            appLife.ApplicationStopping.Register(() => { consulServiceRegistryClient.Deregister(); });
        }

        private static void RegisterDiscoveryConfiguration(IApplicationBuilder app, IServiceCollection serviceCollection)
        {
            var configuration = app.ApplicationServices.GetRequiredService<IConfiguration>() ??
                                throw new ArgumentException("Missing dependency", nameof(IOptions<IConfiguration>));

            serviceCollection.Configure<ConsulDiscoveryConfiguration>(p =>
                configuration.GetSection("Consul:Discovery").Bind(p));
        }
    }
}