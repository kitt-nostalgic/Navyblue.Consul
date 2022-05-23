using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Navyblue.Consul.Catalog;
using Navyblue.Consul.Catalog.Model;

namespace Navyblue.Consul.Extensions.WebApiClient
{
    public static class HttpApiServiceCollectionExtensions
    {
        public static IHttpClientBuilder AddHttpApiAsServiceInvoke<THttpApi>(
            this IServiceCollection services)
            where THttpApi : class
        {
            return services
                .AddHttpApi<THttpApi>((options, serviceProvider) =>
                {
                    ConsulServiceAttribute? consulServiceAttribute = typeof(THttpApi).GetCustomAttribute<ConsulServiceAttribute>();
                    if (consulServiceAttribute == null)
                    {
                        throw new Exception($"Please add ConsulServiceAttribute to this Interface {typeof(THttpApi).FullName}");
                    }

                    using (var scope = serviceProvider.CreateScope())
                    {
                        ICatalogClient consul = scope.ServiceProvider.GetRequiredService<ICatalogClient>() ?? throw new ArgumentException("Missing dependency", nameof(ICatalogClient)); ;

                        var service = consul.GetCatalogService(consulServiceAttribute.ServiceName, new CatalogServiceRequest());

                        if (service.Value != null)
                        {
                            Random random = new Random(Environment.TickCount);

                            int index = random.Next(service.Value.Count);

                            CatalogService catalog = service.Value[index];

                            UriBuilder builder = new UriBuilder(catalog.ServiceAddress)
                            {
                                Port = catalog.ServicePort.Value,
                            };

                            options.HttpHost = builder.Uri;
                        }
                    }
                });
        }
    }
}
