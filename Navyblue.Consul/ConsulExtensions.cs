using Microsoft.Extensions.DependencyInjection;
using Navyblue.Consul.ACL;
using Navyblue.Consul.Agent;
using Navyblue.Consul.Catalog;
using Navyblue.Consul.Coordinate;
using Navyblue.Consul.Event;
using Navyblue.Consul.Health;
using Navyblue.Consul.KV;
using Navyblue.Consul.Query;
using Navyblue.Consul.Session;
using Navyblue.Consul.Status;
using Navyblue.Consul.Transport;

namespace Navyblue.Consul;

public static class ConsulExtensions
{
    public static IServiceCollection AddConsul(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped(typeof(IAclClient), typeof(AclConsulClient));
        serviceCollection.AddScoped(typeof(IAgentClient), typeof(AgentConsulClient));
        serviceCollection.AddScoped(typeof(ICatalogClient), typeof(CatalogConsulClient));
        serviceCollection.AddScoped(typeof(ICoordinateClient), typeof(CoordinateConsulClient));
        serviceCollection.AddScoped(typeof(IEventClient), typeof(EventConsulClient));
        serviceCollection.AddScoped(typeof(IHealthClient), typeof(HealthConsulClient));
        serviceCollection.AddScoped(typeof(IKeyValueClient), typeof(KeyValueConsulClient));
        serviceCollection.AddScoped(typeof(IQueryClient), typeof(QueryConsulClient));
        serviceCollection.AddScoped(typeof(ISessionClient), typeof(SessionConsulClient));
        serviceCollection.AddScoped(typeof(IStatusClient), typeof(StatusConsulClient));
        serviceCollection.AddScoped(typeof(IHttpTransport), typeof(ConsulHttpTransport));
        serviceCollection.AddScoped(typeof(IConsulRawClient), typeof(ConsulRawClient));
        serviceCollection.AddScoped(typeof(IConsulClient), typeof(ConsulClient));

        serviceCollection.Configure<ConsulConfiguration>("Consul", _=>{ });

        return serviceCollection;
    }
}