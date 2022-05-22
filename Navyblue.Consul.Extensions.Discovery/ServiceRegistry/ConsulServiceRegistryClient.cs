using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Navyblue.BaseLibrary;
using Navyblue.Consul.Health;
using ILogger = Serilog.ILogger;

namespace Navyblue.Consul.Extensions.Discovery.ServiceRegistry;

public class ConsulServiceRegistryClient : IConsulServiceRegistryClient
{
    private readonly ILogger<ConsulServiceRegistryClient> _logger;
    private readonly IConsulClient _consulClient;
    private readonly IConsulServiceRegistry _consulServiceRegistry;
    private readonly ConsulDiscoveryConfiguration _consulDiscoveryConfiguration;

    public ConsulServiceRegistryClient(IConsulClient client,
        IConfiguration configuration,
        IConsulServiceRegistry consulServiceRegistry,
        ILogger<ConsulServiceRegistryClient> logger)
    {
        this._consulClient = client;
        this._logger = logger;
        this._consulServiceRegistry = consulServiceRegistry;
        this._consulDiscoveryConfiguration = new ConsulDiscoveryConfiguration(configuration);

        configuration.Bind("Consul:Discovery", _consulDiscoveryConfiguration);
    }

    public void Register()
    {
        _logger.LogInformation("Registering service with consul: " + _consulServiceRegistry.Service);
        try
        {
            if (!this._consulDiscoveryConfiguration.IsRegister)
            {
                _logger.LogDebug("Registration disabled.");
                return;
            }

            var registration = this._consulServiceRegistry.Registration();

            this._consulClient.AgentServiceRegister(_consulServiceRegistry.Service, this._consulDiscoveryConfiguration.AclToken);
        }
        catch (ConsulException e)
        {
            if (this._consulDiscoveryConfiguration.IsFailFast)
            {
                _logger.LogError("Error registering service with consul: " + _consulServiceRegistry.Service,
                        e);
                throw;
            }

            _logger.LogWarning("Failfast is false. Error registering service with consul: " + _consulServiceRegistry.Service, e);
        }
    }

    public void Deregister()
    {
        if (!this._consulDiscoveryConfiguration.IsRegister || !this._consulDiscoveryConfiguration.IsDeregister)
        {
            return;
        }

        _logger.LogInformation("Deregistering service with consul: " + _consulServiceRegistry.GetInstanceId());

        this._consulClient.AgentServiceDeregister(_consulServiceRegistry.GetInstanceId(), this._consulDiscoveryConfiguration.AclToken);
    }

    public string? GetAppName()
    {
        string? appName = this._consulDiscoveryConfiguration.ServiceName;
        return appName.IsNullOrWhiteSpace() ? this._consulDiscoveryConfiguration.ServiceName : appName;
    }

    public void SetStatus(string status)
    {
        if (status.Equals("OUT_OF_SERVICE", StringComparison.OrdinalIgnoreCase))
        {
            this._consulClient.AgentServiceSetMaintenance(_consulServiceRegistry.GetInstanceId(), true);
        }
        else if (status.Equals("UP", StringComparison.OrdinalIgnoreCase))
        {
            this._consulClient.AgentServiceSetMaintenance(_consulServiceRegistry.GetInstanceId(), false);
        }
        else
        {
            throw new ArgumentException("Unknown status: " + status);
        }
    }

    public string GetStatus()
    {
        string? serviceId = _consulServiceRegistry.GetServiceId();
        var response = this._consulClient.GetHealthChecksForService(serviceId, new HealthChecksForServiceRequest
        {
            QueryParams = QueryParams.DEFAULT
        });
        IList<Health.Model.Check>? checks = response.Value;
        if (checks == null || !checks.Any())
        {
            return "UP";
        }

        foreach (Health.Model.Check check in checks)
        {
            if (check.ServiceId != null && check.ServiceId.Equals(_consulServiceRegistry.GetInstanceId(), StringComparison.OrdinalIgnoreCase))
            {
                if (!string.IsNullOrWhiteSpace(check.Name) && check.Name.Equals("Service Maintenance Mode", StringComparison.OrdinalIgnoreCase))
                {
                    return "OUT_OF_SERVICE";
                }
            }
        }

        return "UP";
    }
}