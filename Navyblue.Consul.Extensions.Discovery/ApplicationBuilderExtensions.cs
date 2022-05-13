using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Navyblue.BaseLibrary;
using Navyblue.Consul.Agent.Model;
using Navyblue.Consul.Extensions.Discovery.ServiceRegistry;

namespace Navyblue.Consul.Extensions.Discovery
{
    public static class ApplicationBuilderExtensions
    {
        public static IServiceCollection AddConsulRegisterService(this IServiceCollection serviceCollection)
        {
            //serviceCollection.AddSingleton<HeartbeatBackgroundService>();
            //serviceCollection.AddHostedService(provider => provider.GetService<HeartbeatBackgroundService>());

            ServiceProvider serviceProvider=serviceCollection.BuildServiceProvider();

            RegisterDiscoveryConfiguration(serviceCollection);

            var configuration = serviceCollection.BuildServiceProvider().GetRequiredService<IConfiguration>();

            var consoleDiscoveryConfiguration = new ConsulDiscoveryConfiguration();
            configuration.Bind("Consul:Discovery", consoleDiscoveryConfiguration);

            if (!consoleDiscoveryConfiguration.IsEnabled)
            {
                return serviceCollection;
            }

            if (string.IsNullOrEmpty(consoleDiscoveryConfiguration.ServiceName))
            {
                throw new ArgumentException("Service Name must be configured", nameof(consoleDiscoveryConfiguration.ServiceName));
            }

            var registration = BuildNewService(consoleDiscoveryConfiguration);

            serviceCollection.AddScoped(p=> registration);

            return serviceCollection;
        }

        public static IApplicationBuilder UseConsulRegisterService(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var consulServiceRegistryClient = scope.ServiceProvider.GetRequiredService<IConsulServiceRegistryClient>() ?? throw new ArgumentException("Missing dependency", nameof(IConsulServiceRegistryClient));

                var appLife = scope.ServiceProvider.GetRequiredService<IHostApplicationLifetime>() ?? throw new ArgumentException("Missing dependency", nameof(IHostApplicationLifetime));

                appLife.ApplicationStarted.Register(() =>
                {
                    consulServiceRegistryClient.Register();
                });

                appLife.ApplicationStopping.Register(() =>
                {
                    consulServiceRegistryClient.Deregister();
                });
            }

            return app;
        }

        private static NewService BuildNewService(ConsulDiscoveryConfiguration consoleDiscoveryConfiguration)
        {
            var serviceChecks = new List<NewService.Check>();

            if (!string.IsNullOrEmpty(consoleDiscoveryConfiguration.HealthCheckPath))
            {
                serviceChecks.Add(new NewService.Check()
                {
                    Status = CheckStatus.Passing.Description(),
                    DeregisterCriticalServiceAfter = "1m",
                    Interval = "5s",
                    Http = consoleDiscoveryConfiguration.Scheme + "://" + consoleDiscoveryConfiguration.IpAddress +
                           ":" + consoleDiscoveryConfiguration.Port + "/" +
                           consoleDiscoveryConfiguration.HealthCheckPath
                });
            }

            var serviceId = GetServiceId(consoleDiscoveryConfiguration);

            var registration = new NewService
            {
                Id = serviceId,
                Checks = serviceChecks.ToList(),
                Name = consoleDiscoveryConfiguration.ServiceName,
                Address = consoleDiscoveryConfiguration.IpAddress,
                Port = consoleDiscoveryConfiguration.Port,
            };

            return registration;
        }

        private static string GetServiceId(ConsulDiscoveryConfiguration consoleDiscoveryConfiguration)
        {
            var serviceId = consoleDiscoveryConfiguration.InstanceId.IsNotNullOrEmpty()
                ? $"{consoleDiscoveryConfiguration.ServiceName}_{consoleDiscoveryConfiguration.IpAddress}:{consoleDiscoveryConfiguration.Port}"
                : consoleDiscoveryConfiguration.InstanceId.FormatWith(consoleDiscoveryConfiguration.IpAddress);

            return serviceId;
        }

        private static void RegisterDiscoveryConfiguration(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<HeartbeatConfiguration>();
        }
    }
}