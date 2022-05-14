using Microsoft.Extensions.Configuration;
using Navyblue.BaseLibrary;
using Navyblue.Consul.Catalog;
using Navyblue.Consul.Health;
using Navyblue.Consul.Health.Model;
using Serilog;

namespace Navyblue.Consul.Extensions.Discovery.ServiceDiscovery;

public class ConsulDiscoveryClient : IConsulDiscoveryClient
{
    private readonly ILogger _logger;

    private readonly IConsulClient _consulClient;

    private readonly ConsulDiscoveryConfiguration _consulDiscoveryConfiguration;

    public ConsulDiscoveryClient(ConsulClient consulClient,
        IConfiguration configuration,
        ILogger logger)
    {
        this._consulClient = consulClient;
        this._consulDiscoveryConfiguration = new ConsulDiscoveryConfiguration(configuration);
        this._logger = logger.ForContext<ConsulDiscoveryClient>();

        configuration.Bind("Consul:Discovery", _consulDiscoveryConfiguration);
    }

    public List<Service> GetInstances(string serviceId)
    {
        return GetInstances(serviceId, QueryParams.DEFAULT);
    }

    public List<Service> GetInstances(string serviceId, QueryParams? queryParams)
    {
        List<Service> instances = new();

        AddInstancesToList(instances, serviceId, queryParams);

        return instances;
    }

    private void AddInstancesToList(List<Service> instances, string serviceId, QueryParams? queryParams)
    {
        string? aclToken = this._consulDiscoveryConfiguration.AclToken;
        ConsulResponse<IList<HealthService>> services = this._consulClient.GetHealthServices(serviceId, new HealthServicesRequest
        {
            Tags = new[] { this._consulDiscoveryConfiguration.DefaultQueryTag },
            QueryParams = queryParams,
            Passing = this._consulDiscoveryConfiguration.IsQueryPassing,
            Token = aclToken
        });

        this._logger.Information("Get the healthy services:" + services.ToJson());

        foreach (HealthService service in services.Value ?? new List<HealthService>())
        {
            instances.Add(new Service
            {
                Id = service.Service.Id,
                Port = service.Service.Port,
                Address = service.Service.Address,
                Meta = service.Service.Meta,
                ServiceText = serviceId
            });
        }
    }

    public List<Service> GetAllInstances()
    {
        List<Service> instances = new();

        ConsulResponse<IDictionary<string, IList<string>>> services = this._consulClient.GetCatalogServices(new CatalogServicesRequest
        {
            QueryParams = QueryParams.DEFAULT
        });

        foreach (string serviceId in services.Value.Keys)
        {
            AddInstancesToList(instances, serviceId, QueryParams.DEFAULT);
        }

        return instances;
    }

    public List<string>? GetServices()
    {
        string? aclToken = this._consulDiscoveryConfiguration.AclToken;

        var services = this._consulClient.GetCatalogServices(new CatalogServicesRequest
        {
            QueryParams = QueryParams.DEFAULT,
            Token = aclToken
        });

        return services.Value?.Keys.ToList();
    }
}