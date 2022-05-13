using Navyblue.Consul.Agent.Model;

namespace Navyblue.Consul.Extensions.Discovery.ServiceRegistry;

public class ConsulServiceRegistry : IConsulServiceRegistry
{
    private readonly HeartbeatConfiguration _heartbeatConfiguration;
    private readonly ConsulDiscoveryConfiguration _consulDiscoveryConfiguration;
    public NewService Service { get; }

    public ConsulServiceRegistry(NewService service,
            ConsulDiscoveryConfiguration consulDiscoveryConfiguration,
            HeartbeatConfiguration heartbeatConfiguration)
    {
        this.Service = service;
        this._heartbeatConfiguration = heartbeatConfiguration;
        _consulDiscoveryConfiguration = consulDiscoveryConfiguration;
    }

    public ConsulServiceRegistry Registration()
    {
        NewService service = new NewService();
        string? appName = GetAppName();
        service.Id = GetInstanceId();
        if (!_consulDiscoveryConfiguration.IsPreferAgentAddress)
        {
            service.Address = _consulDiscoveryConfiguration.Hostname;
        }
        service.Name = appName;
        service.Tags = CreateTags();

        if (_consulDiscoveryConfiguration.Port > 0)
        {
            service.Port = _consulDiscoveryConfiguration.Port;
            // we know the port and can set the check
            SetCheck(service);
        }

        ConsulServiceRegistry registration = new ConsulServiceRegistry(service, _consulDiscoveryConfiguration, _heartbeatConfiguration);

        return registration;
    }

    public void SetCheck(NewService service)
    {
        if (_consulDiscoveryConfiguration.IsRegisterHealthCheck && service.Checks == null)
        {
            int checkPort = service.Port.GetValueOrDefault();
            service.NewServiceCheck = CreateCheck(checkPort);
        }
    }

    public string? GetInstanceId()
    {
        return _consulDiscoveryConfiguration.InstanceId;
    }

    public List<string> CreateTags()
    {
        List<string> tags = new List<string>(_consulDiscoveryConfiguration.Tags);

        if (!string.IsNullOrWhiteSpace(_consulDiscoveryConfiguration.InstanceZone))
        {
            tags.Add(_consulDiscoveryConfiguration.DefaultZoneMetadataName + "=" + _consulDiscoveryConfiguration.InstanceZone);
        }
        if (!string.IsNullOrWhiteSpace(_consulDiscoveryConfiguration.InstanceGroup))
        {
            tags.Add("group=" + _consulDiscoveryConfiguration.InstanceGroup);
        }

        tags.Add("secure=" + _consulDiscoveryConfiguration.Scheme.Equals("https", StringComparison.OrdinalIgnoreCase));

        return tags;
    }

    public NewService.Check CreateCheck(int port)
    {
        NewService.Check check = new NewService.Check();
        if (string.IsNullOrWhiteSpace(_consulDiscoveryConfiguration.HealthCheckCriticalTimeout))
        {
            check.DeregisterCriticalServiceAfter = _consulDiscoveryConfiguration.HealthCheckCriticalTimeout;
        }
        if (_heartbeatConfiguration.Enabled)
        {
            check.Ttl = _heartbeatConfiguration.TtlUnit;
            return check;
        }

        if (!string.IsNullOrWhiteSpace(_consulDiscoveryConfiguration.HealthCheckPath))
        {
            check.Http = _consulDiscoveryConfiguration.HealthCheckPath;
        }
        else
        {
            check.Http = string.Format("%s://%s:%s%s", _consulDiscoveryConfiguration.Scheme, _consulDiscoveryConfiguration.Hostname, port, _consulDiscoveryConfiguration.HealthCheckPath);
        }
        check.Header = _consulDiscoveryConfiguration.HealthCheckHeaders;
        check.Interval = _consulDiscoveryConfiguration.HealthCheckInterval;
        check.Timeout = _consulDiscoveryConfiguration.HealthCheckTimeout;
        check.TlsSkipVerify = _consulDiscoveryConfiguration.IsHealthCheckTlsSkipVerify;
        return check;
    }

    /// <summary>
    /// Gets the name of the application.
    /// </summary>
    /// <returns></returns>
    public string? GetAppName()
    {
        return _consulDiscoveryConfiguration.ServiceName;
    }

    public void InitializePort(int knownPort)
    {
        if (Service.Port == 0)
        {
            Service.Port = knownPort;
        }

        SetCheck(Service);
    }

    //public string? GetInstanceId()
    //{
    //    return Service.Id;
    //}

    public string? GetServiceId()
    {
        return Service.Name;
    }

    public string? GetHost()
    {
        return Service.Address;
    }

    public int GetPort()
    {
        return Service.Port.GetValueOrDefault();
    }

    public bool IsSecure()
    {
        return this._consulDiscoveryConfiguration.Scheme.Equals("https", StringComparison.OrdinalIgnoreCase);
    }

    public Dictionary<string, string> GetMetadata()
    {
        Dictionary<string, string> dictionary = new Dictionary<string, string>();
        if (Service.Tags == null) return dictionary;

        foreach (var serviceTag in Service.Tags)
        {
            string[] splitTags = serviceTag.Split("=");
            switch (serviceTag.Length)
            {
                case 0:
                    break;
                case 1:
                    dictionary.Add(splitTags[0], splitTags[0]);
                    break;
                case 2:
                    dictionary.Add(splitTags[0], splitTags[1]);
                    break;
            }


            dictionary.Add(splitTags[0], splitTags[1]);
        }

        return dictionary;
    }

    public Uri GetUri()
    {
        string scheme = IsSecure() ? "https" : "http";
        int port = GetPort();
        if (port <= 0)
        {
            port = (IsSecure()) ? 443 : 80;
        }

        string uri = string.Format("%s://%s:%s", scheme, GetHost(), port);

        return new Uri(uri);
    }
}